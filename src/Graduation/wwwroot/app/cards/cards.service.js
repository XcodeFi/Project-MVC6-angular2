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
var CateService = (function () {
    function CateService(http) {
        this.http = http;
        this.cateApiUrl = 'http://localhost:16174/api/CateApi'; // URL to web API
    }
    CateService.prototype.getCates = function () {
        return this.http.get(this.cateApiUrl)
            .map(this.extractData)
            .catch(this.handleError);
    };
    //get cate child
    CateService.prototype.getChildCates = function () {
        return this.http.get(this.cateApiUrl + '/getChild')
            .map(this.extractData)
            .catch(this.handleError);
    };
    CateService.prototype.getCate = function (id) {
        return this.http.get(this.cateApiUrl + "/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CateService.prototype.getCateUrl = function (url) {
        return this.http.get(this.cateApiUrl + "/geturl/" + url)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CateService.prototype.addCate = function (value) {
        var body = JSON.stringify(value);
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this.cateApiUrl, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CateService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    CateService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        var errMsg = (error.message) ? error.message :
            error.status ? error.status + " - " + error.statusText : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable_1.Observable.throw(errMsg);
    };
    CateService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], CateService);
    return CateService;
}());
exports.CateService = CateService;
//# sourceMappingURL=cards.service.js.map