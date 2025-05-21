import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
    BehaviorSubject,
    map,
    Observable,
    of,
    switchMap,
    take,
    tap,
    throwError,
} from 'rxjs';
import { User } from './users.types';
import { environment } from 'environments/environment';
import { PaginatedApiResponse } from 'app/core/types/paginated-api-response.types';
import { Language } from 'app/core/types/languages.types';
import { IdentifierType } from 'app/core/types/identifiers.types';
import { Role } from '../roles/roles.types';

@Injectable({ providedIn: 'root' })
export class UsersService {
    // Private
    private _user: BehaviorSubject<User | null> = new BehaviorSubject(null);
    private _users: BehaviorSubject<User[] | null> = new BehaviorSubject(null);
    private _languages: BehaviorSubject<Language[] | []> = new BehaviorSubject(
        []
    );
    private _identifierTypes: BehaviorSubject<IdentifierType[] | []> =
        new BehaviorSubject([]);
    private _roles: BehaviorSubject<Role[] | []> = new BehaviorSubject([]);
    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for user
     */
    get user$(): Observable<User> {
        return this._user.asObservable();
    }

    /**
     * Getter for users
     */
    get users$(): Observable<User[]> {
        return this._users.asObservable();
    }

    /**
     * Getter for languages
     */
    get language$(): Observable<Language[]> {
        return this._languages.asObservable();
    }

    /**
     * Getter for languages
     */
    get identifierTypes$(): Observable<IdentifierType[]> {
        return this._identifierTypes.asObservable();
    }

    /**
     * Getter for roles
     */
    get roles$(): Observable<Role[]> {
        return this._roles.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get users
     */
    getUsers(): Observable<User[]> {
        return this._httpClient
            .get<PaginatedApiResponse>(
                `${environment.apiUrl}/users?Order.PropertyName=name&Order.Ascendant=true`
            )
            .pipe(
                map((response: PaginatedApiResponse) => response.data),
                tap((users: User[]) => {
                    this._users.next(users);
                })
            );
    }
    /**
     * Search users with given query
     *
     * @param query
     */
    searchUsers(query: string): Observable<User[]> {
        return this._httpClient
            .get<PaginatedApiResponse>(
                `${environment.apiUrl}/users?Order.PropertyName=name&Order.Ascendant=true&nameSurname=${query}`
            )
            .pipe(
                map((response: PaginatedApiResponse) => response.data),
                tap((users) => {
                    this._users.next(users);
                })
            );
    }

    /**
     * Get user by id
     */
    getUserById(id: number): Observable<User> {
        if (id === 0) {
            // Create a new empty user model
            const newUser: User = {
                id: 0,
                identifierTypeId: 1,
                identifierTypeDesc: null,
                identifier: '',
                name: '',
                surname: '',
                secondSurname: '',
                phoneNumber: '',
                email: '',
                passwordHash: '',
                activeChk: true,
                languageId: 3,
                languageCode: null,
                languageDesc: null,
                roles: null,
            };

            // Return the new empty user with editMode as true
            this._user.next(newUser);
            return of(newUser);
        }

        return this._users.pipe(
            take(1),
            map((users) => {
                // Find the user
                const user = users.find((item: User) => item.id === id) || null;

                // Update the user
                this._user.next(user);

                // Return the user
                return user;
            }),
            switchMap((user) => {
                if (!user) {
                    return throwError(
                        'Could not found user with id of ' + id + '!'
                    );
                }
                return of(user);
            })
        );
    }

    /**
     * Create user
     */
    createUser(user: User): Observable<User> {
        return this.users$.pipe(
            take(1),
            switchMap((users) =>
                this._httpClient
                    .post<User>(`${environment.apiUrl}/users`, user)
                    .pipe(
                        switchMap(() => this.getUsers()), // Fetch all users again after create
                        map((_) => {
                            return user;
                        })
                    )
            )
        );
    }

    /**
     * Update user
     *
     * @param id
     * @param user
     */
    updateUser(id: number, user: User): Observable<User> {
        return this.users$.pipe(
            take(1),
            switchMap((users) =>
                this._httpClient
                    .put<User>(`${environment.apiUrl}/users/${user.id}`, user)
                    .pipe(
                        map((_) => {
                            // Find the index of the updated user
                            const index = users.findIndex(
                                (item) => item.id === id
                            );
                            users[index] = user;
                            this._users.next(users);
                            return user;
                        }),
                        switchMap(() => this.getUsers()), // Fetch all users again after update
                        map((_) => {
                            this._user.next(user);
                            return user;
                        })
                    )
            )
        );
    }

    /**
     * Delete the user
     *
     * @param id
     */
    deleteUser(id: number): Observable<boolean> {
        return this.users$.pipe(
            take(1),
            switchMap((users) =>
                this._httpClient
                    .delete(`${environment.apiUrl}/users/${id}`)
                    .pipe(
                        map((isDeleted: boolean) => {
                            // Find the index of the deleted user
                            const index = users.findIndex(
                                (item: User) => item.id === id
                            );

                            // Delete the user
                            users.splice(index, 1);

                            // Update the users
                            this._users.next(users);

                            // Return the deleted status
                            return isDeleted;
                        })
                    )
            )
        );
    }

    /**
     * Get languages
     */
    getLanguages(): Observable<Language[]> {
        return this._languages.getValue().length > 0
            ? this._languages.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(`${environment.apiUrl}/languages`)
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((languages) => {
                          this._languages.next(languages);
                      })
                  );
    }

    /**
     * Get identifier types
     */
    getIdentifierTypes(): Observable<IdentifierType[]> {
        return this._identifierTypes.getValue().length > 0
            ? this._identifierTypes.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(
                      `${environment.apiUrl}/identifiertypes`
                  )
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((identifierTypes) => {
                          this._identifierTypes.next(identifierTypes);
                      })
                  );
    }

    /**
     * Get roles
     */
    getRoles(): Observable<Role[]> {
        return this._roles.getValue().length > 0
            ? this._roles.asObservable()
            : this._httpClient
                  .get<PaginatedApiResponse>(
                      `${environment.apiUrl}/roles?Order.PropertyName=order&Order.Ascendant=true`
                  )
                  .pipe(
                      map((response: PaginatedApiResponse) => response.data),
                      tap((roles) => {
                          this._roles.next(roles);
                      })
                  );
    }
}
