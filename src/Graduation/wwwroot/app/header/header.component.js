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
var forms_1 = require('@angular/forms');
var cards_service_1 = require('../cards/cards.service');
var account_service_1 = require('../account/account.service');
var HeaderComponent = (function () {
    function HeaderComponent(_cateService, _notify, formBuilder, router, _accountService) {
        this._cateService = _cateService;
        this._notify = _notify;
        this.formBuilder = formBuilder;
        this.router = router;
        this._accountService = _accountService;
        this.statusLogin = true;
        this.statusLogin = this.isLogin();
        this.loginForm = this.formBuilder.group({
            email: ['', [forms_1.Validators.required]],
            password: ['', [forms_1.Validators.required]],
            rememberMe: [false, [forms_1.Validators.required]]
        });
    }
    HeaderComponent.prototype.ngOnInit = function () {
        this.getAllCate();
    };
    HeaderComponent.prototype.isLogin = function () {
        return this._accountService.isUserAuthenticated();
    };
    HeaderComponent.prototype.onSubmit = function () {
        var _this = this;
        this._accountService.doLogin(this.loginForm.value).subscribe(function (res) {
            if (res != 3) {
                _this.statusLogin = true;
                localStorage.setItem('user', _this._accountService.getLogInUser());
                _this._notify.printSuccessMessage('Welcome back ' + res.email + '!');
                _this.router.navigate(['/profiles-center']);
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
                _this.statusLogin = false;
                _this._notify.printSuccessMessage('Logout Success!');
                _this.router.navigate(['/home']);
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
            styleUrls: ["css/validate.css"],
            directives: [router_1.ROUTER_DIRECTIVES, forms_1.REACTIVE_FORM_DIRECTIVES]
        }), 
        __metadata('design:paramtypes', [cards_service_1.CateService, notify_service_1.NotifyService, forms_1.FormBuilder, router_1.Router, account_service_1.AccountService])
    ], HeaderComponent);
    return HeaderComponent;
}());
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map