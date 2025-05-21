import { NgClass } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { OgaHorizontalNavigationComponent } from '@oga/components/navigation/horizontal/horizontal.component';
import { OgaNavigationService } from '@oga/components/navigation/navigation.service';
import { OgaNavigationItem } from '@oga/components/navigation/navigation.types';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'oga-horizontal-navigation-divider-item',
    templateUrl: './divider.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [NgClass],
})
export class OgaHorizontalNavigationDividerItemComponent
    implements OnInit, OnDestroy
{
    @Input() item: OgaNavigationItem;
    @Input() name: string;

    private _ogaHorizontalNavigationComponent: OgaHorizontalNavigationComponent;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _ogaNavigationService: OgaNavigationService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Get the parent navigation component
        this._ogaHorizontalNavigationComponent =
            this._ogaNavigationService.getComponent(this.name);

        // Subscribe to onRefreshed on the navigation component
        this._ogaHorizontalNavigationComponent.onRefreshed
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
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
}
