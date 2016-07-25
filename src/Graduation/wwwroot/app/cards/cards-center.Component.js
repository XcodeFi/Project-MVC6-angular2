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
var cards_service_1 = require('./cards.service');
var card_service_1 = require('../card/card.service');
var CardsCenterComponent = (function () {
    function CardsCenterComponent(_cateService, router) {
        this._cateService = _cateService;
        this.router = router;
        this.cates = [];
    }
    CardsCenterComponent.prototype.ngOnInit = function () {
        this.getCate();
    };
    CardsCenterComponent.prototype.getCate = function () {
        var _this = this;
        this._cateService.getCates()
            .subscribe(function (cates) { return _this.cates = cates; }, function (error) { return _this.errorMessage = error; });
    };
    CardsCenterComponent.prototype.ngAfterViewInit = function () {
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
    CardsCenterComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/cards/cards-center.component.html',
            directives: [router_1.RouterOutlet, router_1.ROUTER_DIRECTIVES],
            providers: [cards_service_1.CateService, card_service_1.CardService],
            styleUrls: ["css/cards-list.component.css"],
        }), 
        __metadata('design:paramtypes', [cards_service_1.CateService, router_1.Router])
    ], CardsCenterComponent);
    return CardsCenterComponent;
}());
exports.CardsCenterComponent = CardsCenterComponent;
//# sourceMappingURL=cards-center.Component.js.map