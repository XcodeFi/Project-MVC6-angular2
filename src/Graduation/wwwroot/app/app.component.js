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
//header
var header_component_1 = require('./header/header.component');
//spotlight
var header_spotlight_component_1 = require('./header/header-spotlight.component');
//footer
var footer_component_1 = require('./footer/footer.component');
var cards_service_1 = require('./cards/cards.service');
var account_service_1 = require('./account/account.service');
//enableProdMode()
var AppComponent = (function () {
    function AppComponent() {
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: 'app/app.component.html',
            directives: [router_1.ROUTER_DIRECTIVES,
                header_component_1.HeaderComponent,
                header_spotlight_component_1.SpotLightComponent,
                footer_component_1.FooterComponent,
            ],
            providers: [cards_service_1.CateService, notify_service_1.NotifyService, account_service_1.AccountService],
        }), 
        __metadata('design:paramtypes', [])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map