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
var models_1 = require('../models/models');
var card_service_1 = require('../services/card.service');
var cate_service_1 = require('../services/cate.service');
var widget_shared_component_1 = require('./widget.shared.component');
var CardDetailComponent = (function () {
    function CardDetailComponent(_cardService, _cateService, _router) {
        this._cardService = _cardService;
        this._cateService = _cateService;
        this._router = _router;
        this.navigated = false; // true if navigated here
    }
    CardDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.sub = this._router.params.subscribe(function (params) {
            if (params['id'] !== undefined) {
                var id = +params['id'];
                _this.navigated = true;
                _this._cardService.getCard(id).subscribe(function (card) {
                    _this.card = card;
                });
            }
            else {
                _this.navigated = false;
            }
        });
    };
    CardDetailComponent.prototype.ngOnDestroy = function () {
        this.sub.unsubscribe();
    };
    CardDetailComponent.prototype.goBack = function () {
        window.history.back();
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', models_1.Card)
    ], CardDetailComponent.prototype, "card", void 0);
    CardDetailComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/components/card-detail.component.html',
            directives: [widget_shared_component_1.WidgetSharedComponent]
        }), 
        __metadata('design:paramtypes', [card_service_1.CardService, cate_service_1.CateService, router_1.ActivatedRoute])
    ], CardDetailComponent);
    return CardDetailComponent;
}());
exports.CardDetailComponent = CardDetailComponent;
//# sourceMappingURL=card-detail.component.js.map