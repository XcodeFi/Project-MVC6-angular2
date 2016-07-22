import { provideRouter, RouterConfig }  from '@angular/router';
import {CatesComponent} from './components/cate.component';
import {CardListComponent} from './components/card.component';
import {CardDetailComponent} from'./components/card-detail.component';
import {HomeComponent} from'./components/home.component';
import {HelpComponent} from'./components/help.component';

import {Route, Router} from '@angular/router-deprecated';


export var Routes = {
    home: new Route({ path: '/', name: 'Home', component: HomeComponent, useAsDefault: true }),
    cates: new Route({ path: '/cates', name: 'Cates', component: CatesComponent }),
    cardDetail: new Route({ path: '/card/:id', name: 'CardDetail', component: CardDetailComponent }),
    help: new Route({ path: '/help', name: 'Help', component: HelpComponent }),
    
};

export const APP_ROUTER_PROVIDERS = Object.keys(Routes).map(r => Routes[r]);


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
