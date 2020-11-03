import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-contact-us',
    templateUrl: './contact-us.component.html',
    styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit {
    contactForm: FormGroup;

    constructor(private fb: FormBuilder) {}

    ngOnInit(): void {
        this.setBackgroundImage();
        this.contactForm = this.fb.group({});
    }

    onSubmit() {}

    setBackgroundImage() {
        $('body').css({
            'background-image': 'none',
            'background-repeat': 'no-repeat',
            'background-size': 'cover'
        });
    }
}
