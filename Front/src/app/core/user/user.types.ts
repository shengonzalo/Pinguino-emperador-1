export interface User {
    sub: string;
    email: string;
    roles: string[];
    uid: string;
    langcode: string;
    exp: number;
    iss: string;
    aud: string;
}
