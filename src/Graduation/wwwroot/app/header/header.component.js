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
var notify_service_1 = require('../utility/notify.service');
var common_1 = require('@angular/common');
var cards_service_1 = require('../cards/cards.service');
var account_1 = require('../models/account');
var account_service_1 = require('../account/account.service');
//enableProdMode()
var HeaderComponent = (function () {
    function HeaderComponent(_cateService, _notify, _accountService) {
        this._cateService = _cateService;
        this._notify = _notify;
        this._accountService = _accountService;
    }
    HeaderComponent.prototype.ngOnInit = function () {
        this._user = new account_1.UserLogin('', '', false);
        this.getAllCate();
    };
    HeaderComponent.prototype.isLogin = function () {
        return this._accountService.isUserAuthenticated();
    };
    HeaderComponent.prototype.onSubmit = function () {
        var _this = this;
        this._accountService.doLogin(this._user).subscribe(function (res) {
            if (res != 3) {
                var user = res;
                localStorage.setItem('user', JSON.stringify(res));
                _this._notify.printSuccessMessage('Welcome back ' + res.email + '!');
            }
            else {
                _this._notify.printErrorMessage('User name or password not truely');
            }
        });
    };
    HeaderComponent.prototype.logOut = function (event) {
        var _this = this;
        event.preventDefault();
        this._accountService.doLogout().subscribe(function (res) {
            if (res == 1) {
                localStorage.removeItem('user');
                _this._notify.printSuccessMessage('Logout Success!');
            }
            else {
                _this._notify.printErrorMessage('Something not truely!');
            }
        });
    };
    HeaderComponent.prototype.getAllCate = function () {
        var _this = this;
        this._cateService.getChildCates().subscribe(function (cate) { return _this.cates = cate; });
    };
    HeaderComponent.prototype.ngAfterViewInit = function () {
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
    HeaderComponent = __decorate([
        core_1.Component({
            selector: 'header-main',
            templateUrl: 'app/header/header.component.html',
            directives: [router_1.ROUTER_DIRECTIVES, common_1.FORM_DIRECTIVES, common_1.CORE_DIRECTIVES],
            styleUrls: ["css/validate.css"]
        }), 
        __metadata('design:paramtypes', [cards_service_1.CateService, notify_service_1.NotifyService, account_service_1.AccountService])
    ], HeaderComponent);
    return HeaderComponent;
}());
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map