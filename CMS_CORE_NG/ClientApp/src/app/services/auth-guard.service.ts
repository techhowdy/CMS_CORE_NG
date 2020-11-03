import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AccountService } from './account.service';
import { CookieService } from 'ngx-cookie-service';
import { Observable, BehaviorSubject } from 'rxjs';
import { ObservableStore } from '@codewithdan/observable-store';
import { StoreState } from '../interfaces/store-state';
import { take, map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService extends ObservableStore<StoreState> implements CanActivate {
    private loginStatus$ = new BehaviorSubject<boolean>(this.acct.checkLoginStatus());

    constructor(private acct: AccountService, private router: Router, private cookieService: CookieService) {
        super({ logStateChanges: true, trackStateHistory: true });

        this.acct.globalStateChanged.subscribe((state) => {
            this.loginStatus$.next(state.loggedInStatus);
        });
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.loginStatus$.pipe(
            take(1),
            map((loginStatus: boolean) => {
                const destination: string = state.url;

                // To check if user is not logged in
                if (!loginStatus) {
                    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
                    return false;
                }
                // if the user is already logged in
                switch (destination) {
                    case '/myaccount': {
                        if (this.cookieService.get('userRole') === 'Customer') {
                            return true;
                        }
                    }
                    case '/myaccount/settings': {
                        if (this.cookieService.get('userRole') === 'Customer') {
                            return true;
                        }
                    }
                    case '/myaccount/activity': {
                        if (this.cookieService.get('userRole') === 'Customer') {
                            return true;
                        }
                    }
                    default:
                        return false;
                }
            })
        );
    }
}
