import { RouterConfig }         from '@angular/router';

import {CardsDetailComponent}from './cards-detail.component';
import { CardsListComponent } from './cards-list.Component';
import { CardsCenterComponent } from './cards-center.Component';
import { CardDetailComponent } from '../card/card-detail.component';



export const cardsRoutes: RouterConfig = [
    {
        path: '',
        redirectTo: '/cards',
        pathMatch: 'full'
    },
    {
        path: 'cards',
        component: CardsCenterComponent,
        children: [
            {
                path: ':id',
                children: [
                    {
                        path: ':id',
                        component: CardDetailComponent
                    }
                    ,
                    {
                        path: '',
                        component: CardsDetailComponent,
                    }
                ]
            },
            {
                path: '',
                component: CardsListComponent
            }
        ]
    }
];