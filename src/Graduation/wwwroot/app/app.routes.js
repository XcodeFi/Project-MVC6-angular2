"use strict";
var router_1 = require('@angular/router');
var home_component_1 = require('./home/home.component');
var cards_routes_1 = require('./cards/cards.routes');
var profiles_routes_1 = require('./profiles/profiles.routes');
var page_not_found_component_1 = require('./pageNotFound/page-not-found.component');
var routes = [
    { path: 'home', component: home_component_1.HomeComponent }
].concat(cards_routes_1.cardsRoutes, profiles_routes_1.profilesRoutes, [
    { path: '**', component: page_not_found_component_1.PageNotFoundComponent },
]);
exports.appRouterProviders = [
    router_1.provideRouter(routes)
];
//# sourceMappingURL=app.routes.js.map