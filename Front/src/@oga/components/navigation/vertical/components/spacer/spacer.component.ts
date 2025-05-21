import { NgClass } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { OgaNavigationService } from '@oga/components/navigation/navigation.service';
import { OgaNavigationItem } from '@oga/components/navigation/navigation.types';
import { OgaVerticalNavigationComponent } from '@oga/components/navigation/vertical/vertical.component';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'oga-vertical-navigation-spacer-item',
    templateUrl: './spacer.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [NgClass],
})
export class OgaVerticalNavigationSpacerItemComponent
    implements OnInit, OnDestroy
{
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
}
