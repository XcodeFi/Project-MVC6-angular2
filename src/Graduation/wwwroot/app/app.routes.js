"use strict";
var cate_component_1 = require('./components/cate.component');
var card_detail_component_1 = require('./components/card-detail.component');
var home_component_1 = require('./components/home.component');
var help_component_1 = require('./components/help.component');
var router_deprecated_1 = require('@angular/router-deprecated');
exports.Routes = {
    home: new router_deprecated_1.Route({ path: '/', name: 'Home', component: home_component_1.HomeComponent, useAsDefault: true }),
    cates: new router_deprecated_1.Route({ path: '/cates', name: 'Cates', component: cate_component_1.CatesComponent }),
    cardDetail: new router_deprecated_1.Route({ path: '/card/:id', name: 'CardDetail', component: card_detail_component_1.CardDetailComponent }),
    help: new router_deprecated_1.Route({ path: '/help', name: 'Help', component: help_component_1.HelpComponent }),
};
exports.APP_ROUTER_PROVIDERS = Object.keys(exports.Routes).map(function (r) { return exports.Routes[r]; });
//const routes: RouterConfig = [
//    {
//        path: 'cates',
//        component: CatesComponent
//    },  
//    {
//        path: '',
//        redirectTo: '/home',
//        pathMatch: 'full'
//    }
//    ,
//    {
//        path: 'home',
//        component: HomeComponent
//    },
//    {
//        path: 'detail/:id',
//        component:CardDetailComponent
//    }
//    ,
//    {
//        path: 'help',
//        component: HelpComponent
//    }
//    ,
//    {
//        path: 'cate/:id',
//        component: CatesComponent
//    }
//];
//export const APP_ROUTER_PROVIDERS = [
//    provideRouter(routes)
//];
//# sourceMappingURL=app.routes.js.map