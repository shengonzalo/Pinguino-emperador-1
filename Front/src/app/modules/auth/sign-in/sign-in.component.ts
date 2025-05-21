import { NgIf } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import {
    FormsModule,
    NgForm,
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TranslocoModule, TranslocoService } from '@ngneat/transloco';
import { ogaAnimations } from '@oga/animations';
import { OgaAlertComponent, OgaAlertType } from '@oga/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { SsoService } from 'app/core/sso/sso.service';
import { environment } from 'environments/environment';

@Component({
    selector: 'auth-sign-in',
    templateUrl: './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: ogaAnimations,
    standalone: true,
    imports: [
        RouterLink,
        OgaAlertComponent,
        NgIf,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatCheckboxModule,
        MatProgressSpinnerModule,
        TranslocoModule
    ],
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;

    alert: { type: OgaAlertType; message: string } = {
        type: 'success',
        message: '',
    };
    signInForm: UntypedFormGroup;
    showAlert: boolean = false;
    microsoftAuthEnabled: boolean = false;
    isSigningInWithMicrosoft: boolean = false;

    /**
     * Constructor
     */
    constructor(
        private readonly _activatedRoute: ActivatedRoute,
        private readonly _authService: AuthService,
        private readonly _formBuilder: UntypedFormBuilder,
        private readonly _router: Router,
        private readonly _translocoService: TranslocoService,
        private readonly _ssoService: SsoService
    ) { }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.microsoftAuthEnabled = environment.microsoftAuthEnabled;
        
        // Create the form
        this.signInForm = this._formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', Validators.required],
            rememberMe: [''],
        });

        this._ssoService.initializeMSAL();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Sign in
     */
    signIn(): void {
        // Return if the form is invalid
        if (this.signInForm.invalid) {
            return;
        }

        // Disable the form
        this.signInForm.disable();

        // Hide the alert
        this.showAlert = false;

        // Sign in
        this._authService.signIn(this.signInForm.value).subscribe(
            () => {
                localStorage.setItem('authMethod', 'normal');

                // Set the redirect url.
                // The '/signed-in-redirect' is a dummy url to catch the request and redirect the user
                // to the correct page after a successful sign in. This way, that url can be set via
                // routing file and we don't have to touch here.
                const redirectURL =
                    this._activatedRoute.snapshot.queryParamMap.get(
                        'redirectURL'
                    ) || '/signed-in-redirect';

                // Navigate to the redirect url
                this._router.navigateByUrl(redirectURL);
            },
            (response) => {
                // Re-enable the form
                this.signInForm.enable();

                // Reset the form
                this.signInNgForm.resetForm();

                // Set the alert
                this.alert = {
                    type: 'error',
                    message: this._translocoService.translate(
                        'sign-in.wrong-email-or-password'
                    ),
                };

                // Show the alert
                this.showAlert = true;
            }
        );
    }

    /**
    * Sign in with MSAL
    */
    async signInMSAL(): Promise<void> {
        this.isSigningInWithMicrosoft = true;

        try {
            // Obtener el token de Microsoft
            const microsoftToken = await this._ssoService.signInMSAL();
    
            if (!microsoftToken) {
                this.alert = {
                    type: 'error',
                    message: this._translocoService.translate('sign-in.failed-microsoft-auth'),
                };
                this.showAlert = true;
                return;
            }
    
            // Enviar el token de Microsoft al backend
            this._authService.signInWithMicrosoft(microsoftToken).subscribe(
                (response) => {
                    // Guardar el token interno en localStorage
                    localStorage.setItem('accessToken', response.accessToken);
                    localStorage.setItem('refreshToken', response.refreshToken);

                    localStorage.setItem('authMethod', 'microsoft');
                    
                    // Redirigir al usuario a la página principal
                    this._router.navigateByUrl('/signed-in-redirect');
                },
                (error) => {
                    this.alert = {
                        type: 'error',
                        message: this._translocoService.translate('sign-in.failed-microsoft-auth'),
                    };
                    this.showAlert = true;
                }
            );
        } 
        catch (error) {
            console.error("Error en la autenticación con Microsoft:", error);
            this.alert = {
                type: 'error',
                message: 'Error al autenticar con Microsoft',
            };
            this.showAlert = true;
        }
        finally{
            this.isSigningInWithMicrosoft = false;
        }
    }
    
}
