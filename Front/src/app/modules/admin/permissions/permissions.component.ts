import { NgClass, NgFor, NgSwitch, NgSwitchCase } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { TranslocoModule, TranslocoService } from '@ngneat/transloco';
import { OgaMediaWatcherService } from '@oga/services/media-watcher';
import { Subject, takeUntil } from 'rxjs';
import { PermissionsService } from './permissions.service';
import { Permission } from './permissions.types';
import { PermissionsGroupConfigurationComponent } from './permissions-group-configuration/permissions-group-configuration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BreadcrumbService } from '@oga/components/breadcrumb/breadcrumb.service';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';

@Component({
    selector: 'permissions',
    templateUrl: './permissions.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        MatSidenavModule,
        MatButtonModule,
        MatIconModule,
        MatFormFieldModule,
        NgFor,
        NgClass,
        NgSwitch,
        NgSwitchCase,
        TranslocoModule,
        PermissionsGroupConfigurationComponent,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
    ],
})
export class PermissionsComponent implements OnInit, OnDestroy {
    /**
     * ViewChild and ViewChildren references.
     */
    @ViewChild('drawer') drawer: MatDrawer;

    /**
     * UI State variables.
     */
    drawerMode: 'over' | 'side' = 'side';
    drawerOpened: boolean = true;
    panels: any[] = [];
    selectedPanel: number = 1;
    searchTerm: string = '';

    /**
     * Subscription management.
     */
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _ogaMediaWatcherService: OgaMediaWatcherService,
        private _permissionsService: PermissionsService,
        private _translate: TranslocoService,
        private _breadcrumbService: BreadcrumbService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.updateBreadcrumbs();
        this._permissionsService.permission$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((permissions: Permission[]) => {
                this.panels = permissions.map((permission) => {
                    if (permission.name.startsWith('base_menu')) {
                        return {
                            id: permission.id,
                            icon: 'base_menu',
                            title: this._translate.translate(permission.name),
                            description: this._translate.translate(
                                permission.description
                            ),
                        };
                    } else if (permission.name.includes('admin_group')) {
                        return {
                            id: permission.id,
                            icon: 'base_admin',
                            title: this._translate.translate(permission.name),
                            description: this._translate.translate(
                                permission.description
                            ),
                        };
                    } else {
                        return {
                            id: permission.id,
                            icon: 'base_read',
                            title: this._translate.translate(permission.name),
                            description: this._translate.translate(
                                permission.description
                            ),
                        };
                    }
                });
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to media changes
        this._ogaMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ matchingAliases }) => {
                // Set the drawerMode and drawerOpened
                if (matchingAliases.includes('lg')) {
                    this.drawerMode = 'side';
                    this.drawerOpened = true;
                } else {
                    this.drawerMode = 'over';
                    this.drawerOpened = false;
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
     * Navigate to the panel
     *
     * @param panel
     */
    goToPanel(panel: number): void {
        this.selectedPanel = panel;

        // Close the drawer on 'over' mode
        if (this.drawerMode === 'over') {
            this.drawer.close();
        }
    }

    /**
     * Get the details of the panel
     *
     * @param id
     */
    getPanelInfo(id: number): any {
        return this.panels.find((panel) => panel.id === id);
    }

    /**
     * Get the icon based on the panel name.
     *
     * This method returns the icon identifier for a given panel name. The icon identifiers
     * are formatted for use with a specific icon library (heroicons_outline).
     *
     * @param name The name of the panel for which the icon is needed.
     * @returns The icon identifier string.
     */
    getPanelIcon(name: string): string {
        switch (name) {
            case 'base_menu':
                return 'heroicons_outline:bars-4';
            case 'base_admin':
                return 'heroicons_outline:lock-closed';
            case 'base_read':
                return 'heroicons_outline:eye';
            default:
                return 'heroicons_outline:key';
        }
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    /**
     * Filters the panels based on the search term.
     *
     * @returns {any[]} The filtered panels.
     */
    filteredPanels(): any[] {
        if (!this.searchTerm) return this.panels;

        return this.panels.filter(
            (panel) =>
                panel.title
                    .toLowerCase()
                    .includes(this.searchTerm.toLowerCase()) ||
                panel.description
                    .toLowerCase()
                    .includes(this.searchTerm.toLowerCase())
        );
    }

    // Método para actualizar los breadcrumbs
    updateBreadcrumbs(): void {
        const items: BreadcrumbItem[] = [
            { name: 'navigation.administration', path: '/administration' },
            {
                name: 'navigation.permissions',
                path: '/administration/permissions',
            },
        ];
        this._breadcrumbService.setBreadcrumbItems(items);
    }

    handleKeyDown(event: KeyboardEvent, panelId: string): void {
        if (event.key === 'Enter' || event.key === ' ') {
            this.goToPanel(parseInt(panelId));
        }
    }
}
