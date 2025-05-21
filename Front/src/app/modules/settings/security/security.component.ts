import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import {
    AbstractControl,
    FormsModule,
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    ValidationErrors,
    ValidatorFn,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TranslocoModule, TranslocoService } from '@ngneat/transloco';
import { SettingsService } from '../settings.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject, takeUntil } from 'rxjs';
import { User } from 'app/modules/admin/users/users.types';
import { MatTooltip } from '@angular/material/tooltip';

export function matchValidator(
    matchTo: string,
    reverse?: boolean
): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (control.parent && reverse) {
            const c = (control.parent?.controls as any)[matchTo];
            if (c) {
                c.updateValueAndValidity();
            }
            return null;
        }
        return !!control.parent &&
            !!control.parent.value &&
            control.value === (control.parent?.controls as any)[matchTo].value
            ? null
            : { matching: true };
    };
}

@Component({
    selector: 'settings-security',
    templateUrl: './security.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSlideToggleModule,
        MatButtonModule,
        TranslocoModule,
        MatTooltip,
    ],
})
export class SettingsSecurityComponent implements OnInit {
    securityForm: UntypedFormGroup;
    user: User;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _formBuilder: UntypedFormBuilder,
        private _settingsService: SettingsService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _snackBar: MatSnackBar,
        private _translocoService: TranslocoService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Create the form
        this.securityForm = this._formBuilder.group({
            email: [''],
            oldPasswordHash: ['', [Validators.required]],
            newPasswordHash: ['', [Validators.required]],
            confirmPassword: [
                null,
                [Validators.required, matchValidator('newPasswordHash')],
            ],
        });

        // Get the user
        this._settingsService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) => {
                // Get the user
                this.user = user;

                this.securityForm.patchValue(this.user);
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Change the password
     */
    changePassword(): void {
        const passwordChange = this.securityForm.getRawValue();

        passwordChange.newPasswordHash = btoa(passwordChange.newPasswordHash);
        passwordChange.oldPasswordHash = btoa(passwordChange.oldPasswordHash);
        delete passwordChange.confirmPassword;

        this._settingsService
            .updatePassword(
                passwordChange.email,
                passwordChange.newPasswordHash,
                passwordChange.oldPasswordHash
            )
            .subscribe(() => {
                this._snackBar.open(
                    this._translocoService.translate(
                        'actions.information-updated'
                    ),
                    '',
                    {
                        duration: 3000,
                        horizontalPosition: 'right',
                        verticalPosition: 'top',
                    }
                );
            });
    }
}
