import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Register } from '../interfaces/register';
import { CookieService } from 'ngx-cookie-service';
import { map, shareReplay } from 'rxjs/operators';
import { ObservableStore } from '@codewithdan/observable-store';
import { StoreState } from '../interfaces/store-state';
import { BehaviorSubject, Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { Login } from '../interfaces/login';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AccountService extends ObservableStore<StoreState> {
    private baseUrlRegister: string = '/api/v1/account/register';
    private baseUrlAuth: string = '/api/v1/account/auth';
    private baseUrlGetProfile: string = '/api/v1/profile/getuserprofile';
    private baseUrlLogout: string = '/api/v1/account/logout';
    private baseUrlUpdateProfile: string = '/api/v1/profile/updateprofile';
    private baseUrlGetUserActivity: string = '/api/v1/profile/getuseractivity';
    private baseUrlChangePassword: string = '/api/v1/profile/changepassword';
    private baseUrlForgotPassword: string = '/api/v1/account/forgotpassword';
    private baseUrlSendExpiryNotification: string = '/api/v1/account/sessionexpirynotification';
    private baseUrlSendTwoFactorCode: string = '/api/v1/account/sendtwofactorcode';
    private baseUrlValidateNotification: string = '/api/v1/account/validatetwofactor';

    private loginStatus = new BehaviorSubject<boolean>(this.checkLoginStatus());
    private UserName = new BehaviorSubject<string>(this.cookieService.get('username'));
    private UserRole = new BehaviorSubject<string>(this.cookieService.get('userRole'));
    private Email = new BehaviorSubject<string>(null);

    private profileDetails$: Observable<any>;
    private activityDetails$: Observable<any>;

    registerModel: Register;
    loginModel: Login;

    constructor(private http: HttpClient, private cookieService: CookieService, private router: Router) {
        super({ logStateChanges: true, trackStateHistory: true });

        this.loginStatus.subscribe((result) => {
            this.setState({ loggedInStatus: result }, 'LOGGED_IN_STATUS');
        });
    }

    login(username: string, password: string, rememberMe: boolean) {
        // Check if there is an active two factor session
        let isSessionActive = localStorage.getItem('isSessionActive');

        if (isSessionActive == '0' || isSessionActive == undefined || !(isSessionActive == '1')) {
            const grantType = 'password';
            // Create a Login Model Object to send to API
            this.loginModel = {
                username: username,
                password: password,
                rememberMe: rememberMe,
                grant_type: 'password'
            };

            return this.http
                .post<any>(
                    this.baseUrlAuth,
                    { username, password, rememberMe, grantType },
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            'No-Auth': 'True',
                            'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN')
                        }
                    }
                )
                .pipe(
                    map((result) => {
                        // First we need to update some values in localstorage before we return the result to client
                        // Here we are checking if the user received an result and the result contains a auth token
                        if (result && result.token) {
                            this.loginStatus.next(true);
                            this.UserName.next(this.cookieService.get('username'));
                            this.UserRole.next(this.cookieService.get('userRole'));

                            // Next Get the Users Profile
                            this.getUserProfile()
                                .toPromise()
                                .then(() => {
                                    console.log('User Profile Fetched');
                                });
                        }
                        return result;
                    })
                );
        } else {
            Swal.fire({
                title: 'Session Active',
                text: 'Previous Session is already active. Do you want to end previous session ?',
                icon: 'info',
                showCancelButton: true,
                confirmButtonText: 'End Session'
            }).then((result) => {
                if (result.value) {
                    // Clear session
                    this.sendExpiryNotification();
                    window.location.reload();
                }
            });

            return new Observable();
        }
    }

    register(
        username: string,
        firstname: string,
        lastname: string,
        password: string,
        email: string,
        country: string,
        phone: string,
        gender: string,
        dob: string,
        terms: boolean
    ) {
        // Create a Register Model Object to send to API
        this.registerModel = {
            username: username,
            firstname: firstname,
            lastname: lastname,
            password: password,
            email: email,
            country: country,
            phone: phone,
            gender: gender,
            terms: terms,
            dob: dob
        };

        return this.http
            .post<any>(this.baseUrlRegister, this.registerModel, {
                headers: {
                    'Content-Type': 'application/json',
                    'No-Auth': 'True',
                    'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN')
                }
            })
            .pipe(
                map(
                    (result) => {
                        return result;
                    },
                    (error) => {
                        return error;
                    }
                )
            );
    }

    logout() {
        this.clearCookies();
        this.clearCache();
        this.setState({ loggedInStatus: false }, 'LOGGED_IN_STATUS');
        return this.http.get<any>(this.baseUrlLogout, {
            headers: { 'Content-Type': 'application/json', 'No-Auth': 'True' }
        });
    }

    getUserProfile(): Observable<any> {
        let params = new HttpParams().set('username', this.UserName.getValue());

        if (params.get('username') !== null) {
            if (!this.profileDetails$) {
                this.profileDetails$ = this.http
                    .get<any>(this.baseUrlGetProfile + '/' + this.UserName.getValue(), { params: params })
                    .pipe(
                        shareReplay(),
                        map(
                            (result) => {
                                if (result.email) {
                                    this.Email.next(result.email);
                                }
                                return result;
                            },
                            (error) => {
                                return new Observable<Error>();
                            }
                        )
                    );
            }
            return this.profileDetails$;
        } else {
            this.router.navigate(['/login']);
            return new Observable<Error>();
        }
    }

    updateProfile(userDetails: any) {
        const formdata = new FormData();
        let params = new HttpParams().set('username', this.UserName.getValue());

        for (const key of Object.keys(userDetails)) {
            const value = userDetails[key];
            formdata.append(key, value);
        }
        return this.http
            .post<any>(this.baseUrlUpdateProfile, formdata, {
                headers: { Accept: 'multipart/form-data', 'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN') },
                params: params
            })
            .pipe(
                map((result) => {
                    this.clearCache();
                    return result;
                })
            );
    }

    getUserActivity(): Observable<any> {
        let params = new HttpParams().set('username', this.UserName.getValue());

        if (params.get('username') !== null) {
            this.activityDetails$ = this.http
                .get<any>(this.baseUrlGetUserActivity + '/' + this.UserName.getValue(), { params: params })
                .pipe(
                    map(
                        (result) => {
                            return result;
                        },
                        (error) => {
                            return new Observable<Error>();
                        }
                    )
                );
            return this.activityDetails$;
        } else {
            this.router.navigate(['/login']);
            return new Observable<Error>();
        }
    }

    changePassword(userDetails: any) {
        const resetPasswordViewModel = {
            OldPassword: userDetails.oldPassword,
            Password: userDetails.newPassword,
            ConfirmPassword: userDetails.newPassword,
            Email: userDetails.email
        };

        return this.http
            .post<any>(this.baseUrlChangePassword, resetPasswordViewModel, {
                headers: { Accept: 'multipart/form-data', 'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN') }
            })
            .pipe(
                map(
                    (result) => {
                        return result;
                    },
                    (error) => {
                        return error;
                    }
                )
            );
    }

    sendForgotPasswordEmail(email: string) {
        return this.http
            .post<any>(
                this.baseUrlForgotPassword + '/' + email,
                {},
                {
                    headers: { Accept: 'application/json', 'No-Auth': 'True', 'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN') }
                }
            )
            .pipe(
                map(
                    (result) => {
                        return result;
                    },
                    (error) => {
                        return error;
                    }
                )
            );
    }

    sendExpiryNotification() {
        let userId = localStorage.getItem('user_id');
        this.clearCookies();
        if (userId) {
            return this.http
                .post<any>(this.baseUrlSendExpiryNotification + `/${userId}`, {
                    headers: { 'Content-Type': 'application/json', 'No-Auth': 'True', 'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN') }
                })
                .subscribe(
                    (result) => {
                        console.log(result);
                    },
                    (error) => {
                        console.log(error);
                    }
                );
        }
    }

    sendTwoFactorProvider(providerType: string, rememberDevice: boolean) {
        let sessionExpiryTime = localStorage.getItem('codeExpiry');
        let twoFactorToken = localStorage.getItem('twoFactorToken');

        if (sessionExpiryTime !== undefined) {
            const endTime = new Date(sessionExpiryTime).getTime();
            const currentTime = new Date().getTime();
            if (currentTime > endTime) {
                console.log('Your Session Expired!!!');
                return new Observable<Error>();
            } else {
                return this.http
                    .post<any>(
                        this.baseUrlSendTwoFactorCode,
                        { providerType, rememberDevice, sessionExpiryTime, twoFactorToken },
                        {
                            headers: {
                                Accept: 'application/json',
                                user_id: localStorage.getItem('user_id'),
                                'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN')
                            }
                        }
                    )
                    .pipe(
                        map((result) => {
                            if (result) {
                            }
                            return result;
                        })
                    );
            }
        }
    }

    validateTwoFactorCode(code: string) {
        return this.http
            .post<any>(this.baseUrlValidateNotification, JSON.stringify(code), {
                headers: {
                    'Content-Type': 'application/json',
                    twoFactorToken: localStorage.getItem('twoFactorToken'),
                    'X-XSRF-TOKEN': this.cookieService.get('XSRF-TOKEN')
                }
            })
            .pipe(
                map(
                    (result) => {
                        if (result && result.token) {
                            // store user details and jwt token in local storage to keep user logged in between page refreshes
                            this.loginStatus.next(true);

                            this.UserName.next(this.cookieService.get('username'));
                            this.UserRole.next(this.cookieService.get('userRole'));

                            // Clear the two factor session as user code validated
                            localStorage.removeItem('user_id');
                            this.sendExpiryNotification();
                        }
                        return result;
                    },
                    (error) => {
                        return error;
                    }
                )
            );
    }

    checkLoginStatus(): boolean {
        let loginCookie = this.cookieService.get('loginStatus');

        return loginCookie == '1';
    }

    clearCache() {
        this.profileDetails$ = null;
    }

    clearCookies() {
        localStorage.removeItem('twoFactorToken');
        localStorage.removeItem('codeExpiry');
        localStorage.removeItem('isSessionActive');
        localStorage.removeItem('attemptsRemaining');
        localStorage.removeItem('codeSendSuccess');
        localStorage.removeItem('user_id');
    }

    get currentUserName() {
        return this.UserName.asObservable();
    }
}
