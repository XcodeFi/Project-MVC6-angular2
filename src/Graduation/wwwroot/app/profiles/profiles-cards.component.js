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
var notify_service_1 = require('../utility/notify.service');
var account_service_1 = require('../account/account.service');
var ProfilesCardsComponent = (function () {
    function ProfilesCardsComponent(router, _cardService, _accountService, _notify) {
        this.router = router;
        this._cardService = _cardService;
        this._accountService = _accountService;
        this._notify = _notify;
        this.cates = [];
        this.cards = [];
    }
    ProfilesCardsComponent.prototype.ngOnInit = function () {
        this.loadCards();
    };
    ProfilesCardsComponent.prototype.loadCards = function () {
        var _this = this;
        this._accountService.getUserInfo().subscribe(function (res) {
            _this._cardService.getCardsForUser(res.id).subscribe(function (cards) {
                _this.cards = cards;
            });
        });
    };
    ProfilesCardsComponent.prototype.delete = function (id, title) {
        var _this = this;
        this._notify.printConfirmationDialog("Bạn thực sự muốn xóa thiệp này?", function () {
            _this._accountService.getUserInfo().subscribe(function (res) {
                _this._cardService.deleteCard(id, res.id).subscribe(function (res) {
                    if (res.succeeded) {
                        _this._notify.printSuccessMessage("Thiệp " + title + " xóa thành công");
                    }
                });
            });
        });
    };
    ProfilesCardsComponent.prototype.ngAfterViewInit = function () {
    };
    ProfilesCardsComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/profiles/profiles-cards.component.html',
            directives: [router_1.RouterOutlet, router_1.ROUTER_DIRECTIVES],
            providers: [card_service_1.CardService, account_service_1.AccountService],
        }), 
        __metadata('design:paramtypes', [router_1.Router, card_service_1.CardService, account_service_1.AccountService, notify_service_1.NotifyService])
    ], ProfilesCardsComponent);
    return ProfilesCardsComponent;
}());
exports.ProfilesCardsComponent = ProfilesCardsComponent;
//# sourceMappingURL=profiles-cards.component.js.map