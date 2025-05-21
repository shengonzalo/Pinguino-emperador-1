import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, forkJoin, Observable, tap } from 'rxjs';
import { ResourcePermissions } from './resource-permissions.types';
import { environment } from 'environments/environment';

@Injectable({ providedIn: 'root' })
export class ResourcePermissionsService {
    // Private
    private _resourcePermissions: BehaviorSubject<ResourcePermissions[]> =
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
    get resourcePermissions$(): Observable<ResourcePermissions[]> {
        return this._resourcePermissions.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Get permissions
     */
    getResourcePermissions(
        resourceIds: string[]
    ): Observable<ResourcePermissions[]> {
        const requests = resourceIds.map((id) =>
            this._httpClient.get<ResourcePermissions>(
                `${environment.apiUrl}/permissions?resourceId=${id}`
            )
        );

        return forkJoin(requests).pipe(
            tap((permissionsArray: ResourcePermissions[]) => {
                const currentPermissions = [];
                permissionsArray.forEach((permissions) => {
                    currentPermissions.push(permissions);
                });
                this._resourcePermissions.next(currentPermissions);
            })
        );
    }
}
