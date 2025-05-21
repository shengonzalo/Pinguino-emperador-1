import { TextFieldModule } from '@angular/cdk/text-field';
import { DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
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
import { RolesService } from 'app/modules/admin/roles/roles.service';
import { Subject, takeUntil } from 'rxjs';
import { RolesListComponent } from '../list/list.component';
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
    selector: 'roles-details',
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
        HasPermissionDirective,
    ],
})
export class RolesDetailsComponent implements OnInit, OnDestroy {
    editMode: boolean = false;
    role: Role;
    roleForm: UntypedFormGroup;
    roles: Role[];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        public rolesService: RolesService,
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _rolesListComponent: RolesListComponent,
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
        this._rolesListComponent.matDrawer.open();

        // Create the role form
        this.roleForm = this._formBuilder.group({
            id: [''],
            name: [null, [Validators.required]],
            description: [null, [Validators.required]],
            order: [null, [Validators.required]],
        });

        // Get the roles
        this.rolesService.roles$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((roles: Role[]) => {
                this.roles = roles;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the role
        this.rolesService.role$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((role: Role) => {
                // Open the drawer if it's closed
                this._rolesListComponent.matDrawer.open();

                // Get the role
                this.role = role;
                this.roleForm.patchValue(role);

                if (role.id === 0) {
                    this.toggleEditMode(true); // Activate edit mode if the ID is 0
                } else {
                    this.toggleEditMode(false); // Deactivate edit mode otherwise
                }

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
        return this._rolesListComponent.matDrawer.close();
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
     * Update the role
     */
    saveRole(): void {
        // Get the role object
        const role = this.roleForm.getRawValue();

        if (role.id === 0) {
            delete role.id;
            // Update the role on the server
            this.rolesService.createRole(role).subscribe(() => {
                // Toggle the edit mode off
                this._router.navigate(['/roles']);
                this.toggleEditMode(false);
            });
        } else {
            // Update the role on the server
            this.rolesService.updateRole(role.id, role).subscribe(() => {
                // Toggle the edit mode off
                this.toggleEditMode(false);
            });
        }
    }

    /**
     * Delete the role
     */
    deleteRole(): void {
        // Open the confirmation dialog
        const confirmation = this._ogaConfirmationService.open({
            title: 'role.delete-role',
            message: 'role.delete-role-confirmation',
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
                // Get the current role's id
                const id = this.role.id;

                // Get the next/previous role's id
                const currentRoleIndex = this.roles.findIndex(
                    (item) => item.id === id
                );
                const nextRoleIndex =
                    currentRoleIndex +
                    (currentRoleIndex === this.roles.length - 1 ? -1 : 1);
                const nextRoleId =
                    this.roles.length === 1 && this.roles[0].id === id
                        ? null
                        : this.roles[nextRoleIndex].id;

                // Delete the role
                this.rolesService.deleteRole(id).subscribe((isDeleted) => {
                    // Return if the role wasn't deleted...
                    if (!isDeleted) {
                        return;
                    }

                    // Navigate to the next role if available
                    if (nextRoleId) {
                        this._router.navigate(['../', nextRoleId], {
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
