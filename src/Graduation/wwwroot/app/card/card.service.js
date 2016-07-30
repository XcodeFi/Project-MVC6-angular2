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
var CardService = (function () {
    function CardService(http) {
        this.http = http;
        this.cardApiUrl = 'http://localhost:16174/api/cardapi'; // URL to web API
    }
    //for anyone can get
    CardService.prototype.getCards = function () {
        return this.http.get(this.cardApiUrl + "/getNative")
            .map(this.extractData)
            .catch(this.handleError);
    };
    //for user
    CardService.prototype.getCardsForUser = function (id) {
        return this.http.get(this.cardApiUrl + "/getMyCard/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CardService.prototype.getCard = function (id) {
        return this.http.get(this.cardApiUrl + "/" + id)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CardService.prototype.getCardUrl = function (urlSlug) {
        return this.http.get(this.cardApiUrl + "/geturl/" + urlSlug)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CardService.prototype.deleteCard = function (id, userId) {
        return this.http.delete(this.cardApiUrl + "/user/" + id + "/" + userId)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CardService.prototype.addCard = function (value) {
        var body = JSON.stringify({ value: value });
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        var options = new http_2.RequestOptions({ headers: headers });
        return this.http.post(this.cardApiUrl, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    };
    CardService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    CardService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        var errMsg = (error.message) ? error.message :
            error.status ? error.status + " - " + error.statusText : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable_1.Observable.throw(errMsg);
    };
    CardService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], CardService);
    return CardService;
}());
exports.CardService = CardService;
//# sourceMappingURL=card.service.js.map