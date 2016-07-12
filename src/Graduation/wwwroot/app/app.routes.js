"use strict";
var router_1 = require('@angular/router');
var cate_component_1 = require('./components/cate.component');
var card_detail_component_1 = require('./components/card-detail.component');
var home_component_1 = require('./components/home.component');
var help_component_1 = require('./components/help.component');
var routes = [
    {
        path: 'cates',
        component: cate_component_1.CatesComponent
    },
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: home_component_1.HomeComponent
    },
    {
        path: 'detail/:id',
        component: card_detail_component_1.CardDetailComponent
    },
    {
        path: 'help',
        component: help_component_1.HelpComponent
    },
    {
        path: 'cate/:id',
        component: cate_component_1.CatesComponent
    }
];
exports.APP_ROUTER_PROVIDERS = [
    router_1.provideRouter(routes)
];
//# sourceMappingURL=app.routes.js.map