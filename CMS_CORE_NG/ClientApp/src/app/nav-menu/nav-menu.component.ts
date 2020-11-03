import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    LoginStatus$ = new BehaviorSubject<boolean>(null);
    Username$: Observable<string>;

    constructor(private acct: AccountService, private router: Router) {}

    ngOnInit(): void {
        this.acct.globalStateChanged.subscribe((state) => {
            this.LoginStatus$.next(state.loggedInStatus);
        });

        this.Username$ = this.acct.currentUserName;
    }

    onLogout() {
        this.acct.logout().subscribe((result) => {
            console.log('Logged Out Successfully');
        });

        this.router.navigate(['/login']);
    }
}
