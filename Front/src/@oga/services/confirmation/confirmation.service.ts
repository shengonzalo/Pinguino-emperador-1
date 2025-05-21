import { inject, Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { OgaConfirmationConfig } from '@oga/services/confirmation/confirmation.types';
import { OgaConfirmationDialogComponent } from '@oga/services/confirmation/dialog/dialog.component';
import { merge } from 'lodash-es';

@Injectable({ providedIn: 'root' })
export class OgaConfirmationService {
    private _matDialog: MatDialog = inject(MatDialog);
    private _defaultConfig: OgaConfirmationConfig = {
        title: 'actions.confirm',
        message: 'actions.confirm-action',
        icon: {
            show: true,
            name: 'heroicons_outline:exclamation-triangle',
            color: 'warn',
        },
        actions: {
            confirm: {
                show: true,
                label: 'actions.confirm',
                color: 'warn',
            },
            cancel: {
                show: true,
                label: 'actions.cancel',
            },
        },
        dismissible: false,
    };

    /**
     * Constructor
     */
    constructor() {}

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    open(
        config: OgaConfirmationConfig = {}
    ): MatDialogRef<OgaConfirmationDialogComponent> {
        // Merge the user config with the default config
        const userConfig = merge({}, this._defaultConfig, config);

        // Open the dialog
        return this._matDialog.open(OgaConfirmationDialogComponent, {
            autoFocus: false,
            disableClose: !userConfig.dismissible,
            data: userConfig,
            panelClass: 'oga-confirmation-dialog-panel',
        });
    }
}
