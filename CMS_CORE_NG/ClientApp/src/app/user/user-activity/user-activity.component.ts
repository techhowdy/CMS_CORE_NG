import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-user-activity',
    templateUrl: './user-activity.component.html',
    styleUrls: ['./user-activity.component.css']
})
export class UserActivityComponent implements OnInit {
    userActivities: any = [];

    constructor(private acct: AccountService, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.loadUserActivity();
    }

    loadUserActivity() {
        this.acct
            .getUserActivity()
            .toPromise()
            .then((result) => {
                this.userActivities = result.data;
                this.toastr.success(result.message);
            });
    }
}
