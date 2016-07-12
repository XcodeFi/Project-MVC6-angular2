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
// Observable Version
var core_1 = require('@angular/core');
var card_service_1 = require('../services/card.service');
var CardListComponent = (function () {
    function CardListComponent(_cardService) {
        this._cardService = _cardService;
        this.mode = 'Observable';
    }
    CardListComponent.prototype.ngOnInit = function () { this.getCards(); };
    CardListComponent.prototype.getCards = function () {
        var _this = this;
        this._cardService.getCards()
            .subscribe(function (cards) { return _this.cards = cards; }, function (error) { return _this.errorMessage = error; });
    };
    CardListComponent = __decorate([
        core_1.Component({
            selector: 'card-list',
            //templateUrl: 'cates/index',
            templateUrl: 'cates/cate',
            styleUrls: ['css/cate.component.css']
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService])
    ], CardListComponent);
    return CardListComponent;
}());
exports.CardListComponent = CardListComponent;
//# sourceMappingURL=card.component.js.map