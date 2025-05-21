import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
    BehaviorSubject,
    map,
    Observable,
    of,
    switchMap,
    take,
    tap,
    throwError,
} from 'rxjs';
import { environment } from 'environments/environment';
import { PaginatedApiResponse } from 'app/core/types/paginated-api-response.types';
import { Language } from 'app/core/types/languages.types';
import { IdentifierType } from 'app/core/types/identifiers.types';
import { User } from '../admin/users/users.types';
import { Role } from '../admin/roles/roles.types';
import { UserService } from 'app/core/user/user.service';

@Injectable({ providedIn: 'root' })
export class SettingsService {
    // Private
    private _user: BehaviorSubject<User | null> = new BehaviorSubject(null);
    private _languages: BehaviorSubject<Language[] | []> = new BehaviorSubject(
        []
    );
    private _identifierTypes: BehaviorSubject<IdentifierType[] | []> =
        new BehaviorSubject([]);
    private _roles: BehaviorSubject<Role[] | []> = new BehaviorSubject([]);
    /**
     * Constructor
     */
    constructor(
        private _httpClient: HttpClient,
        private _userService: UserService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for user
     */
    get user$(): Observable<User> {
        return this._user.asObservable();
    }

    /**
     * Getter for languages
     */
    get language$(): Observable<Language[]> {
        return this._languages.asObservable();
    }

    /**
     * Getter for languages
     */
    get identifierTypes$(): Observable<IdentifierType[]> {
        return this._identifierTypes.asObservable();
    }

    /**
     * Getter for languages
     */
    get roles$(): Observable<Role[]> {
        return this._roles.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get user profile
     */
    getUserProfile(): Observable<User> {
        return this._userService.user$.pipe(
            switchMap((user) =>
                this._httpClient.get<User>(
                    `${environment.apiUrl}/users/${user.uid}`
                )
            ),
            tap((user) => {
                this._user.next(user);
            })
        );
    }

    /**
     * Update user
     *
     * @param id
     * @param user
     */
    updateUser(id: number, user: User): Observable<User> {
        return this._httpClient
            .put<User>(`${environment.apiUrl}/users/${user.id}`, user)
            .pipe(
                map((_) => {
                    return user;
                }),
                switchMap(() => this.getUserProfile()),
                map((_) => {
                    this._user.next(user);
                    return user;
                })
            );
    }

    /**
     * Update password
     *
     * @param id
     * @param newPassword
     */
    updatePassword(
        email: string,
        newPasswordHash: string,
        oldPasswordHash: string
    ): Observable<boolean> {
        return this._httpClient
            .put<User>(`${environment.apiUrl}/users/password-user-from-user`, {
                email,
                newPasswordHash,
                oldPasswordHash,
            })
            .pipe(
                switchMap(() => this.getUserProfile()),
                map((_) => {
                    return true;
                })
            );
    }

    /**
     * Get languages
     */
    getLanguages(): Observable<Language[]> {
        return this._languages.getValue().length > 0
            ? this._languages.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(`${environment.apiUrl}/languages`)
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((languages) => {
                          this._languages.next(languages);
                      })
                  );
    }

    /**
     * Get identifier types
     */
    getIdentifierTypes(): Observable<IdentifierType[]> {
        return this._identifierTypes.getValue().length > 0
            ? this._identifierTypes.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(
                      `${environment.apiUrl}/identifiertypes`
                  )
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((identifierTypes) => {
                          this._identifierTypes.next(identifierTypes);
                      })
                  );
    }

    /**
     * Get roles
     */
    getRoles(): Observable<Role[]> {
        return this._roles.getValue().length > 0
            ? this._roles.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(
                      `${environment.apiUrl}/roles?Order.PropertyName=order&Order.Ascendant=true`
                  )
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((roles) => {
                          this._roles.next(roles);
                      })
                  );
    }
}
