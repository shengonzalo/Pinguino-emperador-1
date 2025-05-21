import { Injectable } from '@angular/core';
import { BreadcrumbItem } from 'app/core/types/breadcrumbItem.types';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root', // Proporciona el servicio a nivel de aplicación
})
export class BreadcrumbService {
    private _breadcrumbItems: BehaviorSubject<BreadcrumbItem[]> =
        new BehaviorSubject<BreadcrumbItem[]>([]);

    constructor() {}

    // Método para obtener el observable de los breadcrumbs
    get breadcrumbItems$(): Observable<BreadcrumbItem[]> {
        return this._breadcrumbItems.asObservable();
    }

    // Método para actualizar los breadcrumbs
    setBreadcrumbItems(items: BreadcrumbItem[]): void {
        this._breadcrumbItems.next(items);
    }
}
