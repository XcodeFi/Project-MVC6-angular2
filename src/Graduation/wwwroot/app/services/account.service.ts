import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';

import { Account }           from '../models/models';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class AccountService {
    constructor(private http: Http) { }

    private accountApiUrl = 'http://localhost:16174/account/logoff';  // URL to web API

    //getCards(): Observable<Card[]> {
    //    return this.http.get(this.cardApiUrl)
    //        .map(this.extractData)
    //        .catch(this.handleError);
    //}

    //getCard(id: number): Observable<Card> {
    //    return this.http.get(this.cardApiUrl + "/" + id)
    //        .map(this.extractData)
    //        .catch(this.handleError);
    //}

    Logoff(): void {
        let body = JSON.stringify({});
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        this.http.post(this.accountApiUrl, body, options);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}
