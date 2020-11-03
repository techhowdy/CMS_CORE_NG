import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class ValidatorService {
    constructor() {}

    MustMatch(firstControl: AbstractControl): ValidatorFn {
        return (
            secondControl: AbstractControl
        ): { [key: string]: boolean } | null => {
            // return null if controls haven't initialised yet
            if (!firstControl && !secondControl) {
                return null;
            }

            // return null if another validator has already found an error on the matchingControl
            if (secondControl.hasError && !firstControl.hasError) {
                return null;
            }
            // set error on matchingControl if validation fails
            if (firstControl.value !== secondControl.value) {
                return { mustMatch: true };
            } else {
                return null;
            }
        };
    }
}
