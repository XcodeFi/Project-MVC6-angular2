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
var cate_service_1 = require('./services/cate.service');
var card_service_1 = require('./services/card.service');
var account_service_1 = require('./services/account.service');
var slide_service_1 = require('./services/slide.service');
core_1.enableProdMode();
var AppComponent = (function () {
    function AppComponent(_cateService, _accountService) {
        this._cateService = _cateService;
        this._accountService = _accountService;
        this.cates = [];
    }
    AppComponent.prototype.ngOnInit = function () {
        this.getCate();
    };
    AppComponent.prototype.getCate = function () {
        var _this = this;
        this._cateService.getChildCates()
            .subscribe(function (cates) { return _this.cates = cates; }, function (error) { return _this.errorMessage = error; });
    };
    AppComponent.prototype.logOff = function () {
        this._accountService.Logoff();
        console.log('Logout');
    };
    AppComponent.prototype.ngAfterViewInit = function () {
        $(document).ready(function () {
            $(".search-btn a").click(function () {
                $(".search-collapse").fadeOut(function () {
                    $(".search-box").fadeIn();
                });
            });
            $(".search-btn-close").click(function () {
                $(".search-box").fadeOut(function () {
                    $(".search-collapse").fadeIn();
                });
            });
        });
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: 'app/app.component.html',
            directives: [router_1.ROUTER_DIRECTIVES],
            providers: [cate_service_1.CateService, card_service_1.CardService, account_service_1.AccountService, slide_service_1.SlideService]
        }), 
        __metadata('design:paramtypes', [cate_service_1.CateService, account_service_1.AccountService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map