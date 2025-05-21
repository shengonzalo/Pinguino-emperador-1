import { Injectable } from '@angular/core';
import { OgaDrawerComponent } from '@oga/components/drawer/drawer.component';

@Injectable({ providedIn: 'root' })
export class OgaDrawerService {
    private _componentRegistry: Map<string, OgaDrawerComponent> = new Map<
        string,
        OgaDrawerComponent
    >();

    /**
     * Constructor
     */
    constructor() {}

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Register drawer component
     *
     * @param name
     * @param component
     */
    registerComponent(name: string, component: OgaDrawerComponent): void {
        this._componentRegistry.set(name, component);
    }

    /**
     * Deregister drawer component
     *
     * @param name
     */
    deregisterComponent(name: string): void {
        this._componentRegistry.delete(name);
    }

    /**
     * Get drawer component from the registry
     *
     * @param name
     */
    getComponent(name: string): OgaDrawerComponent | undefined {
        return this._componentRegistry.get(name);
    }
}
