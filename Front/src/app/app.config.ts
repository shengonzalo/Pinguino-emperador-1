import { provideHttpClient } from '@angular/common/http';
import { APP_INITIALIZER, ApplicationConfig, inject } from '@angular/core';
import { MAT_LUXON_DATE_ADAPTER_OPTIONS, MAT_LUXON_DATE_FORMATS } from '@angular/material-luxon-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { PreloadAllModules, provideRouter, withInMemoryScrolling, withPreloading } from '@angular/router';
import { provideOga } from '@oga';
import { provideTransloco, TranslocoService } from '@ngneat/transloco';
import { firstValueFrom } from 'rxjs';
import { appRoutes } from 'app/app.routes';
import { provideAuth } from 'app/core/auth/auth.provider';
import { provideIcons } from 'app/core/icons/icons.provider';
import { TranslocoHttpLoader } from './core/transloco/transloco.http-loader';
import { provideMSAL } from './core/sso/sso.msal.provider';
import { IMAGE_CONFIG } from '@angular/common';
import { MY_DATE_FORMAT } from './shared/helpers/date-format';
import { CustomDateAdapter } from './shared/providers/custom-date_adapter';

export const appConfig: ApplicationConfig = {
    providers: [
        provideAnimations(),
        provideHttpClient(),
        provideRouter(appRoutes,
            withPreloading(PreloadAllModules),
            withInMemoryScrolling({ scrollPositionRestoration: 'enabled' }),
        ),

        /**
         * Material Date Adapter configuration.
         */
        {
            provide: MAT_DATE_LOCALE,
            useValue: 'es-ES',
        },
        {
            provide: MAT_LUXON_DATE_FORMATS,
            useValue: MY_DATE_FORMAT,
        },
        {
            provide: MAT_DATE_FORMATS,
            useValue: MAT_LUXON_DATE_FORMATS,
        },
        {
            provide: DateAdapter,
            useClass: CustomDateAdapter,
            deps: [
                TranslocoService,
                MAT_DATE_LOCALE,
                MAT_LUXON_DATE_ADAPTER_OPTIONS,
            ],
        },
        {
            provide: IMAGE_CONFIG,
            useValue: {
                disableImageSizeWarning: true,
                disableImageLazyLoadWarning: true,
            },
        },

        // Transloco Config
        provideTransloco({
            config: {
                availableLangs: [
                    {
                        id: 'es',
                        label: 'Español',
                    },
                    {
                        id: 'en',
                        label: 'English',
                    },
                ],
                defaultLang: 'es',
                fallbackLang: 'es',
                reRenderOnLangChange: true,
                prodMode: true,
            },
            loader: TranslocoHttpLoader,
        }),
        {
            // Preload the default language before the app starts to prevent empty/jumping content
            provide: APP_INITIALIZER,
            useFactory: () => {
                const translocoService = inject(TranslocoService);
                const defaultLang = translocoService.getDefaultLang();
                translocoService.setActiveLang(defaultLang);
                return () => firstValueFrom(translocoService.load(defaultLang));
            },
            multi: true,
        },

        // Oga
        provideMSAL(),
        provideAuth(),
        provideIcons(),
        provideOga({
            oga: {
                layout: 'classic',
                scheme: 'light',
                screens: {
                    sm: '600px',
                    md: '960px',
                    lg: '1280px',
                    xl: '1440px',
                },
                theme: 'theme-oga',
                themes: [
                    {
                        id: 'theme-oga',
                        name: 'OGA',
                    },
                ],
            },
        }),
    ],
};
