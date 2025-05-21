import { NgClass, NgIf } from '@angular/common';
import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { OgaConfirmationConfig } from '@oga/services/confirmation/confirmation.types';

@Component({
    selector: 'oga-confirmation-dialog',
    templateUrl: './dialog.component.html',
    styles: [
        `
            .oga-confirmation-dialog-panel {
                @screen md {
                    @apply w-128;
                }

                .mat-mdc-dialog-container {
                    .mat-mdc-dialog-surface {
                        padding: 0 !important;
                    }
                }
            }
        `,
    ],
    encapsulation: ViewEncapsulation.None,
    standalone: true,
    imports: [
        NgIf,
        MatButtonModule,
        MatDialogModule,
        MatIconModule,
        NgClass,
        TranslocoModule,
    ],
})
export class OgaConfirmationDialogComponent {
    /**
     * Constructor
     */
    constructor(@Inject(MAT_DIALOG_DATA) public data: OgaConfirmationConfig) {}
}
