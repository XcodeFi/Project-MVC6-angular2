"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_1 = require('@angular/http');
var http_2 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
require('rxjs/add/operator/map');
require('rxjs/add/operator/catch');
var AccountService = (function () {
    function AccountService(http) {
        this.http = http;
        this._accountApi = 'http://localhost:16174/api/accountapi/';
        this._accountUserInfo = 'http://localhost:16174/api/accountapi/CurrentUserInfo';
        this._accountLoginAPI = 'http://localhost:16174/api/accountapi/login';
        this._accountLogoutAPI = 'http://localhost:16174/api/accountapi/logoff';
        this._accountChangePassAPI = 'http://localhost:16174/api/accountapi/changePassword';
    }
    //register(newUser: Registration) {
    //    this.accountService.set(this._accountRegisterAPI);
    //    return this.accountService.post(JSON.stringify(newUser));
    //}
    //get user info
    AccountService.prototype.getUserInfo = function () {
        return this.http.get(this._accountUserInfo).map(this.extractData)
            .catch(this.handleError);
    };
    AccountService.prototype.changePassword = function (value) {
        var body = JSON.stringify(value);
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this._accountChangePassAPI, body, options)
            .map(this.extractData).catch(this.handleError);
    };
    AccountService.prototype.isSignIn = function () {
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this._accountApi + "isSignIn", null, options)
            .map(this.extractData).catch(this.handleError);
    };
    AccountService.prototype.isUserAuthenticated = function () {
        return this.isSignIn().subscribe(function (res) {
            return res;
        });
    };
    AccountService.prototype.getLogInUser = function () {
        if (this.isUserAuthenticated()) {
            return this.getUserInfo().subscribe(function (res) {
                return JSON.stringify(res.id, res.email, res.usename);
            });
        }
        else {
            return null;
        }
    };
    AccountService.prototype.doLogin = function (value) {
        var body = JSON.stringify(value);
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this._accountLoginAPI, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    };
    AccountService.prototype.doLogout = function () {
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this._accountLogoutAPI, null, options)
            .map(this.extractData)
            .catch(this.handleError);
    };
    AccountService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    AccountService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        var errMsg = (error.message) ? error.message :
            error.status ? error.status + " - " + error.statusText : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable_1.Observable.throw(errMsg);
    };
    AccountService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], AccountService);
    return AccountService;
}());
exports.AccountService = AccountService;
//# sourceMappingURL=account.service.js.map