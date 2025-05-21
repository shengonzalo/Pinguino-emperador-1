import { NgFor, NgIf } from '@angular/common';
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
import { ogaAnimations } from '@oga/animations';
import { OgaNavigationService } from '@oga/components/navigation/navigation.service';
import { OgaNavigationItem } from '@oga/components/navigation/navigation.types';
import { OgaUtilsService } from '@oga/services/utils/utils.service';
import { ReplaySubject, Subject } from 'rxjs';
import { OgaHorizontalNavigationBasicItemComponent } from './components/basic/basic.component';
import { OgaHorizontalNavigationBranchItemComponent } from './components/branch/branch.component';
import { OgaHorizontalNavigationSpacerItemComponent } from './components/spacer/spacer.component';

@Component({
    selector: 'oga-horizontal-navigation',
    templateUrl: './horizontal.component.html',
    styleUrls: ['./horizontal.component.scss'],
    animations: ogaAnimations,
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'ogaHorizontalNavigation',
    standalone: true,
    imports: [
        NgFor,
        NgIf,
        OgaHorizontalNavigationBasicItemComponent,
        OgaHorizontalNavigationBranchItemComponent,
        OgaHorizontalNavigationSpacerItemComponent,
    ],
})
export class OgaHorizontalNavigationComponent
    implements OnChanges, OnInit, OnDestroy
{
    @Input() name: string = this._ogaUtilsService.randomId();
    @Input() navigation: OgaNavigationItem[];

    onRefreshed: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _ogaNavigationService: OgaNavigationService,
        private _ogaUtilsService: OgaUtilsService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On changes
     *
     * @param changes
     */
    ngOnChanges(changes: SimpleChanges): void {
        // Navigation
        if ('navigation' in changes) {
            // Mark for check
            this._changeDetectorRef.markForCheck();
        }
    }

    /**
     * On init
     */
    ngOnInit(): void {
        // Make sure the name input is not an empty string
        if (this.name === '') {
            this.name = this._ogaUtilsService.randomId();
        }

        // Register the navigation component
        this._ogaNavigationService.registerComponent(this.name, this);
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Deregister the navigation component from the registry
        this._ogaNavigationService.deregisterComponent(this.name);

        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Refresh the component to apply the changes
     */
    refresh(): void {
        // Mark for check
        this._changeDetectorRef.markForCheck();

        // Execute the observable
        this.onRefreshed.next(true);
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
}
