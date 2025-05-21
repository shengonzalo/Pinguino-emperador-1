import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { OgaNavigationItem } from '@oga/components/navigation';
import {
    Navigation,
    NavigationApiResponse,
} from 'app/core/navigation/navigation.types';
import { environment } from 'environments/environment';
import { BehaviorSubject, map, Observable, ReplaySubject, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NavigationService {
    private _httpClient = inject(HttpClient);
    private _navigation: ReplaySubject<Navigation> =
        new ReplaySubject<Navigation>(1);

    private readonly _activePageTitle: BehaviorSubject<string> =
        new BehaviorSubject<string>('');

    constructor() {}

    /**
     * Getter for navigation observable.
     * @returns An observable of the current navigation data.
     */
    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    /**
     * Getter for active page title observable.
     * @returns An observable of the current active page title.
     */
    get activePageTitle$(): Observable<string> {
        return this._activePageTitle.asObservable();
    }

    /**
     * Sets the active page title.
     * @param title The title of the active page.
     */
    setActivePageTitle(title: string): void {
        this._activePageTitle.next(title);
    }

    /**
     * Fetches all navigation data from the API.
     * @returns An observable of the navigation data.
     */
    get(): Observable<Navigation> {
        return this._httpClient
            .get<NavigationApiResponse[]>(`${environment.apiUrl}/menu`)
            .pipe(
                map((apiResponse: NavigationApiResponse[]) =>
                    this.transformApiResponseToNavigation(apiResponse)
                ),
                tap((navigation: Navigation) => {
                    this._navigation.next(navigation);
                })
            );
    }

    /**
     * Transforms the API response into the navigation structure.
     * @param apiResponse The response from the API containing navigation data.
     * @returns The transformed navigation data.
     */
    private transformApiResponseToNavigation(
        apiResponse: NavigationApiResponse[]
    ): Navigation {
        const transformedNavigation: Navigation = {
            compact: [],
            default: [],
            futuristic: [],
            horizontal: [],
        };

        apiResponse.forEach((apiItem) => {
            const navigationItem =
                this.transformApiItemToNavigationItem(apiItem);
            transformedNavigation.compact.push(navigationItem);
            transformedNavigation.default.push(navigationItem);
            transformedNavigation.futuristic.push(navigationItem);
            transformedNavigation.horizontal.push(navigationItem);
        });
        return transformedNavigation;
    }

    /**
     * Transforms an individual API item into a navigation item.
     * @param apiItem The API item to transform.
     * @returns The transformed navigation item.
     */
    private transformApiItemToNavigationItem(apiItem: any): any {
        const navigationItem: any = {
            id: apiItem.id.toString(),
            title: apiItem.isLiteral ? apiItem.literal : apiItem.name,
            isLiteral: apiItem.isLiteral,
            type: this.determineType(apiItem), // Usar una función para determinar el tipo
            icon: apiItem.icon,
            link: apiItem.path,
            order: apiItem.order, // Añadir el campo order
            children:
                apiItem.menuItems.length > 0
                    ? apiItem.menuItems.map((child: any) =>
                          this.transformApiItemToNavigationItem(child)
                      )
                    : undefined,
        };
        return navigationItem;
    }

    private determineType(apiItem: any): string {
        if (apiItem.name === 'spacer') {
            return 'spacer';
        } else if (apiItem.name === 'divider') {
            return 'divider';
        } else {
            return apiItem.menuItems.length > 0 ? 'collapsable' : 'basic';
        }
    }
}
