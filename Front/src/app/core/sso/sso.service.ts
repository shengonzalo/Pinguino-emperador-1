import { Injectable } from "@angular/core";
import { MsalService } from "@azure/msal-angular";
import { MSALGuardConfigFactory } from "./sso.msal.config";
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SsoService {

    constructor(
        private readonly _msalService: MsalService
    ) {

    }

    initializeMSAL() {
        this._msalService.instance.handleRedirectPromise().then((response) => {
            if (response && response.account) {
                this._msalService.instance.setActiveAccount(response.account);
            }
        }).catch((error) => {
            console.error(error);
        });
    }

    /**
    * Sign in
    */
    async signInMSAL(): Promise<string | null> {
        try {
            const request: any = MSALGuardConfigFactory().authRequest;
            const loginResponse = await firstValueFrom(this._msalService.loginPopup(request));

            if (loginResponse && loginResponse.account) {
                this._msalService.instance.setActiveAccount(loginResponse.account);

                const tokenResponse = await this._msalService.instance.acquireTokenSilent({
                    account: loginResponse.account,
                    scopes: request.scopes,
                });

                return tokenResponse.accessToken;
            }
        } catch (error) {
            console.error("Error al autenticar con Microsoft: ", error);
            return null;
        }
    }

    /**
    * Sign out
    */
    signOutMSAL() {
        this._msalService.logout();
    }

    /**
    * Check the authentication status
    */
    async checkMSAL(): Promise<string | null> {
        try {
            // Intentar obtener la cuenta activa de MSAL
            const account = this._msalService.instance.getActiveAccount();

            // Si hay una cuenta activa, intentar obtener el token silenciosamente
            if (account) {
                // Intentar obtener el token sin interacción
                const request: any = MSALGuardConfigFactory().authRequest;

                await this._msalService.instance.initialize();
                const tokenResponse = await this._msalService.instance.acquireTokenSilent({
                    account: account,
                    scopes: request.scopes
                });

                // Si obtenemos el token correctamente, configuramos la cuenta activa
                if (tokenResponse && tokenResponse.accessToken) {
                    this._msalService.instance.setActiveAccount(account);
                    return tokenResponse.accessToken;
                }
            }

            // Si no hay una cuenta activa o no podemos obtener el token silenciosamente, retornar false
            return null;
        } 
        catch (error) {
            console.error('Error en checkMSAL: ', error);
            return null;
        }
    }
}