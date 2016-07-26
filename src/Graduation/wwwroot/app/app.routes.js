"use strict";
var router_1 = require('@angular/router');
var home_component_1 = require('./home/home.component');
var cards_routes_1 = require('./cards/cards.routes');
var card_detail_component_1 = require('./card/card-detail.component');
var profiles_routes_1 = require('./profiles/profiles.routes');
var routes = [
    { path: 'home', component: home_component_1.HomeComponent },
    { path: 'card-detail/:id', component: card_detail_component_1.CardDetailComponent }
].concat(cards_routes_1.cardsRoutes, profiles_routes_1.profilesRoutes);
exports.appRouterProviders = [
    router_1.provideRouter(routes)
];
//# sourceMappingURL=app.routes.js.map