import { Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { SettingsService } from './settings.service';
import { inject } from '@angular/core';

export default [
    {
        path: '',
        component: SettingsComponent,
        resolve: {
            users: () => inject(SettingsService).getUserProfile(),
            languages: () => inject(SettingsService).getLanguages(),
            roles: () => inject(SettingsService).getRoles(),
            identifierTypes: () => inject(SettingsService).getIdentifierTypes(),
        },
    },
] as Routes;
