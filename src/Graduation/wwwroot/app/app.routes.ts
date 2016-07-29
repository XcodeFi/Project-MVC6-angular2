import { provideRouter, RouterConfig } from '@angular/router';
import { HomeComponent }     from './home/home.component';
import {cardsRoutes} from './cards/cards.routes';
import {CardDetailComponent} from './card/card-detail.component';
import {profilesRoutes} from './profiles/profiles.routes';
import {AuthGuard} from './profiles/auth.guard';
import {AccountService} from './account/account.service';

const routes: RouterConfig = [
    { path: 'home', component: HomeComponent },
    ...cardsRoutes,
    ...profilesRoutes
];

export const appRouterProviders = [
    provideRouter(routes),
];