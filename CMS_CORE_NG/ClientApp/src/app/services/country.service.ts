import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay, map } from 'rxjs/operators';
import { Country } from '../interfaces/country';

@Injectable({
    providedIn: 'root'
})
export class CountryService {
    private countryListUrl: string = '/api/v1/country/getcountries';

    private country$: Observable<Country[]>;

    constructor(private http: HttpClient) {}

    getCountries() {
        if (!this.country$) {
            this.country$ = this.http.get<any>(this.countryListUrl).pipe(
                shareReplay(),
                map((result) => {
                    return result;
                })
            );
        }

        // if countries cache exists return it
        return this.country$;
    }
}
