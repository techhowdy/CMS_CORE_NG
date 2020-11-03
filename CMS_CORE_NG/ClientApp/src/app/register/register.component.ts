import { Component, OnInit } from '@angular/core';
import { CountryService } from '../services/country.service';
import { Observable } from 'rxjs';
import { Country } from '../interfaces/country';
import {
    FormGroup,
    FormBuilder,
    FormControl,
    Validators
} from '@angular/forms';
import { ValidatorService } from '../services/common/validator.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

declare let $: any;

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    imageUrl: string = '/assets/images/bg-register.jpeg';

    country$: Observable<Country[]>;
    countries: Country[];
    registerForm: FormGroup;
    errorList: string[];
    modalMessage: string;
    modalTitle: string;
    isRegistrationInProcess: boolean = false;
    username: FormControl;
    firstname: FormControl;
    lastname: FormControl;
    email: FormControl;
    confirmEmail: FormControl;
    password: FormControl;
    cpassword: FormControl;
    country: FormControl;
    phone: FormControl;
    gender: FormControl;
    dob: FormControl;
    terms: FormControl;

    constructor(
        private countryservice: CountryService,
        private fb: FormBuilder,
        private validatorService: ValidatorService,
        public toastrservice: ToastrService,
        private acct: AccountService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.username = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(5)
        ]);
        this.firstname = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(5)
        ]);
        this.lastname = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(5)
        ]);
        this.email = new FormControl('', [
            Validators.required,
            Validators.email
        ]);
        this.confirmEmail = new FormControl('', [
            Validators.required,
            this.validatorService.MustMatch(this.email)
        ]);
        this.password = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(5)
        ]);
        this.cpassword = new FormControl('', [
            Validators.required,
            this.validatorService.MustMatch(this.password)
        ]);
        this.country = new FormControl('', [Validators.required]);
        this.phone = new FormControl('', [
            Validators.required,
            Validators.pattern(
                '^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$'
            )
        ]);
        this.gender = new FormControl('', [Validators.required]);
        this.dob = new FormControl('', [Validators.required]);
        this.terms = new FormControl('', [Validators.required]);

        this.registerForm = this.fb.group({
            username: this.username,
            firstname: this.firstname,
            lastname: this.lastname,
            email: this.email,
            confirmEmail: this.confirmEmail,
            password: this.password,
            cpassword: this.cpassword,
            country: this.country,
            phone: this.phone,
            gender: this.gender,
            dob: this.dob,
            terms: this.terms
        });

        this.setbackgroundImage();
        this.loadCountries();

        $(() => {
            $('#datepicker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '1920:2099',
                onSelect: (dateText) => {
                    this.dob.setValue(dateText);
                }
            });
        });
    }

    onSubmit() {
        this.isRegistrationInProcess = true;
        let userDetails = this.registerForm.value;
        this.errorList = [];
        this.acct
            .register(
                userDetails.username,
                userDetails.firstname,
                userDetails.lastname,
                userDetails.password,
                userDetails.email,
                userDetails.country,
                userDetails.phone,
                userDetails.gender,
                userDetails.dob,
                userDetails.terms
            )
            .subscribe(
                (result) => {
                    if (result.message == 'Registration Successful') {
                        let timerInterval;
                        Swal.fire({
                            title: 'Registration was Successful!',
                            html:
                                'Please check your email for verification email.<br>Redirecting to login page <b></b>.',
                            timer: 10000,
                            allowOutsideClick: false,
                            onBeforeOpen: () => {
                                Swal.showLoading();
                                timerInterval = setInterval(() => {
                                    Swal.getContent().querySelector(
                                        'b'
                                    ).textContent = String(
                                        (Swal.getTimerLeft() / 1000).toFixed(0)
                                    );
                                }, 100);
                            },
                            onClose: () => {
                                clearInterval(timerInterval);
                            }
                        }).then((r) => {
                            Swal.stopTimer();
                            this.router.navigate(['/login']);
                        });
                        this.isRegistrationInProcess = false;
                    }
                },
                (error) => {
                    if (error.status == 500) {
                        this.toastrservice.info(
                            'An error occured while processing this request. Check details or Try again.',
                            '',
                            { positionClass: 'toast-top-right' }
                        );
                    }
                    if (error.error && error.error.value) {
                        this.errorList = [];
                        for (let i = 0; i < error.error.value.length; i++) {
                            this.errorList.push(error.error.value[i]);
                        }
                        this.showModalError();
                    }

                    this.isRegistrationInProcess = false;
                }
            );
    }

    showModalError() {
        this.modalTitle = 'Registration Error';
        this.modalMessage = 'Your Registration was Unsuccessful';
        $('#errorModal').modal('show');
    }

    setbackgroundImage() {
        $('body').css({
            'background-image': 'url(' + this.imageUrl + ')',
            'background-size': 'cover'
        });
    }

    loadCountries() {
        this.country$ = this.countryservice.getCountries();
        this.country$.subscribe((countrylist) => {
            this.countries = countrylist;
        });
    }
}
