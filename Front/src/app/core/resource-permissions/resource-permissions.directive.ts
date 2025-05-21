import {
    Directive,
    Input,
    TemplateRef,
    ViewContainerRef,
    OnDestroy,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { ResourcePermissionsService } from './resource-permissions.service';

/**
 * Directive to conditionally include an HTML element based on user permissions.
 */
@Directive({
    selector: '[ogaHasPermission]',
    standalone: true,
})
export class HasPermissionDirective implements OnDestroy {
    private permissionName: string;
    private permissionsSubscription: Subscription;

    /**
     * Constructs the permission directive.
     * @param templateRef Reference to the template where the directive is applied.
     * @param viewContainer Reference to the container view that hosts the view.
     * @param permissionsService Service to get permissions from.
     */
    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private permissionsService: ResourcePermissionsService
    ) {}

    /**
     * Input property that sets the permission name and checks permissions.
     * @param permissionName The name of the permission to check.
     */
    @Input() set ogaHasPermission(permissionName: string) {
        if (permissionName) {
            this.permissionName = permissionName;
            this.checkPermissions();
        }
    }

    /**
     * Checks permissions and updates the view accordingly.
     */
    private checkPermissions() {
        this.viewContainer.clear();
        if (this.permissionsSubscription) {
            this.permissionsSubscription.unsubscribe();
        }        

        this.permissionsSubscription =
            this.permissionsService.resourcePermissions$.subscribe(
                (permissions) => {
                    const hasPermission = this.hasPermissionRecursive(
                        permissions,
                        this.permissionName
                    );
                    if (hasPermission) {
                        this.viewContainer.createEmbeddedView(this.templateRef);
                    } else {
                        this.viewContainer.clear();
                    }
                }
            );
    }

    /**
     * Recursively checks if the permission exists within the nested permission structure.
     * @param permissions The list of permissions to check.
     * @param resourceName The name of the resource to check permissions against.
     * @returns True if the permission is granted, false otherwise.
     */
    private hasPermissionRecursive(
        permissions: any[],
        resourceName: string
    ): boolean {
        for (const permission of permissions) {
            if (
                permission.resource.name === resourceName &&
                permission.permissions.some((p) => p.allowed)
            ) {
                return true;
            }
            if (permission.nodes && permission.nodes.length > 0) {
                if (
                    this.hasPermissionRecursive(permission.nodes, resourceName)
                ) {
                    return true;
                }
            }
        }
        return false;
    }

    /**
     * Cleans up the subscription on directive destruction.
     */
    ngOnDestroy() {
        if (this.permissionsSubscription) {
            this.permissionsSubscription.unsubscribe();
        }
    }
}
