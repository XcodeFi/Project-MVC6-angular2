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
var angular2_infinite_scroll_1 = require('angular2-infinite-scroll');
var CardsListComponent = (function () {
    function CardsListComponent(_cardService, _router) {
        this._cardService = _cardService;
        this._router = _router;
        this.cards = [];
        this.sum = 0;
    }
    CardsListComponent.prototype.ngOnInit = function () {
        this.getCard();
    };
    CardsListComponent.prototype.getCard = function () {
        var _this = this;
        this._cardService.getCards(0)
            .subscribe(function (cards) { return _this.cards = cards; }, function (err) { return _this.errorMessage = err; });
    };
    CardsListComponent.prototype.onScrollDown = function () {
        var _this = this;
        console.log('scrolled!!');
        this.sum += 1;
        this._cardService.getCards(this.sum)
            .subscribe(function (cards) {
            if (cards.length == 0) {
                return;
            }
            for (var _i = 0, cards_1 = cards; _i < cards_1.length; _i++) {
                var item = cards_1[_i];
                _this.cards.push(item);
            }
        }, function (err) { return _this.errorMessage = err; });
    };
    CardsListComponent.prototype.ngAfterViewInit = function () {
        $(document).ready(function () {
            //$(".toggle-social-buttons").click(function () {
            $("#shared").click(function () {
                console.log('click');
                var shareButtonRow = $(this).closest(".caption").find(".share-button-row");
                var socialButtonRow = $(this).closest(".caption").find(".social-button-row");
                if ($(shareButtonRow).hasClass("hidden")) {
                    $(shareButtonRow).removeClass("hidden");
                    $(socialButtonRow).addClass("hidden");
                }
                else {
                    $(shareButtonRow).addClass("hidden");
                    $(socialButtonRow).removeClass("hidden");
                }
            });
            $(".scroll-to-top").click(function (e) {
                e.preventDefault();
                $("html,body").animate({ scrollTop: 0 }, 500);
            });
        });
    };
    CardsListComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/cards/cards-list.component.html',
            directives: [angular2_infinite_scroll_1.InfiniteScroll],
            styles: [
                ".search-results {\n            height: 270px;\n            //overflow: scroll;\n        }"
            ],
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService, router_1.Router])
    ], CardsListComponent);
    return CardsListComponent;
}());
exports.CardsListComponent = CardsListComponent;
//# sourceMappingURL=cards-list.Component.js.map