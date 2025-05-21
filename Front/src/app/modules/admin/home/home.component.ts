import { Component, ViewEncapsulation } from '@angular/core';
import { BreadcrumbService } from '@oga/components/breadcrumb/breadcrumb.service';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';

@Component({
    selector: 'home',
    standalone: true,
    templateUrl: './home.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class HomeComponent {
    /**
     * Constructor
     */
    constructor(private _breadcrumbService: BreadcrumbService) {
        this.updateBreadcrumbs();
    }

    // Método para actualizar los breadcrumbs
    updateBreadcrumbs(): void {
        const items: BreadcrumbItem[] = [
            { name: 'navigation.home', path: '/' },
        ];
        this._breadcrumbService.setBreadcrumbItems(items);
    }
}
