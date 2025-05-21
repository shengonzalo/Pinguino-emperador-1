import { Routes } from '@angular/router';
import { PermissionsComponent } from './permissions.component';
import { PermissionsService } from './permissions.service';
import { inject } from '@angular/core';
import { ResourcePermissionsService } from 'app/core/resource-permissions/resource-permissions.service';

export default [
    {
        path: '',
        component: PermissionsComponent,
        resolve: {
            resourcePermissions: () =>
                inject(ResourcePermissionsService).getResourcePermissions([
                    '300',
                ]),
            permissions: () => inject(PermissionsService).getPermissions(),
            roles: () => inject(PermissionsService).getRoles(),
        },
    },
] as Routes;
