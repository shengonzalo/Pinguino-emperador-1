import { BooleanInput } from '@angular/cdk/coercion';
import { NgClass, NgFor, NgIf } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    forwardRef,
    Input,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { OgaNavigationService } from '@oga/components/navigation/navigation.service';
import { OgaNavigationItem } from '@oga/components/navigation/navigation.types';
import { OgaVerticalNavigationBasicItemComponent } from '@oga/components/navigation/vertical/components/basic/basic.component';
import { OgaVerticalNavigationCollapsableItemComponent } from '@oga/components/navigation/vertical/components/collapsable/collapsable.component';
import { OgaVerticalNavigationDividerItemComponent } from '@oga/components/navigation/vertical/components/divider/divider.component';
import { OgaVerticalNavigationSpacerItemComponent } from '@oga/components/navigation/vertical/components/spacer/spacer.component';
import { OgaVerticalNavigationComponent } from '@oga/components/navigation/vertical/vertical.component';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'oga-vertical-navigation-group-item',
    templateUrl: './group.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        NgClass,
        NgIf,
        MatIconModule,
        NgFor,
        OgaVerticalNavigationBasicItemComponent,
        OgaVerticalNavigationCollapsableItemComponent,
        OgaVerticalNavigationDividerItemComponent,
        forwardRef(() => OgaVerticalNavigationGroupItemComponent),
        OgaVerticalNavigationSpacerItemComponent,
        TranslocoModule
    ],
})
export class OgaVerticalNavigationGroupItemComponent
    implements OnInit, OnDestroy
{
    /* eslint-disable @typescript-eslint/naming-convention */
    static ngAcceptInputType_autoCollapse: BooleanInput;
    /* eslint-enable @typescript-eslint/naming-convention */

    @Input() autoCollapse: boolean;
    @Input() item: OgaNavigationItem;
    @Input() name: string;

    private _ogaVerticalNavigationComponent: OgaVerticalNavigationComponent;
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
        this._ogaVerticalNavigationComponent =
            this._ogaNavigationService.getComponent(this.name);

        // Subscribe to onRefreshed on the navigation component
        this._ogaVerticalNavigationComponent.onRefreshed
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

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

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
