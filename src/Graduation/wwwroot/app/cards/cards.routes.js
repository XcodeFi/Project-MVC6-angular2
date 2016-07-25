"use strict";
var cards_detail_component_1 = require('./cards-detail.component');
var cards_list_Component_1 = require('./cards-list.Component');
var cards_center_Component_1 = require('./cards-center.Component');
exports.cardsRoutes = [
    {
        path: '',
        redirectTo: '/cards',
        pathMatch: 'full'
    },
    {
        path: 'cards',
        component: cards_center_Component_1.CardsCenterComponent,
        children: [
            {
                path: ':id',
                component: cards_detail_component_1.CardsDetailComponent,
            },
            {
                path: '',
                component: cards_list_Component_1.CardsListComponent
            }
        ]
    }
];
//# sourceMappingURL=cards.routes.js.map