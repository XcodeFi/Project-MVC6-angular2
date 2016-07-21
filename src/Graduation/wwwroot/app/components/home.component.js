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
var coming_day_shared_component_1 = require('./coming-day.shared.component');
var slide_service_1 = require('../services/slide.service');
var HomeComponent = (function () {
    function HomeComponent(_slideService) {
        this._slideService = _slideService;
        this.slides = [];
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.getSlide();
    };
    HomeComponent.prototype.getSlide = function () {
        var _this = this;
        this._slideService.getSlides()
            .subscribe(function (slides) { return _this.slides = slides; }, function (error) { return _this.errorMessage = error; });
    };
    HomeComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/components/home.component.html',
            directives: [coming_day_shared_component_1.ComingdaySharedComponent]
        }), 
        __metadata('design:paramtypes', [slide_service_1.SlideService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map