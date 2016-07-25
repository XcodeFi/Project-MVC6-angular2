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
var router_1 = require('@angular/router');
var card_service_1 = require('../card/card.service');
var CardsListComponent = (function () {
    function CardsListComponent(_cardService, _router) {
        this._cardService = _cardService;
        this._router = _router;
        this.cards = [];
    }
    CardsListComponent.prototype.ngOnInit = function () {
        this.getCard();
    };
    CardsListComponent.prototype.getCard = function () {
        var _this = this;
        this._cardService.getCards()
            .subscribe(function (cards) { return _this.cards = cards; }, function (err) { return _this.errorMessage = err; });
    };
    CardsListComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/cards/cards-list.component.html'
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService, router_1.Router])
    ], CardsListComponent);
    return CardsListComponent;
}());
exports.CardsListComponent = CardsListComponent;
//# sourceMappingURL=cards-list.Component.js.map