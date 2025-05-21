import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { APP_INITIALIZER, ENVIRONMENT_INITIALIZER, EnvironmentProviders, importProvidersFrom, inject, Provider } from '@angular/core';
import { MATERIAL_SANITY_CHECKS } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { OgaConfig } from '@oga/services/config';
import { OGA_CONFIG } from '@oga/services/config/config.constants';
import { OgaConfirmationService } from '@oga/services/confirmation';
import { ogaLoadingInterceptor, OgaLoadingService } from '@oga/services/loading';
import { OgaMediaWatcherService } from '@oga/services/media-watcher';
import { OgaPlatformService } from '@oga/services/platform';
import { OgaSplashScreenService } from '@oga/services/splash-screen';
import { OgaUtilsService } from '@oga/services/utils';

export type OgaProviderConfig = {
    mockApi?: {
        delay?: number;
        services?: any[];
    },
    oga?: OgaConfig
}

/**
 * Oga provider
 */
export const provideOga = (config: OgaProviderConfig): Array<Provider | EnvironmentProviders> =>
{
    // Base providers
    const providers: Array<Provider | EnvironmentProviders> = [
        {
            // Disable 'theme' sanity check
            provide : MATERIAL_SANITY_CHECKS,
            useValue: {
                doctype: true,
                theme  : false,
                version: true,
            },
        },
        {
            // Use the 'fill' appearance on Angular Material form fields by default
            provide : MAT_FORM_FIELD_DEFAULT_OPTIONS,
            useValue: {
                appearance: 'fill',
            },
        },
        {
            provide : OGA_CONFIG,
            useValue: config?.oga ?? {},
        },

        importProvidersFrom(MatDialogModule),
        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaConfirmationService),
            multi   : true,
        },

        provideHttpClient(withInterceptors([ogaLoadingInterceptor])),
        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaLoadingService),
            multi   : true,
        },

        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaMediaWatcherService),
            multi   : true,
        },
        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaPlatformService),
            multi   : true,
        },
        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaSplashScreenService),
            multi   : true,
        },
        {
            provide : ENVIRONMENT_INITIALIZER,
            useValue: () => inject(OgaUtilsService),
            multi   : true,
        },
    ];

    // Return the providers
    return providers;
};
