import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
    imageUrl: string = '/assets/images/bg-register.jpeg';
    insertForm: FormGroup;
    Email: FormControl;

    constructor(private acct: AccountService, private fb: FormBuilder, private toasterService: ToastrService) {}

    ngOnInit(): void {
        this.setBackgroundImage();

        // Initialize Controls
        this.Email = new FormControl('', [Validators.required, Validators.email]);

        this.insertForm = this.fb.group({
            Email: this.Email
        });
    }

    onSubmit() {
        let userInfo = this.insertForm.value;
        this.acct.sendForgotPasswordEmail(userInfo.Email).subscribe(
            (result) => {
                if (result && result.message == 'Success') {
                    $('#forgotPassCard').html('');
                    $('#forgotPassCard').append(
                        "<div class='alert alert-success show'>" +
                            '<strong>Success!</strong> Please check your email for password reset instructions.' +
                            '</div>'
                    );
                }
            },
            (error) => {
                this.toasterService.error('An error occured while processing this request.', '', { positionClass: 'toast-top-right' });
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
