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
import { Language } from 'app/core/types/languages.types';
import { UsersService } from 'app/modules/admin/users/users.service';
import { User } from 'app/modules/admin/users/users.types';
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
    selector: 'users-list',
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
export class UsersListComponent implements OnInit, OnDestroy {
    @ViewChild('matDrawer', { static: true }) matDrawer: MatDrawer;

    users$: Observable<User[]>;

    usersCount: number = 0;
    languages: Language[];
    drawerMode: 'side' | 'over';
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    selectedUser: User;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _usersService: UsersService,
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
        // Get the users
        this.users$ = this._usersService.users$;
        this._usersService.users$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((users: User[]) => {
                // Update the counts
                this.usersCount = users.length;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the user
        this._usersService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) => {
                // Update the selected user
                this.selectedUser = user;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the languages
        this._usersService.language$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((languages: Language[]) => {
                this.languages = languages;

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
                    this._usersService.searchUsers(query)
                )
            )
            .subscribe();

        // Subscribe to MatDrawer opened change
        this.matDrawer.openedChange.subscribe((opened) => {
            if (!opened) {
                // Remove the selected user when drawer closed
                this.selectedUser = null;

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
                this.createUser();
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
     * Create user
     */
    createUser(): void {
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
