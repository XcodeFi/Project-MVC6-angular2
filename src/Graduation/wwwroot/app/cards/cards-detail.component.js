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
var cards_service_1 = require('./cards.service');
var CardsDetailComponent = (function () {
    function CardsDetailComponent(_cardService, _router, _cateService, route) {
        this._cardService = _cardService;
        this._router = _router;
        this._cateService = _cateService;
        this.route = route;
        this.cards = [];
    }
    CardsDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.sub = this.route
            .params
            .subscribe(function (params) {
            var url = params['url'];
            _this._id = url;
            _this._cateService.getCateUrl(url).subscribe(function (cate) {
                if (cate) {
                    _this.cateName = cate.name;
                    _this.cards = cate.cards;
                }
                else {
                    _this._router.navigate(['/**']);
                }
            });
        });
    };
    CardsDetailComponent.prototype.ngOnDestroy = function () {
        if (this.sub) {
            this.sub.unsubscribe();
        }
    };
    CardsDetailComponent.prototype.ngAfterViewInit = function () {
        $(document).ready(function () {
            $(".toggle-social-buttons").click(function () {
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
    CardsDetailComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/cards/cards-detail.component.html'
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService, router_1.Router, cards_service_1.CateService, router_1.ActivatedRoute])
    ], CardsDetailComponent);
    return CardsDetailComponent;
}());
exports.CardsDetailComponent = CardsDetailComponent;
//# sourceMappingURL=cards-detail.component.js.map