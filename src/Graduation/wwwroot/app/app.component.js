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
var notify_service_1 = require('./utility/notify.service');
var cards_service_1 = require('./cards/cards.service');
var account_1 = require('./models/account');
var account_service_1 = require('./account/account.service');
//enableProdMode()
var AppComponent = (function () {
    function AppComponent(_cateService, _notify, _accountService) {
        this._cateService = _cateService;
        this._notify = _notify;
        this._accountService = _accountService;
    }
    AppComponent.prototype.ngOnInit = function () {
        this._user = new account_1.UserLogin('', '', false);
        this.getAllCate();
    };
    AppComponent.prototype.onSubmit = function () {
        var _this = this;
        this._accountService.doLogin(this._user).subscribe(function (res) {
            if (res) {
                var user = res;
                localStorage.setItem('user', JSON.stringify(res));
                _this._notify.printSuccessMessage('Welcome back ' + res.email + '!');
            }
            else {
                _this._notify.printErrorMessage('User name or password not truely');
            }
        });
    };
    AppComponent.prototype.logout = function () {
        localStorage.removeItem('user');
    };
    AppComponent.prototype.getAllCate = function () {
        var _this = this;
        this._cateService.getChildCates().subscribe(function (cate) { return _this.cates = cate; });
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
            providers: [cards_service_1.CateService, notify_service_1.NotifyService, account_service_1.AccountService],
        }), 
        __metadata('design:paramtypes', [cards_service_1.CateService, notify_service_1.NotifyService, account_service_1.AccountService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map