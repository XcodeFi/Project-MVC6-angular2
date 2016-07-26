import { Injectable } from '@angular/core';
import { UserLogin, ChangePassword } from '../models/account';


import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class AccountService {

    private _accountRegisterAPI: string = 'api/account/register/';
    private _accountLoginAPI: string = 'http://localhost:16174/api/accountapi/login';
    private _accountLogoutAPI: string = 'http://localhost:16174/api/accountapi/logoff';
    private _accountChangePassAPI: string = 'http://localhost:16174/api/accountapi/changePassword';

    constructor(private http: Http) { }

    //register(newUser: Registration) {

    //    this.accountService.set(this._accountRegisterAPI);

    //    return this.accountService.post(JSON.stringify(newUser));
    //}

    changePassword(value: ChangePassword) {
        let body = JSON.stringify(value);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._accountChangePassAPI, body, options)
            .map(this.extractData);
    }

    isUserAuthenticated(): boolean {
        var _user: UserLogin = localStorage.getItem('user');
        if (_user != null)
            return true;
        else
            return false;
    }

    getLoggedInUser(): UserLogin {
        var _user: UserLogin;

        if (this.isUserAuthenticated()) {
            var _userData = JSON.parse(localStorage.getItem('user'));
            _user = new UserLogin(_userData.email, _userData.password,_userData.remember);
        }

        return _user;
    }
    doLogin(value: UserLogin) {
        let body = JSON.stringify(value);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._accountLoginAPI, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    doLogout() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._accountLogoutAPI, null, options)
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