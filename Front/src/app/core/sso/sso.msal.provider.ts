import { APP_INITIALIZER, EnvironmentProviders, inject, Provider } from "@angular/core";
import { MSAL_GUARD_CONFIG, MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MsalBroadcastService, MsalGuard, MsalInterceptor, MsalService } from "@azure/msal-angular";
import { MSALGuardConfigFactory, MSALInstanceFactory, MSALInterceptorConfigFactory } from "./sso.msal.config";
import { HTTP_INTERCEPTORS } from "@angular/common/http";

export const provideMSAL = (): Array<Provider | EnvironmentProviders> => {
    return [
        {
            provide: APP_INITIALIZER,
            useFactory: () => {
                inject(MsalService).initialize();
                return () => true;
            },
            multi: true,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: MsalInterceptor,
            multi: true
        },
        {
            provide: MSAL_INSTANCE,
            useFactory: MSALInstanceFactory
        },
        {
            provide: MSAL_GUARD_CONFIG,
            useFactory: MSALGuardConfigFactory
        },
        {
            provide: MSAL_INTERCEPTOR_CONFIG,
            useFactory: MSALInterceptorConfigFactory
        },
        MsalService,
        MsalGuard,
        MsalBroadcastService
    ];
};