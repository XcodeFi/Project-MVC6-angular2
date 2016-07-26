"use strict";
var profiles_center_component_1 = require('./profiles-center.component');
var profiles_cards_component_1 = require('./profiles-cards.component');
var profiles_account_setting_component_1 = require('./profiles-account-setting.component');
exports.profilesRoutes = [
    {
        path: 'profiles-center',
        component: profiles_center_component_1.ProfilesCenterComponent,
        children: [
            {
                path: '',
                redirectTo: 'cards',
                pathMatch: 'full'
            },
            {
                path: 'cards',
                component: profiles_cards_component_1.ProfilesCardsComponent,
            },
            {
                path: 'account-setting',
                component: profiles_account_setting_component_1.ProfileAccountSettingComponent
            }
        ]
    }
];
//# sourceMappingURL=profiles.routes.js.map