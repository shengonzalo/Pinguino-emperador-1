import { inject } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    Router,
    RouterStateSnapshot,
    Routes,
} from '@angular/router';
import { RolesComponent } from 'app/modules/admin/roles/roles.component';
import { RolesService } from 'app/modules/admin/roles/roles.service';
import { catchError, throwError } from 'rxjs';
import { RolesDetailsComponent } from './details/details.component';
import { RolesListComponent } from './list/list.component';
import { ResourcePermissionsService } from 'app/core/resource-permissions/resource-permissions.service';

/**
 * Role resolver
 *
 * @param route
 * @param state
 */
const roleResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const rolesService = inject(RolesService);
    const router = inject(Router);

    return rolesService.getRoleById(Number(route.paramMap.get('id'))).pipe(
        // Error here means the requested role is not available
        catchError((error) => {
            // Log the error
            console.error(error);

            // Get the parent url
            const parentUrl = state.url.split('/').slice(0, -1).join('/');

            // Navigate to there
            router.navigateByUrl(parentUrl);

            // Throw an error
            return throwError(error);
        })
    );
};

/**
 * Can deactivate roles details
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */
const canDeactivateRolesDetails = (
    component: RolesDetailsComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot
) => {
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;
    while (nextRoute.firstChild) {
        nextRoute = nextRoute.firstChild;
    }

    // If the next state doesn't contain '/roles'
    // it means we are navigating away from the
    // roles app
    if (!nextState.url.includes('/roles')) {
        // Let it navigate
        return true;
    }

    // If we are navigating to another role...
    if (nextRoute.paramMap.get('id')) {
        // Just navigate
        return true;
    }

    // Otherwise, close the drawer first, and then navigate
    return component.closeDrawer().then(() => true);
};

export default [
    {
        path: '',
        component: RolesComponent,
        resolve: {
            resourcePermissions: () =>
                inject(ResourcePermissionsService).getResourcePermissions([
                    '200',
                ]),
        },
        children: [
            {
                path: '',
                component: RolesListComponent,
                resolve: {
                    roles: () => inject(RolesService).getRoles(),
                },
                children: [
                    {
                        path: ':id',
                        component: RolesDetailsComponent,
                        resolve: {
                            role: roleResolver,
                            roles: () => inject(RolesService).getRoles(),
                        },
                        canDeactivate: [canDeactivateRolesDetails],
                    },
                ],
            },
        ],
    },
] as Routes;
