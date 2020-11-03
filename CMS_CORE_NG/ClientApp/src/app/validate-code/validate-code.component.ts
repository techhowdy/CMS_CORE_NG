import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-validate-code',
    templateUrl: './validate-code.component.html',
    styleUrls: ['./validate-code.component.css']
})
export class ValidateCodeComponent implements OnInit {
    imageUrl: string = '/assets/images/bg-register.jpeg';
    insertForm: FormGroup;
    code: FormControl;
    returnUrl: string;
    ErrorMessage: string;
    invalidLogin: boolean;
    attemptsRemaining: string = '3';

    constructor(
        private fb: FormBuilder,
        private acct: AccountService,
        private router: Router,
        private route: ActivatedRoute,
        public toasterService: ToastrService
    ) {}

    ngOnInit(): void {
        this.code = new FormControl('', [Validators.required]);
        this.setBackgroundImage();
        this.insertForm = this.fb.group({
            code: this.code
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    onSubmit() {
        let validateCodeDetails = this.insertForm.value;
        this.acct.validateTwoFactorCode(validateCodeDetails.code).subscribe(
            (result) => {
                $('body').css({ 'background-image': 'none' });
                this.invalidLogin = false;
                this.router.navigateByUrl(this.returnUrl);
            },
            (error) => {
                this.invalidLogin = true;
                this.ErrorMessage = error.error;

                if ((<any>this.ErrorMessage).loginError == 'Code-InValid') {
                    if ((<any>this.ErrorMessage).attemptsRemaining == 0) {
                        this.acct.sendExpiryNotification();
                        let timerInterval;

                        Swal.fire({
                            title: 'Your Account is Locked!',
                            html: 'You will be redirected in <b></b> seconds.',
                            timer: 10000,
                            allowOutsideClick: false,
                            onBeforeOpen: () => {
                                timerInterval = setInterval(() => {
                                    const content = Swal.getContent();
                                    if (content) {
                                        const b = content.querySelector('b');
                                        if (b) {
                                            b.textContent = String((Swal.getTimerLeft() / 1000).toFixed());
                                        }
                                    }
                                }, 100);
                            },
                            onClose: () => {
                                clearInterval(timerInterval);
                            }
                        }).then((result) => {
                            if (result.dismiss === Swal.DismissReason.timer) {
                            }
                            window.location.href = '/login';
                        });
                    } else {
                        this.attemptsRemaining = (<any>this.ErrorMessage).attemptsRemaining;
                        this.toasterService.warning('Invalid Code', '', { positionClass: 'toast-top-right' });
                    }
                } else {
                    this.toasterService.error('Request cannot be processed at the moment.  Try again later.', '', {
                        positionClass: 'toast-top-right'
                    });
                }
            }
        );
    }

    /* Set the background image when page loads */
    setBackgroundImage() {
        $('body').css({
            'background-image': 'url(' + this.imageUrl + '),  linear-gradient(rgba(255, 0, 0, 0.5), rgba(255, 0, 0, 0.5))',
            'background-repeat': 'no-repeat',
            'background-size': 'cover'
        });
    }
}
