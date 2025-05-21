import { Role } from '../roles/roles.types';

export interface User {
    id: number;
    identifierTypeId: number;
    identifierTypeDesc: null;
    identifier: string;
    name: string;
    surname: string;
    secondSurname: null | string;
    phoneNumber: null | string;
    email: string;
    passwordHash: string;
    activeChk: boolean;
    languageId: number;
    languageCode: null | string;
    languageDesc: null | string;
    roles: Role[];
}
