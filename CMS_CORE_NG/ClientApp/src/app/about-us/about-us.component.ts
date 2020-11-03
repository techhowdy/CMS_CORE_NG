import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-about-us',
    templateUrl: './about-us.component.html',
    styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {
    constructor() {}

    ngOnInit(): void {
        this.setBackgroundImage();
    }

    setBackgroundImage() {
        $('body').css({
            'background-image': 'none',
            'background-repeat': 'no-repeat',
            'background-size': 'cover'
        });
    }
}
