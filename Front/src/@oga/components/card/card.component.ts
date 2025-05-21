import { BooleanInput, coerceBooleanProperty } from '@angular/cdk/coercion';
import { NgIf } from '@angular/common';
import {
    Component,
    HostBinding,
    Input,
    OnChanges,
    SimpleChanges,
    ViewEncapsulation,
} from '@angular/core';
import { ogaAnimations } from '@oga/animations';
import { OgaCardFace } from '@oga/components/card/card.types';

@Component({
    selector: 'oga-card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: ogaAnimations,
    exportAs: 'ogaCard',
    standalone: true,
    imports: [NgIf],
})
export class OgaCardComponent implements OnChanges {
    /* eslint-disable @typescript-eslint/naming-convention */
    static ngAcceptInputType_expanded: BooleanInput;
    static ngAcceptInputType_flippable: BooleanInput;
    /* eslint-enable @typescript-eslint/naming-convention */

    @Input() expanded: boolean = false;
    @Input() face: OgaCardFace = 'front';
    @Input() flippable: boolean = false;

    /**
     * Constructor
     */
    constructor() {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Host binding for component classes
     */
    @HostBinding('class') get classList(): any {
        /* eslint-disable @typescript-eslint/naming-convention */
        return {
            'oga-card-expanded': this.expanded,
            'oga-card-face-back': this.flippable && this.face === 'back',
            'oga-card-face-front': this.flippable && this.face === 'front',
            'oga-card-flippable': this.flippable,
        };
        /* eslint-enable @typescript-eslint/naming-convention */
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On changes
     *
     * @param changes
     */
    ngOnChanges(changes: SimpleChanges): void {
        // Expanded
        if ('expanded' in changes) {
            // Coerce the value to a boolean
            this.expanded = coerceBooleanProperty(
                changes.expanded.currentValue
            );
        }

        // Flippable
        if ('flippable' in changes) {
            // Coerce the value to a boolean
            this.flippable = coerceBooleanProperty(
                changes.flippable.currentValue
            );
        }
    }
}
