
import { Component, AfterViewInit, enableProdMode, OnInit} from '@angular/core';
import { Router, ROUTER_DIRECTIVES} from '@angular/router';
import {NotifyService} from './utility/notify.service';

//header
import {HeaderComponent} from './header/header.component';

//spotlight
import {SpotLightComponent} from './header/header-spotlight.component';
//footer
import {FooterComponent} from './footer/footer.component';


import {CateService} from './cards/cards.service';
import {Cate} from './models/models';
import {UserLogin} from './models/account';
import {AccountService} from './account/account.service';

//enableProdMode()

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    directives: [ROUTER_DIRECTIVES,
        HeaderComponent,
        SpotLightComponent,
        FooterComponent,
    ],
    providers: [CateService, NotifyService, AccountService],
})
export class AppComponent { 
    
}
