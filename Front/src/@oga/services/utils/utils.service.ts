import { Injectable } from '@angular/core';
import { IsActiveMatchOptions } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class OgaUtilsService {
    /**
     * Constructor
     */
    constructor() {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get the equivalent "IsActiveMatchOptions" options for "exact = true".
     */
    get exactMatchOptions(): IsActiveMatchOptions {
        return {
            paths: 'exact',
            fragment: 'ignored',
            matrixParams: 'ignored',
            queryParams: 'exact',
        };
    }

    /**
     * Get the equivalent "IsActiveMatchOptions" options for "exact = false".
     */
    get subsetMatchOptions(): IsActiveMatchOptions {
        return {
            paths: 'subset',
            fragment: 'ignored',
            matrixParams: 'ignored',
            queryParams: 'subset',
        };
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Generates a random id
     *
     * @param length
     */
    randomId(length: number = 10): string {
        const chars =
            'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        let name = '';
        const array = new Uint32Array(length);

        window.crypto.getRandomValues(array);

        for (let i = 0; i < length; i++) {
            name += chars.charAt(array[i] % chars.length);
        }

        return name;
    }
}
