// -----------------------------------------------------------------------------------------------------
// @ AUTH UTILITIES
//
// Methods are derivations of the Auth0 Angular-JWT helper service methods
// https://github.com/auth0/angular2-jwt
// -----------------------------------------------------------------------------------------------------

export class AuthUtils {
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Is token expired?
     *
     * @param token
     * @param offsetSeconds
     */
    static isTokenExpired(token: string, offsetSeconds?: number): boolean {
        // Return if there is no token
        if (!token || token === '') {
            return true;
        }

        // Get the expiration date
        const date = this._getTokenExpirationDate(token);

        offsetSeconds = offsetSeconds || 0;

        if (date === null) {
            return true;
        }

        // Check if the token is expired
        return date.valueOf() <= new Date().valueOf() + offsetSeconds * 1000;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Base64 decoder
     * Credits: https://github.com/atk
     *
     * @param str
     * @private
     */
    /**
     * Decodes a Base64 encoded string.
     *
     * @param {string} str - The Base64 encoded string to decode.
     * @returns {string} - The decoded string.
     * @throws {Error} - Throws an error if the string is not correctly encoded.
     */
    private static _b64decode(str: string): string {
        const chars =
            'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';
        let output = '';

        // Convert to string in case the input is "falsy" or not a string
        str = String(str);
        // Remove trailing '=' characters from the string without using regex
        str = this._removeTrailingEquals(str);

        if (str.length % 4 === 1) {
            throw new Error(
                "'atob' failed: The string to be decoded is not correctly encoded."
            );
        }

        /* eslint-disable */
        for (
            let bc = 0, bs: any, buffer: any, idx = 0;
            idx < str.length;
            idx++
        ) {
            buffer = chars.indexOf(str.charAt(idx));
            if (~buffer) {
                bs = bc % 4 ? bs * 64 + buffer : buffer;
                if (bc++ % 4) {
                    output += String.fromCharCode(
                        255 & (bs >> ((-2 * bc) & 6))
                    );
                }
            }
        }
        /* eslint-enable */

        return output;
    }

    /**
     * Manually removes trailing '=' characters from the string.
     * Equivalent to replace(/=+$/, '') without using regex.
     *
     * @param {string} str - The string from which to remove trailing '=' characters.
     * @returns {string} - The string without trailing '=' characters.
     */
    private static _removeTrailingEquals(str: string): string {
        let i = str.length - 1;
        while (i >= 0 && str[i] === '=') {
            i--;
        }
        return str.substring(0, i + 1);
    }

    /**
     * Base64 unicode decoder
     *
     * @param str
     * @private
     */
    private static _b64DecodeUnicode(str: any): string {
        return decodeURIComponent(
            Array.prototype.map
                .call(
                    this._b64decode(str),
                    (c: any) =>
                        '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
                )
                .join('')
        );
    }

    /**
     * URL Base 64 decoder
     *
     * @param str
     * @private
     */
    static urlBase64Decode(str: string): string {
        let output = str.replace(/-/g, '+').replace(/_/g, '/');
        switch (output.length % 4) {
            case 0: {
                break;
            }
            case 2: {
                output += '==';
                break;
            }
            case 3: {
                output += '=';
                break;
            }
            default: {
                throw Error('Illegal base64url string!');
            }
        }
        return this._b64DecodeUnicode(output);
    }

    /**
     * Decodes a JWT token.
     *
     * @param {string} token - The JWT token to decode.
     * @returns {any} - The decoded token payload.
     */
    static decodeToken(token: string): any {
        // Return if there is no token
        if (!token) {
            return null;
        }

        // Split the token
        const parts = token.split('.');

        if (parts.length !== 3) {
            throw new Error(
                "The inspected token doesn't appear to be a JWT. Check to make sure it has three parts and see https://jwt.io for more."
            );
        }

        // Decode the token using the Base64 decoder
        const decoded = this.urlBase64Decode(parts[1]);

        if (!decoded) {
            throw new Error('Cannot decode the token.');
        }

        return JSON.parse(decoded);
    }

    /**
     * Get token expiration date
     *
     * @param token
     * @private
     */
    private static _getTokenExpirationDate(token: string): Date | null {
        // Get the decoded token
        const decodedToken = this.decodeToken(token);

        // Return if the decodedToken doesn't have an 'exp' field
        if (!decodedToken.hasOwnProperty('exp')) {
            return null;
        }

        // Convert the expiration date
        const date = new Date(0);
        date.setUTCSeconds(decodedToken.exp);

        return date;
    }
}
