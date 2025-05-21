import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnChanges,
    OnDestroy,
    OnInit,
    SimpleChanges,
    ViewEncapsulation,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TranslocoModule, TranslocoService } from '@ngneat/transloco';
import { Role } from 'app/modules/admin/roles/roles.types';
import { PermissionsService } from '../permissions.service';
import { Subject, takeUntil } from 'rxjs';
import { NgForOf } from '@angular/common';
import { Permission, RolePermissionGroup } from '../permissions.types';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'settings-permissions-group-configuration',
    templateUrl: './permissions-group-configuration.component.html',
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
        NgForOf,
        MatTooltipModule,
        RouterLink,
    ],
})
export class PermissionsGroupConfigurationComponent
    implements OnInit, OnDestroy, OnChanges
{
    @Input() id: number;

    roles: Role[];

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _permissionsService: PermissionsService,
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
        this.loadData();
    }

    /**
     * On changes
     */
    ngOnChanges(changes: SimpleChanges): void {
        if (changes.id) {
            this.loadData();
        }
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    /**
     * Loads the data for the permissions group configuration.
     * It fetches the permissions and roles from the service, and updates the roles
     * with a 'selected' property to indicate if they are part of the current group.
     */
    loadData(): void {
        this._permissionsService.permission$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((permissions: Permission[]) => {
                // Find the current group using the component's id
                const currentGroup = permissions.find((p) => p.id === this.id);
                if (currentGroup) {
                    // Create a set of active role IDs from the current group
                    const activeRoleIds = new Set(
                        currentGroup.rolePermissionGroup.map(
                            (role) => role.roleId
                        )
                    );
                    // Fetch roles and update their 'selected' status based on the active role IDs
                    this._permissionsService.roles$
                        .pipe(takeUntil(this._unsubscribeAll))
                        .subscribe((roles: Role[]) => {
                            this.roles = roles.map((role) => ({
                                ...role,
                                selected: activeRoleIds.has(role.id),
                            }));
                            // Trigger change detection to update the view
                            this._changeDetectorRef.markForCheck();
                        });
                }
            });
    }

    /**
     * Toggles the selection state of a role and updates the role permission group accordingly.
     *
     * @param role The role object with an optional selected property indicating its current state.
     */
    toggleRole(role: Role & { selected?: boolean }): void {
        const rolePermissionGroup: RolePermissionGroup = {
            groupId: this.id,
            roleId: role.id,
            roleName: role.name,
            roleDesc: role.description,
        };

        if (!role.selected) {
            this._permissionsService.addRolePermissionGroup(
                rolePermissionGroup
            );
        } else {
            this._permissionsService.removeRolePermissionGroup(
                rolePermissionGroup
            );
        }
        role.selected = !role.selected;
    }

    /**
     * Saves changes by updating permissions and displaying a notification.
     * This method calls the updatePermissions method from the permissions service,
     * and upon successful update, it shows a snack bar notification indicating
     * that the information has been updated.
     */
    saveChanges(): void {
        this._permissionsService.updatePermissions().subscribe(() => {
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
