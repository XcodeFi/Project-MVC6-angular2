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
var cate_service_1 = require('../services/cate.service');
var CateListComponent = (function () {
    function CateListComponent(_cateService) {
        this._cateService = _cateService;
        this.cates = [];
    }
    CateListComponent.prototype.ngOnInit = function () {
        this.getCate();
    };
    CateListComponent.prototype.getCate = function () {
        var _this = this;
        this._cateService.getCates()
            .subscribe(function (cates) { return _this.cates = cates; }, function (error) { return _this.errorMessage = error; });
    };
    CateListComponent = __decorate([
        core_1.Component({
            selector: 'cate-list',
            templateUrl: 'app/components/cate-list.component.html',
            styleUrls: ['css/cate-list.component.css']
        }), 
        __metadata('design:paramtypes', [cate_service_1.CateService])
    ], CateListComponent);
    return CateListComponent;
}());
exports.CateListComponent = CateListComponent;
//# sourceMappingURL=cate-list.component.js.map