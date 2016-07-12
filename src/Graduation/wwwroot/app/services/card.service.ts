import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';

import { Card }           from '../models/models';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class CardService {
    constructor(private http: Http) { }

    private cardApiUrl = 'http://localhost:16174/api/cardapi';  // URL to web API

    getCards(): Observable<Card[]> {
        return this.http.get(this.cardApiUrl)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getCard(id: number): Observable<Card> {
        return this.http.get(this.cardApiUrl + "/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    }

    addCard(value: Card): Observable<Card> {
        let body = JSON.stringify({ value });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.cardApiUrl, body, options)
            .map(this.extractData)
            .catch(this.handleError);
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
