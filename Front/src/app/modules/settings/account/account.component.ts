import { TextFieldModule } from '@angular/cdk/text-field';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import {
    FormsModule,
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { RouterLink } from '@angular/router';
import { TranslocoModule, TranslocoService } from '@ngneat/transloco';
import { SettingsService } from '../settings.service';
import { User } from 'app/modules/admin/users/users.types';
import { Language } from 'app/core/types/languages.types';
import { IdentifierType } from 'app/core/types/identifiers.types';
import { Role } from 'app/modules/admin/roles/roles.types';
import { Subject, takeUntil } from 'rxjs';
import { NgFor } from '@angular/common';
import { MatTooltip } from '@angular/material/tooltip';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
    selector: 'settings-account',
    templateUrl: './account.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        TextFieldModule,
        MatSelectModule,
        MatOptionModule,
        MatButtonModule,
        TranslocoModule,
        RouterLink,
        NgFor,
        MatTooltip,
    ],
})
export class SettingsAccountComponent implements OnInit {
    accountForm: UntypedFormGroup;
    user: User;
    languages: Language[];
    identifierTypes: IdentifierType[];
    roles: Role[];

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
        this.accountForm = this._formBuilder.group({
            id: [''],
            name: ['', [Validators.required]],
            surname: ['', [Validators.required]],
            secondSurname: [''],
            email: ['', [Validators.required, Validators.email]],
            identifierTypeId: [''],
            identifier: [''],
            phoneNumber: [''],
            languageId: ['', [Validators.required]],
            password: [''],
            confirmPassword: [''],
            passwordHash: [''],
            roles: [{ value: '', disabled: true }, [Validators.required]],
            activeChk: [{ value: '', disabled: true }, [Validators.required]],
        });

        // Get the user
        this._settingsService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) => {
                // Get the user
                this.user = user;
                // Fill the form values with a conditional check for roles
                const values = {
                    ...user,
                    roles: user.roles?.length ? user.roles[0].id : null,
                };
                this.accountForm.patchValue(values);
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this._settingsService.language$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((languages: Language[]) => {
                this.languages = languages;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this._settingsService.identifierTypes$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((identifierTypes: IdentifierType[]) => {
                this.identifierTypes = identifierTypes;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this._settingsService.roles$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((roles: Role[]) => {
                this.roles = roles;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Update the user
     */
    saveUser(): void {
        // Get the user object
        const user = this.accountForm.getRawValue();

        // Assign the user's roles by mapping the roles ID from the form value to an array of objects
        user.roles = [user.roles].map((id: string) => ({ id }));

        this._settingsService.updateUser(user.id, user).subscribe(() => {
            this._snackBar.open(
                this._translocoService.translate('actions.information-updated'),
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
