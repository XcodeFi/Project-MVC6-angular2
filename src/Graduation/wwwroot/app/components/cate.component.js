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
var cate_service_1 = require('../services/cate.service');
var cate_list_component_1 = require('./cate-list.component');
var card_service_1 = require('../services/card.service');
var CatesComponent = (function () {
    function CatesComponent(_cateService, _cardService, _router) {
        this._cateService = _cateService;
        this._cardService = _cardService;
        this._router = _router;
        //cates: Cate[]=[];
        this.cards = [];
    }
    CatesComponent.prototype.ngOnInit = function () { this.getCard(); };
    //getCates() {
    //    this._cateService.getCates()
    //        .subscribe(
    //        cates => this.cates = cates.slice(1,5),
    //        error => this.errorMessage = <any>error);
    //}
    CatesComponent.prototype.getCard = function () {
        var _this = this;
        this._cardService.getCards()
            .subscribe(function (cards) { return _this.cards = cards; }, function (err) { return _this.errorMessage = err; });
    };
    CatesComponent.prototype.gotoDetail = function (card) {
        var link = ['/detail', card.id];
        this._router.navigate(link);
    };
    CatesComponent.prototype.ngAfterViewInit = function () {
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
    CatesComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/components/cate.component.html',
            //styleUrls: ['css/cate.component.css'],
            directives: [cate_list_component_1.CateListComponent],
            animations: [
                core_1.trigger('flyInOut', [
                    core_1.state('in', core_1.style({ opacity: 1, transform: 'translateX(0)' })),
                    core_1.transition('void => *', [
                        core_1.style({
                            opacity: 0,
                            transform: 'translateX(-100%)'
                        }),
                        core_1.animate('0.6s ease-in')
                    ]),
                    core_1.transition('* => void', [
                        core_1.animate('0.2s 10 ease-out', core_1.style({
                            opacity: 0,
                            transform: 'translateX(100%)'
                        }))
                    ])
                ])
            ]
        }), 
        __metadata('design:paramtypes', [cate_service_1.CateService, card_service_1.CardService, router_1.Router])
    ], CatesComponent);
    return CatesComponent;
}());
exports.CatesComponent = CatesComponent;
//# sourceMappingURL=cate.component.js.map