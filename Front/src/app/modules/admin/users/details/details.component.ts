import { Overlay } from '@angular/cdk/overlay';
import { TextFieldModule } from '@angular/cdk/text-field';
import { DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    Renderer2,
    ViewContainerRef,
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
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDrawerToggleResult } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { OgaFindByKeyPipe } from '@oga/pipes/find-by-key/find-by-key.pipe';
import { OgaConfirmationService } from '@oga/services/confirmation';
import { UsersService } from 'app/modules/admin/users/users.service';
import { User } from 'app/modules/admin/users/users.types';
import { Subject, takeUntil } from 'rxjs';
import { UsersListComponent } from '../list/list.component';
import { TranslocoModule } from '@ngneat/transloco';
import { Language } from 'app/core/types/languages.types';
import { IdentifierType } from 'app/core/types/identifiers.types';
import { Role } from '../../roles/roles.types';
import { HasPermissionDirective } from 'app/core/resource-permissions/resource-permissions.directive';

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
    selector: 'users-details',
    templateUrl: './details.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        NgIf,
        MatButtonModule,
        MatTooltipModule,
        RouterLink,
        MatIconModule,
        NgFor,
        FormsModule,
        ReactiveFormsModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatCheckboxModule,
        NgClass,
        MatSelectModule,
        MatOptionModule,
        MatDatepickerModule,
        TextFieldModule,
        OgaFindByKeyPipe,
        DatePipe,
        TranslocoModule,
        HasPermissionDirective
    ],
})
export class UsersDetailsComponent implements OnInit, OnDestroy {
    editMode: boolean = false;
    user: User;
    userForm: UntypedFormGroup;
    users: User[];
    languages: Language[];
    identifierTypes: IdentifierType[];
    roles: Role[];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        public usersService: UsersService,
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _usersListComponent: UsersListComponent,
        private _formBuilder: UntypedFormBuilder,
        private _ogaConfirmationService: OgaConfirmationService,
        private _router: Router
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Open the drawer
        this._usersListComponent.matDrawer.open();

        // Create the user form
        this.userForm = this._formBuilder.group({
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
            roles: ['', [Validators.required]],
            activeChk: ['', [Validators.required]],
        });

        // Get the users
        this.usersService.users$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((users: User[]) => {
                this.users = users;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the user
        this.usersService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) => {
                // Open the drawer if it's closed
                this._usersListComponent.matDrawer.open();

                // Get the user
                this.user = user;

                // Fill the form values with a conditional check for roles
                const values = {
                    ...user,
                    roles: user.roles?.length ? user.roles[0].id : null,
                };
                this.userForm.patchValue(values);

                if (user.id === 0) {
                    this.toggleEditMode(true); // Activate edit mode if the ID is 0
                    // Add required validations to password and confirm password
                    this.userForm
                        .get('password')
                        .setValidators([Validators.required]);
                    this.userForm
                        .get('confirmPassword')
                        .setValidators([
                            Validators.required,
                            matchValidator('password'),
                        ]);
                    this.userForm.get('password').updateValueAndValidity();
                    this.userForm
                        .get('confirmPassword')
                        .updateValueAndValidity();
                } else {
                    this.toggleEditMode(false); // Deactivate edit mode otherwise
                }

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this.usersService.language$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((languages: Language[]) => {
                this.languages = languages;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this.usersService.identifierTypes$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((identifierTypes: IdentifierType[]) => {
                this.identifierTypes = identifierTypes;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this.usersService.roles$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((roles: Role[]) => {
                this.roles = roles;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Close the drawer
     */
    closeDrawer(): Promise<MatDrawerToggleResult> {
        return this._usersListComponent.matDrawer.close();
    }

    /**
     * Toggle edit mode
     *
     * @param editMode
     */
    toggleEditMode(editMode: boolean | null = null): void {
        if (editMode === null) {
            this.editMode = !this.editMode;
        } else {
            this.editMode = editMode;
        }

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    /**
     * Update the user
     */
    saveUser(): void {
        // Get the user object
        const user = this.userForm.getRawValue();

        // Assign the user's roles by mapping the roles ID from the form value to an array of objects
        user.roles = [this.userForm.value.roles].map((id: string) => ({ id }));

        if (user.id === 0) {
            delete user.id;
            user.passwordHash = btoa(user.password);
            // Update the user on the server
            this.usersService.createUser(user).subscribe(() => {
                // Toggle the edit mode off
                this._router.navigate(['/users']);
                this.toggleEditMode(false);
            });
        } else {
            // Update the user on the server
            this.usersService.updateUser(user.id, user).subscribe(() => {
                // Toggle the edit mode off
                this.toggleEditMode(false);
            });
        }
    }

    /**
     * Delete the user
     */
    deleteUser(): void {
        // Open the confirmation dialog
        const confirmation = this._ogaConfirmationService.open({
            title: 'user.delete-user',
            message: 'user.delete-user-confirmation',
            actions: {
                confirm: {
                    label: 'actions.delete',
                },
            },
        });

        // Subscribe to the confirmation dialog closed action
        confirmation.afterClosed().subscribe((result) => {
            // If the confirm button pressed...
            if (result === 'confirmed') {
                // Get the current user's id
                const id = this.user.id;

                // Get the next/previous user's id
                const currentUserIndex = this.users.findIndex(
                    (item) => item.id === id
                );
                const nextUserIndex =
                    currentUserIndex +
                    (currentUserIndex === this.users.length - 1 ? -1 : 1);
                const nextUserId =
                    this.users.length === 1 && this.users[0].id === id
                        ? null
                        : this.users[nextUserIndex].id;

                // Delete the user
                this.usersService.deleteUser(id).subscribe((isDeleted) => {
                    // Return if the user wasn't deleted...
                    if (!isDeleted) {
                        return;
                    }

                    // Navigate to the next user if available
                    if (nextUserId) {
                        this._router.navigate(['../', nextUserId], {
                            relativeTo: this._activatedRoute,
                        });
                    }
                    // Otherwise, navigate to the parent
                    else {
                        this._router.navigate(['../'], {
                            relativeTo: this._activatedRoute,
                        });
                    }

                    // Toggle the edit mode off
                    this.toggleEditMode(false);
                });

                // Mark for check
                this._changeDetectorRef.markForCheck();
            }
        });
    }
}
