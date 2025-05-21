import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { UserService } from 'app/core/user/user.service';
import { environment } from 'environments/environment';
import { catchError, from, Observable, of, switchMap, throwError } from 'rxjs';
import { SsoService } from '../sso/sso.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private _authenticated: boolean = false;
    private _authMethod: 'microsoft' | 'normal' | 'none' = 'none';
    constructor(
        private readonly _httpClient: HttpClient,
        private readonly _userService: UserService,
        private readonly _ssoService: SsoService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for access token
     */
    set accessToken(token: string) {
        localStorage.setItem('accessToken', token);
    }

    get accessToken(): string {
        return localStorage.getItem('accessToken') ?? '';
    }

    set refreshToken(token: string) {
        localStorage.setItem('refreshToken', token);
    }

    get refreshToken(): string {
        return localStorage.getItem('refreshToken') ?? '';
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // ----------------------------------------------------------------------------------------------------

    /**
     * Sign in
     *
     * @param credentials
     */
    signIn(credentials: { email: string; password: string }): Observable<any> {
        if (this._authenticated) {
            return throwError(() => new Error('User is already logged in.'));
        }

        const credentialsBase64 = window.btoa(
            `${credentials.email}:${credentials.password}`
        );

        return this._httpClient
            .post(`${environment.apiUrl}/token`, null, {
                headers: new HttpHeaders({
                    Authorization: `Basic ${credentialsBase64}`,
                    NO_HTTP_INTERCEPTOR: 'true',
                    'x-timezone':
                        Intl.DateTimeFormat().resolvedOptions().timeZone,
                }),
            })
            .pipe(
                switchMap((response: any) => {
                    if (!response.accessToken || !response.refreshToken) {
                        throw new Error('Invalid token response');
                    }

                    this.accessToken = response.accessToken;
                    this.refreshToken = response.refreshToken;
                    this._authenticated = true;
                    this._userService.user = AuthUtils.decodeToken(
                        response.accessToken
                    );

                    return of(response);
                }),
                catchError((error) => {
                    console.error('SignIn Error:', error);
                    return throwError(() => new Error('SignIn Failed'));
                })
            );
    }

    /**
     * Sign in using the access token
     */
    signInUsingToken(): Observable<any> {
        if (!this.refreshToken) {
            return of(false).pipe(
                switchMap(() => {
                    this.clearAuthData();
                    return throwError(
                        () => new Error('No refresh token available')
                    );
                })
            );
        }

        return this._httpClient
            .put(`${environment.apiUrl}/token/refresh`, null, {
                headers: new HttpHeaders({
                    Authorization: `Bearer ${this.refreshToken}`,
                    NO_HTTP_INTERCEPTOR: 'true',
                    'x-timezone':
                        Intl.DateTimeFormat().resolvedOptions().timeZone,
                }),
            })
            .pipe(
                switchMap((response: any) => {
                    if (!response.accessToken || !response.refreshToken) {
                        throw new Error('Invalid token refresh response');
                    }

                    this.accessToken = response.accessToken;
                    this.refreshToken = response.refreshToken;
                    this._authenticated = true;
                    this._userService.user = AuthUtils.decodeToken(
                        response.accessToken
                    );

                    return of(true);
                }),
                catchError((error) => {
                    console.error('SignInUsingToken Error:', error);
                    this.clearAuthData();
                    return of(false);
                })
            );
    }

    signInWithMicrosoft(microsoftToken: string): Observable<any> {
        return this._httpClient
            .post(`${environment.apiUrl}/sso/sign-in/microsoft`, {
                token: microsoftToken,
            })
            .pipe(
                switchMap((response: any) => {
                    if (!response.accessToken || !response.refreshToken) {
                        throw new Error(
                            'Invalid token response from Microsoft Auth'
                        );
                    }

                    this.accessToken = response.accessToken;
                    this.refreshToken = response.refreshToken;
                    this._authenticated = true;
                    this._userService.user = AuthUtils.decodeToken(
                        response.accessToken
                    );

                    return of(response);
                }),
                catchError((error) => {
                    console.error('Microsoft SignIn Error:', error);
                    return throwError(
                        () => new Error('Microsoft SignIn Failed')
                    );
                })
            );
    }

    /**
     * Sign out
     */
    signOut(): Observable<any> {
        this.clearAuthData();
        return of(true);
    }

    /**
     * Check the authentication status
     */
    /**
     * Check the authentication status
     */
    check(): Observable<boolean> {
        this._authMethod = localStorage.getItem('authMethod') as 'microsoft' | 'normal' | 'none';
        if (environment.microsoftAuthEnabled && this._authMethod === 'microsoft') {
            return from(this._ssoService.checkMSAL()).pipe(
                switchMap((tokenMsal) => {
                    // Si ya estamos autenticados con un token válido, devolvemos true directamente
                    if (this._authenticated && this.accessToken && !AuthUtils.isTokenExpired(this.accessToken)) {
                        return of(true);
                    }

                    // Intentar renovar el token si está expirado o no existe
                    if (!this.accessToken || AuthUtils.isTokenExpired(this.accessToken)) {
                        return this.signInUsingToken().pipe(
                            catchError(() => of(false))
                        );
                    }

                    // Si el usuario está autenticado con Microsoft pero no tiene token interno, generarlo
                    if (tokenMsal !== null) {
                        return this.signInWithMicrosoft(tokenMsal)
                            .pipe(
                                catchError(() => of(false))
                            );
                    }

                    return of(false);
                })
            );
        }
        else if (this._authMethod === 'normal') {
            if (this._authenticated) {
                return of(true);
            }

            if (!this.accessToken || AuthUtils.isTokenExpired(this.accessToken)) {
                this.clearAuthData();
                return of(false);
            }

            return this.signInUsingToken();
        }

        return of(false);
    } 

    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // ----------------------------------------------------------------------------------------------------

    /**
     * Clears authentication data
     */
    private clearAuthData(): void {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        this._authenticated = false;
    }
}
