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
var account_1 = require('../models/account');
var account_service_1 = require('../account/account.service');
var equal_validator_directive_1 = require('../utility/equal-validator.directive');
var common_1 = require('@angular/common');
var ProfileAccountSettingComponent = (function () {
    function ProfileAccountSettingComponent(router, _notify, _accountService) {
        this.router = router;
        this._notify = _notify;
        this._accountService = _accountService;
    }
    ProfileAccountSettingComponent.prototype.ngOnInit = function () {
        this.model = new account_1.ChangePassword('', '', '');
    };
    ProfileAccountSettingComponent.prototype.onSubmit = function () {
        var _this = this;
        console.log(this.model);
        this._accountService.changePassword(this.model).subscribe(function (res) {
            if (res.succeeded) {
                _this._notify.printSuccessMessage(res.message);
            }
            else {
                _this._notify.printErrorMessage(res.message);
            }
        }, function (error) {
            if (error.status == 400) {
                var data = error.json();
                _this._notify.printErrorMessage(data);
            }
        });
    };
    ProfileAccountSettingComponent.prototype.ngAfterViewInit = function () {
    };
    ProfileAccountSettingComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/profiles/profiles-account-setting.component.html',
            //templateUrl: 'account/login',
            directives: [router_1.RouterOutlet, router_1.ROUTER_DIRECTIVES, equal_validator_directive_1.EqualValidator, common_1.FORM_DIRECTIVES, common_1.CORE_DIRECTIVES],
            styleUrls: ['css/validate.css']
        }), 
        __metadata('design:paramtypes', [router_1.Router, notify_service_1.NotifyService, account_service_1.AccountService])
    ], ProfileAccountSettingComponent);
    return ProfileAccountSettingComponent;
}());
exports.ProfileAccountSettingComponent = ProfileAccountSettingComponent;
//# sourceMappingURL=profiles-account-setting.component.js.map