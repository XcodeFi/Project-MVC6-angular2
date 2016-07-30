import { RouterConfig }         from '@angular/router';
import {ProfilesCenterComponent} from './profiles-center.component';
import {ProfilesCardsComponent} from './profiles-cards.component';
import {ProfileAccountSettingComponent } from './profiles-account-setting.component';

import {AccountService} from '../account/account.service';

export const profilesRoutes: RouterConfig = [
    
    {
        path: 'profiles-center',
        component: ProfilesCenterComponent,
        children: [
            {
                path: '',
                redirectTo: 'cards',
                pathMatch:'full'
            },
            {
                path: 'cards',
                component: ProfilesCardsComponent,
            },
            {
                path: 'account-setting',
                component: ProfileAccountSettingComponent,
            }
        ]
    }
];


