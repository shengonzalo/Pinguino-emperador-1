import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, switchMap, tap } from 'rxjs';
import { environment } from 'environments/environment';
import { PaginatedApiResponse } from 'app/core/types/paginated-api-response.types';
import { Permission, RolePermissionGroup } from './permissions.types';
import { Role } from 'app/modules/admin/roles/roles.types';

@Injectable({ providedIn: 'root' })
export class PermissionsService {
    // Private
    private _permissions: BehaviorSubject<Permission[] | []> =
        new BehaviorSubject([]);
    private _roles: BehaviorSubject<Role[] | []> = new BehaviorSubject([]);
    private _rolePermissionGroups: BehaviorSubject<RolePermissionGroup[] | []> =
        new BehaviorSubject([]);

    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for permissions
     */
    get permission$(): Observable<Permission[]> {
        return this._permissions.asObservable();
    }

    /**
     * Getter for roles
     */
    get roles$(): Observable<Role[]> {
        return this._roles.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Extracts all RolePermissionGroup arrays from an array of Permissions and flattens them into a single array.
     * @param permissions An array of Permission objects.
     * @returns An array of RolePermissionGroup objects.
     */
    private extractRolePermissionGroups(
        permissions: Permission[]
    ): RolePermissionGroup[] {
        const rolePermissionGroups = permissions.flatMap(
            (permission) => permission.rolePermissionGroup
        );
        return rolePermissionGroups;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Adds a new RolePermissionGroup to the existing list of role permission groups.
     * @param rolePermissionGroup The RolePermissionGroup object to add.
     */
    addRolePermissionGroup(rolePermissionGroup: RolePermissionGroup): void {
        const currentGroups = this._rolePermissionGroups.getValue();
        this._rolePermissionGroups.next([
            ...currentGroups,
            rolePermissionGroup,
        ]);
    }

    /**
     * Removes a specified RolePermissionGroup from the current list of role permission groups.
     * @param rolePermissionGroup The RolePermissionGroup object to remove.
     */
    removeRolePermissionGroup(rolePermissionGroup: RolePermissionGroup): void {
        const currentGroups = this._rolePermissionGroups.getValue();
        const index = currentGroups.findIndex(
            (group: RolePermissionGroup) =>
                group.roleId === rolePermissionGroup.roleId &&
                group.groupId === rolePermissionGroup.groupId
        );
        if (index !== -1) {
            currentGroups.splice(index, 1);
            this._rolePermissionGroups.next([...currentGroups]);
        }
    }

    /**
     * Get permissions
     */
    getPermissions(): Observable<Permission[]> {
        return this._httpClient
            .get<PaginatedApiResponse>(
                `${environment.apiUrl}/permissions/groups`
            )
            .pipe(
                map((response: PaginatedApiResponse) => response.data),
                tap((permissions: Permission[]) => {
                    this._permissions.next(permissions);
                    const rolePermissionGroups =
                        this.extractRolePermissionGroups(permissions);
                    this._rolePermissionGroups.next(rolePermissionGroups);
                })
            );
    }

    /**
     * Get roles
     */
    getRoles(): Observable<Role[]> {
        return this._roles.getValue().length > 0
            ? this._roles.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(
                      `${environment.apiUrl}/roles?Order.PropertyName=order&Order.Ascendant=true`
                  )
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((roles) => {
                          this._roles.next(roles);
                      })
                  );
    }

    /**
     * Updates permissions by sending the current state of role permission groups to the server.
     * After updating, it fetches the updated permissions and updates the local state.
     * @returns {Observable<boolean>} An observable that emits true when the operation is successful.
     */
    updatePermissions(): Observable<boolean> {
        return this._httpClient
            .put<Permission[]>(
                `${environment.apiUrl}/permissions/groups/roles`,
                this._rolePermissionGroups.getValue()
            )
            .pipe(
                switchMap(() => this.getPermissions()),
                tap((permissions: Permission[]) =>
                    this._permissions.next(permissions)
                ),
                map(() => true)
            );
    }
}
