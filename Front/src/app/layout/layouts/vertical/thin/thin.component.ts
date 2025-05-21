import { NgIf, UpperCasePipe } from '@angular/common';
import {
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import { BreadcrumbComponent } from '@oga/components/breadcrumb/breadcrumb.component';
import { OgaFullscreenComponent } from '@oga/components/fullscreen';
import { OgaLoadingBarComponent } from '@oga/components/loading-bar';
import {
    OgaNavigationService,
    OgaVerticalNavigationComponent,
} from '@oga/components/navigation';
import { OgaMediaWatcherService } from '@oga/services/media-watcher';
import { NavigationService } from 'app/core/navigation/navigation.service';
import { Navigation } from 'app/core/navigation/navigation.types';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';
import { LanguagesComponent } from 'app/layout/common/languages/languages.component';
import { UserComponent } from 'app/layout/common/user/user.component';
import { Subject, takeUntil } from 'rxjs';
import { BreadcrumbService } from '@oga/components/breadcrumb/breadcrumb.service';

@Component({
    selector: 'thin-layout',
    templateUrl: './thin.component.html',
    encapsulation: ViewEncapsulation.None,
    standalone: true,
    imports: [
        OgaLoadingBarComponent,
        OgaVerticalNavigationComponent,
        BreadcrumbComponent,
        MatMenuModule,
        MatButtonModule,
        MatIconModule,
        LanguagesComponent,
        OgaFullscreenComponent,
        UserComponent,
        NgIf,
        RouterOutlet,
        TranslocoModule,
    ],
})
export class ThinLayoutComponent implements OnInit, OnDestroy {
    isScreenSmall: boolean;
    navigation: Navigation;
    activePageTitle: string;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    breadcrumbItems: BreadcrumbItem[] = [];
    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _navigationService: NavigationService,
        private _ogaMediaWatcherService: OgaMediaWatcherService,
        private _ogaNavigationService: OgaNavigationService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _breadcrumbService: BreadcrumbService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for current year
     */
    get currentYear(): number {
        return new Date().getFullYear();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Subscribe to navigation data
        this._navigationService.navigation$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((navigation: Navigation) => {
                this.navigation = navigation;
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to active page title
        this._navigationService.activePageTitle$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((title: string) => {
                this.activePageTitle = title;
            });

        // Subscribe to media changes
        this._ogaMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ matchingAliases }) => {
                // Check if the screen is small
                this.isScreenSmall = !matchingAliases.includes('md');
            });

        // Suscríbete al observable de breadcrumbs
        this._breadcrumbService.breadcrumbItems$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((items: BreadcrumbItem[]) => {
                this.breadcrumbItems = items;
                this._changeDetectorRef.markForCheck();
            });

        this._changeDetectorRef.detectChanges();
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
     * Toggle navigation
     *
     * @param name
     */
    toggleNavigation(name: string): void {
        // Get the navigation
        const navigation =
            this._ogaNavigationService.getComponent<OgaVerticalNavigationComponent>(
                name
            );

        if (navigation) {
            // Toggle the opened status
            navigation.toggle();
        }
    }
}
