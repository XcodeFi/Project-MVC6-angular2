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
var card_service_1 = require('./card.service');
var cards_service_1 = require('../cards/cards.service');
var CardDetailComponent = (function () {
    function CardDetailComponent(_cardService, _cateService, route, router) {
        this._cardService = _cardService;
        this._cateService = _cateService;
        this.route = route;
        this.router = router;
        this.navigated = false; // true if navigated here
    }
    CardDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            var id = +params['id'];
            if (id !== undefined) {
                _this.navigated = true;
                _this._cardService.getCard(id).subscribe(function (card) {
                    _this.card = card;
                });
            }
            else {
                _this.navigated = false;
            }
        });
    };
    CardDetailComponent.prototype.goBack = function () {
        window.history.back();
    };
    CardDetailComponent.prototype.ngAfterViewInit = function () {
        //$(function () {
        //    $('#eSendDate').datetimepicker({ format: 'MM/DD/YYYY', defaultDate: moment() });
        //});
        //$(".toggle-social-buttons").click(function () {
        //    var shareButtonRow = $(this).closest(".caption").find(".share-button-row");
        //    var socialButtonRow = $(this).closest(".caption").find(".social-button-row");
        //    if ($(shareButtonRow).hasClass("hidden")) {
        //        $(shareButtonRow).removeClass("hidden");
        //        $(socialButtonRow).addClass("hidden");
        //    }
        //    else {
        //        $(shareButtonRow).addClass("hidden");
        //        $(socialButtonRow).removeClass("hidden");
        //    }
        //});
        //$("fb-root").html=("<script>" +
        //                    "(function (d, s, id) {"+
        //                        var js, fjs = d.getElementsByTagName(s)[0];
        //                        if (d.getElementById(id)) return;
        //                        js = d.createElement(s); js.id = id;
        //                        js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5&appId=467222143374291";
        //                        fjs.parentNode.insertBefore(js, fjs);
        //                    }(document, 'script', 'facebook-jssdk'));</script>";
    };
    CardDetailComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/card/card-detail.component.html',
            providers: [card_service_1.CardService]
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService, cards_service_1.CateService, router_1.ActivatedRoute, router_1.Router])
    ], CardDetailComponent);
    return CardDetailComponent;
}());
exports.CardDetailComponent = CardDetailComponent;
//# sourceMappingURL=card-detail.component.js.map