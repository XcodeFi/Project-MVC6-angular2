import { provideRouter, RouterConfig } from '@angular/router';
import { HomeComponent }     from './home/home.component';
import {cardsRoutes} from './cards/cards.routes';
import {CardDetailComponent} from './card/card-detail.component';
import {profilesRoutes} from './profiles/profiles.routes';


const routes: RouterConfig = [
    { path: 'home', component: HomeComponent },
    { path: 'card-detail/:id', component: CardDetailComponent },
    ...cardsRoutes,
    ...profilesRoutes
];

export const appRouterProviders = [


    provideRouter(routes)
];