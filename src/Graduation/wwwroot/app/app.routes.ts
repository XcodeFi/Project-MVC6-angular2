import { provideRouter, RouterConfig } from '@angular/router';
import { HomeComponent }     from './home/home.component';
import {cardsRoutes} from './cards/cards.routes';
import {CardDetailComponent} from './card/card-detail.component';
import {profilesRoutes} from './profiles/profiles.routes';
import {AccountService} from './account/account.service';
import {PageNotFoundComponent} from './pageNotFound/page-not-found.component';


const routes: RouterConfig = [
    { path: 'home', component: HomeComponent },
    ...cardsRoutes,
    ...profilesRoutes,
    { path: '**', component: PageNotFoundComponent },
];


export const appRouterProviders = [
    provideRouter(routes)
];