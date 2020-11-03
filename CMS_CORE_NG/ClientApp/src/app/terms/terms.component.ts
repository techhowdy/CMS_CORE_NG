import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-terms',
    templateUrl: './terms.component.html',
    styleUrls: ['./terms.component.css']
})
export class TermsComponent implements OnInit {
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
