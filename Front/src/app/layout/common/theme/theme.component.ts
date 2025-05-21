import {
    ChangeDetectionStrategy,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { OgaConfig, OgaConfigService, Scheme } from '@oga/services/config';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'theme',
    templateUrl: './theme.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'theme',
    standalone: true,
    imports: [MatIconModule, MatButtonModule],
})
export class ThemeComponent implements OnInit, OnDestroy {
    config: OgaConfig;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(private _ogaConfigService: OgaConfigService) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Subscribe to config changes
        this._ogaConfigService.config$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config: OgaConfig) => {
                // Store the config
                this.config = config;
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
     * Set the scheme on the config
     *
     * @param scheme
     */
    setScheme(scheme: Scheme): void {
        this._ogaConfigService.config = { scheme };
    }
}
