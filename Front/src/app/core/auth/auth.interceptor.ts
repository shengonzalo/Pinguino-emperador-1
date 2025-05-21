import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandlerFn,
    HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from 'app/core/auth/auth.service';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { catchError, Observable, throwError } from 'rxjs';
import { OgaConfirmationService } from '@oga/services/confirmation';
/**
 * Intercept
 *
 * @param req
 * @param next
 */
export const authInterceptor = (
    req: HttpRequest<unknown>,
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
    const authService = inject(AuthService);
    const ogaConfirmationService = inject(OgaConfirmationService);

    // Clone the request object
    let newReq = req.clone();

    // Request
    //
    // If the access token didn't expire, add the Authorization header.
    // We won't add the Authorization header if the access token expired.
    // This will force the server to return a "401 Unauthorized" response
    // for the protected API routes which our response interceptor will
    // catch and delete the access token from the local storage while logging
    // the user out from the app.
    if (
        authService.accessToken &&
        !AuthUtils.isTokenExpired(authService.accessToken)
    ) {
        newReq = req.clone({
            headers: req.headers
                .set('Authorization', 'Bearer ' + authService.accessToken)
                .set('x-timezone', Intl.DateTimeFormat().resolvedOptions().timeZone)
        });
    }

    // Response
    return next(newReq).pipe(
        catchError((error) => {
            // Catch "401 Unauthorized" responses
            if (error instanceof HttpErrorResponse && error.status === 401) {
                // Sign out
                authService.signOut();

                // Reload the app
                location.reload();
            } else {
                // Handle other errors by opening a dialog
                ogaConfirmationService.open({
                    title: 'actions.error',
                    message: `errorCodes.${
                        error.error.customCodes?.length
                            ? error.error.customCodes[0]
                            : 'UNCONTROLLED_ERROR'
                    }`,
                    actions: {
                        confirm: {
                            label: 'actions.accept',
                        },
                        cancel: {
                            show: false,
                        },
                    },
                });
            }

            return throwError(error);
        })
    );
};
