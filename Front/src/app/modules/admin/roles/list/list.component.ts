import {
    AsyncPipe,
    DOCUMENT,
    I18nPluralPipe,
    NgClass,
    NgFor,
    NgIf,
} from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Inject,
    OnDestroy,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from '@angular/core';
import {
    FormsModule,
    ReactiveFormsModule,
    UntypedFormControl,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import {
    ActivatedRoute,
    Router,
    RouterLink,
    RouterOutlet,
} from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import { OgaMediaWatcherService } from '@oga/services/media-watcher';
import { HasPermissionDirective } from 'app/core/resource-permissions/resource-permissions.directive';
import { RolesService } from 'app/modules/admin/roles/roles.service';
import { Role } from 'app/modules/admin/roles/roles.types';
import {
    debounceTime,
    filter,
    fromEvent,
    Observable,
    Subject,
    switchMap,
    takeUntil,
} from 'rxjs';

@Component({
    selector: 'roles-list',
    templateUrl: './list.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [
        MatSidenavModule,
        RouterOutlet,
        NgIf,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        NgFor,
        NgClass,
        RouterLink,
        AsyncPipe,
        I18nPluralPipe,
        TranslocoModule,
        HasPermissionDirective
    ],
})
export class RolesListComponent implements OnInit, OnDestroy {
    @ViewChild('matDrawer', { static: true }) matDrawer: MatDrawer;

    roles$: Observable<Role[]>;

    rolesCount: number = 0;
    drawerMode: 'side' | 'over';
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    selectedRole: Role;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _rolesService: RolesService,
        @Inject(DOCUMENT) private _document: any,
        private _router: Router,
        private _ogaMediaWatcherService: OgaMediaWatcherService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Get the roles
        this.roles$ = this._rolesService.roles$;
        this._rolesService.roles$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((roles: Role[]) => {
                // Update the counts
                this.rolesCount = roles.length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the role
        this._rolesService.role$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((role: Role) => {
                // Update the selected role
                this.selectedRole = role;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to search input field value changes
        this.searchInputControl.valueChanges
            .pipe(
                debounceTime(300),
                takeUntil(this._unsubscribeAll),
                switchMap((query) =>
                    // Search
                    this._rolesService.searchRoles(query)
                )
            )
            .subscribe();

        // Subscribe to MatDrawer opened change
        this.matDrawer.openedChange.subscribe((opened) => {
            if (!opened) {
                // Remove the selected role when drawer closed
                this.selectedRole = null;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            }
        });

        // Subscribe to media changes
        this._ogaMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ matchingAliases }) => {
                // Set the drawerMode if the given breakpoint is active
                if (matchingAliases.includes('lg')) {
                    this.drawerMode = 'side';
                } else {
                    this.drawerMode = 'over';
                }

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Listen for shortcuts
        fromEvent(this._document, 'keydown')
            .pipe(
                takeUntil(this._unsubscribeAll),
                filter<KeyboardEvent>(
                    (event) =>
                        (event.ctrlKey === true || event.metaKey) && // Ctrl or Cmd
                        event.key === '/' // '/'
                )
            )
            .subscribe(() => {
                this.createRole();
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
     * On backdrop clicked
     */
    onBackdropClicked(): void {
        // Go back to the list
        this._router.navigate(['./'], { relativeTo: this._activatedRoute });

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    /**
     * Create role
     */
    createRole(): void {
        this._router.navigate(['./', 0], {
            relativeTo: this._activatedRoute,
        });

        // Mark for check
        this._changeDetectorRef.markForCheck();
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
