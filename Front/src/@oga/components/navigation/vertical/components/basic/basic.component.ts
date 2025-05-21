import { NgClass, NgIf, NgTemplateOutlet } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import {
    IsActiveMatchOptions,
    RouterLink,
    RouterLinkActive,
} from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import { OgaNavigationService } from '@oga/components/navigation/navigation.service';
import { OgaNavigationItem } from '@oga/components/navigation/navigation.types';
import { OgaVerticalNavigationComponent } from '@oga/components/navigation/vertical/vertical.component';
import { OgaUtilsService } from '@oga/services/utils/utils.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'oga-vertical-navigation-basic-item',
    templateUrl: './basic.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        NgClass,
        NgIf,
        RouterLink,
        RouterLinkActive,
        MatTooltipModule,
        NgTemplateOutlet,
        MatIconModule,
        TranslocoModule
    ],
})
export class OgaVerticalNavigationBasicItemComponent
    implements OnInit, OnDestroy
{
    @Input() item: OgaNavigationItem;
    @Input() name: string;

    isActiveMatchOptions: IsActiveMatchOptions;
    private _ogaVerticalNavigationComponent: OgaVerticalNavigationComponent;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _ogaNavigationService: OgaNavigationService,
        private _ogaUtilsService: OgaUtilsService
    ) {
        // Set the equivalent of {exact: false} as default for active match options.
        // We are not assigning the item.isActiveMatchOptions directly to the
        // [routerLinkActiveOptions] because if it's "undefined" initially, the router
        // will throw an error and stop working.
        this.isActiveMatchOptions = this._ogaUtilsService.subsetMatchOptions;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Set the "isActiveMatchOptions" either from item's
        // "isActiveMatchOptions" or the equivalent form of
        // item's "exactMatch" option
        this.isActiveMatchOptions =
            this.item.isActiveMatchOptions ?? this.item.exactMatch
                ? this._ogaUtilsService.exactMatchOptions
                : this._ogaUtilsService.subsetMatchOptions;

        // Get the parent navigation component
        this._ogaVerticalNavigationComponent =
            this._ogaNavigationService.getComponent(this.name);

        // Mark for check
        this._changeDetectorRef.markForCheck();

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
