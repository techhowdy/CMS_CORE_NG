import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription, timer } from 'rxjs';
import { takeWhile, tap } from 'rxjs/operators';

@Component({
    selector: 'app-send-code',
    templateUrl: './send-code.component.html',
    styleUrls: ['./send-code.component.css']
})
export class SendCodeComponent implements OnInit {
    imageUrl: string = '/assets/images/bg-register.jpeg';
    insertForm: FormGroup;
    provider: FormControl;
    rememberDevice: FormControl;
    returnUrl: string;
    interval: any = null;
    private subscription: Subscription;

    constructor(private acct: AccountService, private route: ActivatedRoute, private fb: FormBuilder, private router: Router) {}

    ngOnInit(): void {
        this.setBackgroundImage();

        this.observerTimer();

        if ((localStorage.getItem('twoFactorToken') && localStorage.getItem('codeExpiry')) == null) {
            this.router.navigate(['/login']);
        }
        // Initialize Form Controls
        this.provider = new FormControl('', [Validators.required]);
        this.rememberDevice = new FormControl(false);

        this.insertForm = this.fb.group({
            provider: this.provider,
            rememberDevice: this.rememberDevice
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    /* Set the background image when page loads */
    setBackgroundImage() {
        $('body').css({
            'background-image': 'url(' + this.imageUrl + '),  linear-gradient(rgba(255, 0, 0, 0.5), rgba(255, 0, 0, 0.5))',
            'background-repeat': 'no-repeat',
            'background-size': 'cover'
        });
    }

    /* Observable timer */
    observerTimer() {
        let endTime = new Date(localStorage.getItem('codeExpiry')).getTime();
        let currentTime = new Date().getTime();
        let difference = endTime - currentTime;
        let seconds = Math.floor(difference / 1000);

        this.subscription = timer(1000, 1000) //Initial delay 1 seconds and interval countdown also 1 second
            .pipe(
                takeWhile(() => seconds > 0),
                tap(() => seconds--)
            )
            .subscribe(() => {
                $('#countDown').text(seconds + 's');
                let isSessionActive = localStorage.getItem('isSessionActive');
                if (seconds <= 0 || isSessionActive == '0' || isSessionActive == undefined || !(isSessionActive == '1')) {
                    $('#Provider').attr('disabled', '');
                    $('#countDown').text('EXPIRED');
                    $('#btnLogin').removeAttr('hidden');
                    this.stopTimer();
                    this.acct.sendExpiryNotification();
                }
            });
    }

    private stopTimer() {
        this.subscription.unsubscribe();
    }

    onSubmit() {
        let sendCodeDetails = this.insertForm.value;
        this.acct.sendTwoFactorProvider(sendCodeDetails.provider, sendCodeDetails.rememberDevice).subscribe(
            (result) => {
                this.router.navigate(['/validate-code']);
            },
            (error) => {
                console.log(error);
            }
        );
    }
}
