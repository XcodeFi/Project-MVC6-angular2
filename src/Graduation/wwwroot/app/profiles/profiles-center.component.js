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
var account_service_1 = require('../account/account.service');
var ProfilesCenterComponent = (function () {
    function ProfilesCenterComponent(router, _accountService) {
        this.router = router;
        this._accountService = _accountService;
        this.cates = [];
    }
    ProfilesCenterComponent.prototype.ngOnInit = function () {
        if (!this._accountService.isUserAuthenticated()) {
            this.router.navigate(['/home']);
        }
    };
    ProfilesCenterComponent.prototype.ngAfterViewInit = function () {
    };
    ProfilesCenterComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/profiles/profiles-center.component.html',
            directives: [router_1.RouterOutlet, router_1.ROUTER_DIRECTIVES],
            providers: [card_service_1.CardService],
        }), 
        __metadata('design:paramtypes', [router_1.Router, account_service_1.AccountService])
    ], ProfilesCenterComponent);
    return ProfilesCenterComponent;
}());
exports.ProfilesCenterComponent = ProfilesCenterComponent;
//# sourceMappingURL=profiles-center.component.js.map