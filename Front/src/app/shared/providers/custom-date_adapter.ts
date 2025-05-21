import { Injectable, Inject } from '@angular/core';
import { LuxonDateAdapter, MAT_LUXON_DATE_ADAPTER_OPTIONS } from '@angular/material-luxon-adapter';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { TranslocoService } from '@ngneat/transloco';

@Injectable()
export class CustomDateAdapter extends LuxonDateAdapter {

    constructor(
        private _translate: TranslocoService,
        @Inject(MAT_DATE_LOCALE) dateLocale: string,
        @Inject(MAT_LUXON_DATE_ADAPTER_OPTIONS) options?: any
    ) {
        let currentLang = localStorage.getItem('language');
        dateLocale = currentLang ? (currentLang == 'es' ? 'es-ES' : 'en-US') : dateLocale;

        super(dateLocale, options);

        this._translate.langChanges$
            .subscribe(
                (lang: string) => {
                    this.setLocale(lang == 'es' ? 'es-ES' : 'en-US');
                }
            );
    }
}