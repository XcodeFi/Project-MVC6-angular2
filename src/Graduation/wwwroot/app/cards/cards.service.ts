import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';

import { Cate, CateDetail }           from '../models/models';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class CateService {
    constructor(private http: Http) { }

     private cateApiUrl = 'http://localhost:16174/api/CateApi';  // URL to web API


    getCates(): Observable<Cate[]> {
        return this.http.get(this.cateApiUrl)
            .map(this.extractData)
            .catch(this.handleError);
    }
    //get cate child
    getChildCates(): Observable<Cate[]> {
        return this.http.get(this.cateApiUrl+'/getChild')
            .map(this.extractData)
            .catch(this.handleError);
    }

    getCate(id: number): Observable<CateDetail> {
        return this.http.get(this.cateApiUrl + "/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    }


    getCateUrl(url: string): Observable<CateDetail> {
        return this.http.get(this.cateApiUrl + "/geturl/" + url)
            .map(this.extractData)
            .catch(this.handleError);
    }

    
    addCate(value: Cate): Observable<Cate> {
        let body = JSON.stringify(value);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.cateApiUrl, body, options)
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
