import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';

import { Slide }           from '../models/models';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class SlideService {
    constructor(private http: Http) { }

    private slideApiUrl = 'http://localhost:16174/api/slideapi';  // URL to web API

    getSlides(): Observable<Slide[]> {
        return this.http.get(this.slideApiUrl)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getSlide(id: number): Observable<Slide> {
        return this.http.get(this.slideApiUrl + "/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    }

    addSlide(value: Slide): Observable<Slide> {
        let body = JSON.stringify({ value });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.slideApiUrl, body, options)
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
