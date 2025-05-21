import { inject } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    Router,
    RouterStateSnapshot,
    Routes,
} from '@angular/router';
import { UsersComponent } from 'app/modules/admin/users/users.component';
import { UsersService } from 'app/modules/admin/users/users.service';
import { catchError, throwError } from 'rxjs';
import { UsersDetailsComponent } from './details/details.component';
import { UsersListComponent } from './list/list.component';
import { ResourcePermissionsService } from 'app/core/resource-permissions/resource-permissions.service';

/**
 * User resolver
 *
 * @param route
 * @param state
 */
const userResolver = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    const usersService = inject(UsersService);
    const router = inject(Router);

    return usersService.getUserById(Number(route.paramMap.get('id'))).pipe(
        // Error here means the requested user is not available
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
 * Can deactivate users details
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */
const canDeactivateUsersDetails = (
    component: UsersDetailsComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot
) => {
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;
    while (nextRoute.firstChild) {
        nextRoute = nextRoute.firstChild;
    }

    // If the next state doesn't contain '/users'
    // it means we are navigating away from the
    // users app
    if (!nextState.url.includes('/users')) {
        // Let it navigate
        return true;
    }

    // If we are navigating to another user...
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
        component: UsersComponent,
        resolve: {
            resourcePermissions: () =>
                inject(ResourcePermissionsService).getResourcePermissions([
                    '100',
                ]),
        },
        children: [
            {
                path: '',
                component: UsersListComponent,
                resolve: {
                    users: () => inject(UsersService).getUsers(),
                    languages: () => inject(UsersService).getLanguages(),
                    roles: () => inject(UsersService).getRoles(),
                    identifierTypes: () =>
                        inject(UsersService).getIdentifierTypes(),
                },
                children: [
                    {
                        path: ':id',
                        component: UsersDetailsComponent,
                        resolve: {
                            user: userResolver,
                            languages: () =>
                                inject(UsersService).getLanguages(),
                            roles: () => inject(UsersService).getRoles(),
                            identifierTypes: () =>
                                inject(UsersService).getIdentifierTypes(),
                        },
                        canDeactivate: [canDeactivateUsersDetails],
                    },
                ],
            },
        ],
    },
] as Routes;
