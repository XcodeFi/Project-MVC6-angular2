/// <reference path="../../typings/index.d.ts" />
"use strict";
var forms_1 = require('@angular/forms');
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
var app_routes_1 = require('./app.routes');
var common_1 = require('@angular/common');
var http_1 = require('@angular/http');
var app_component_1 = require('./app.component');
platform_browser_dynamic_1.bootstrap(app_component_1.AppComponent, [
    app_routes_1.appRouterProviders,
    http_1.HTTP_PROVIDERS,
    forms_1.disableDeprecatedForms(),
    { provide: common_1.LocationStrategy, useClass: common_1.HashLocationStrategy } //for router
]).catch(function (err) { return console.log(err); });
//# sourceMappingURL=main.js.map