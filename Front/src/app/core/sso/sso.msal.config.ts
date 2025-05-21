import { MsalGuardConfiguration, MsalInterceptorConfiguration } from "@azure/msal-angular";
import { BrowserCacheLocation, InteractionType, PublicClientApplication } from "@azure/msal-browser";
import { environment } from "environments/environment";

export function MSALInstanceFactory(): PublicClientApplication {
    return new PublicClientApplication({
        auth: {
            clientId: environment.clientId,
            authority: `https://login.microsoftonline.com/${environment.tenantId}`,
            redirectUri: '/',
            postLogoutRedirectUri: '/sign-in'
        },
        cache: {
            cacheLocation: BrowserCacheLocation.LocalStorage
        },
        system: {
            tokenRenewalOffsetSeconds: 300
        }
    });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
    protectedResourceMap.set('https://graph.microsoft.com/v1.0/me', ['user.read']);
    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap
    };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
    return {
        interactionType: InteractionType.Redirect,
        authRequest: {
            scopes: ['user.read'],
            prompt: 'select_account'
        },
        loginFailedRoute: '/sign-out'
    };
}