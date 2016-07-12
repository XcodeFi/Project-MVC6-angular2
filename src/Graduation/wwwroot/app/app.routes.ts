import { provideRouter, RouterConfig }  from '@angular/router';
import {CatesComponent} from './components/cate.component';
import {CardListComponent} from './components/card.component';
import {CardDetailComponent} from'./components/card-detail.component';
import {HomeComponent} from'./components/home.component';
import {HelpComponent} from'./components/help.component';

const routes: RouterConfig = [
    {
        path: 'cates',
        component: CatesComponent
    },  
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    }
    ,
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'detail/:id',
        component:CardDetailComponent
    }
    ,
    {
        path: 'help',
        component: HelpComponent
    }
    ,
    {
        path: 'cate/:id',
        component: CatesComponent
    }
];

export const APP_ROUTER_PROVIDERS = [
    provideRouter(routes)
];
