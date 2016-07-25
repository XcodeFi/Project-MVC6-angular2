import { provideRouter, RouterConfig } from '@angular/router';
import { HomeComponent }     from './home/home.component';
import {cardsRoutes} from './cards/cards.routes';
import {CardDetailComponent} from './card/card-detail.component';



const routes: RouterConfig = [
    { path: 'home', component: HomeComponent },
    { path: 'card-detail/:id', component: CardDetailComponent },
    ...cardsRoutes
    
];

export const appRouterProviders = [


    provideRouter(routes)
];