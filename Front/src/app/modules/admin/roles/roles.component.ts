import {
    ChangeDetectionStrategy,
    Component,
    ViewEncapsulation,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BreadcrumbService } from '@oga/components/breadcrumb/breadcrumb.service';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';

@Component({
    selector: 'roles',
    templateUrl: './roles.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [RouterOutlet],
})
export class RolesComponent {
    /**
     * Constructor
     */
    constructor(private _breadcrumbService: BreadcrumbService) {
        this.updateBreadcrumbs();
    }

    // Método para actualizar los breadcrumbs
    updateBreadcrumbs(): void {
        const items: BreadcrumbItem[] = [
            { name: 'navigation.administration', path: '/administration' },
            { name: 'navigation.roles', path: '/administration/roles' },
        ];
        this._breadcrumbService.setBreadcrumbItems(items);
    }
}
