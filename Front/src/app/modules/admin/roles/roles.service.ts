import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
    BehaviorSubject,
    map,
    Observable,
    of,
    switchMap,
    take,
    tap,
    throwError,
} from 'rxjs';
import { Role } from './roles.types';
import { environment } from 'environments/environment';
import { PaginatedApiResponse } from 'app/core/types/paginated-api-response.types';

@Injectable({ providedIn: 'root' })
export class RolesService {
    // Private
    private _role: BehaviorSubject<Role | null> = new BehaviorSubject(null);
    private _roles: BehaviorSubject<Role[] | null> = new BehaviorSubject(null);
    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for role
     */
    get role$(): Observable<Role> {
        return this._role.asObservable();
    }

    /**
     * Getter for roles
     */
    get roles$(): Observable<Role[]> {
        return this._roles.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get roles
     */
    getRoles(): Observable<Role[]> {
        return this._httpClient
            .get<PaginatedApiResponse>(
                `${environment.apiUrl}/roles?Order.PropertyName=order&Order.Ascendant=true`
            )
            .pipe(
                map((response: PaginatedApiResponse) => response.data),
                tap((roles: Role[]) => {
                    this._roles.next(roles);
                })
            );
    }
    /**
     * Search roles with given query
     *
     * @param query
     */
    searchRoles(query: string): Observable<Role[]> {
        return this._httpClient
            .get<PaginatedApiResponse>(
                `${environment.apiUrl}/roles?Order.PropertyName=order&Order.Ascendant=true&name=${query}`
            )
            .pipe(
                map((response: PaginatedApiResponse) => response.data),
                tap((roles) => {
                    this._roles.next(roles);
                })
            );
    }

    /**
     * Get role by id
     */
    getRoleById(id: number): Observable<Role> {
        if (id === 0) {
            // Create a new empty role model
            const newRole: Role = {
                id: 0,
                name: '',
                description: '',
                order: 0
            };

            // Return the new empty role with editMode as true
            this._role.next(newRole);
            return of(newRole);
        }

        return this._roles.pipe(
            take(1),
            map((roles) => {
                // Find the role
                const role = roles.find((item: Role) => item.id === id) || null;

                // Update the role
                this._role.next(role);

                // Return the role
                return role;
            }),
            switchMap((role) => {
                if (!role) {
                    return throwError(
                        'Could not found role with id of ' + id + '!'
                    );
                }
                return of(role);
            })
        );
    }

    /**
     * Create role
     */
    createRole(role: Role): Observable<Role> {
        return this.roles$.pipe(
            take(1),
            switchMap((roles) =>
                this._httpClient
                    .post<Role>(`${environment.apiUrl}/roles`, role)
                    .pipe(
                        switchMap(() => this.getRoles()), // Fetch all roles again after create
                        map((_) => {
                            return role;
                        })
                    )
            )
        );
    }

    /**
     * Update role
     *
     * @param id
     * @param role
     */
    updateRole(id: number, role: Role): Observable<Role> {
        return this.roles$.pipe(
            take(1),
            switchMap((roles) =>
                this._httpClient
                    .put<Role>(`${environment.apiUrl}/roles/${role.id}`, role)
                    .pipe(
                        map((_) => {
                            // Find the index of the updated role
                            const index = roles.findIndex(
                                (item) => item.id === id
                            );
                            roles[index] = role;
                            this._roles.next(roles);
                            return role;
                        }),
                        switchMap(() => this.getRoles()), // Fetch all roles again after update
                        map((_) => {
                            this._role.next(role);
                            return role;
                        })
                    )
            )
        );
    }

    /**
     * Delete the role
     *
     * @param id
     */
    deleteRole(id: number): Observable<boolean> {
        return this.roles$.pipe(
            take(1),
            switchMap((roles) =>
                this._httpClient
                    .delete(`${environment.apiUrl}/roles/${id}`)
                    .pipe(
                        map((isDeleted: boolean) => {
                            // Find the index of the deleted role
                            const index = roles.findIndex(
                                (item: Role) => item.id === id
                            );

                            // Delete the role
                            roles.splice(index, 1);

                            // Update the roles
                            this._roles.next(roles);

                            // Return the deleted status
                            return isDeleted;
                        })
                    )
            )
        );
    }

}
