import
{
    enableProdMode,
    Component,
    OnInit,
    AfterViewInit,
} from '@angular/core';
import { RouteConfig, ROUTER_DIRECTIVES } from '@angular/router-deprecated';
import {CateService} from './services/cate.service';
import {CardService} from './services/card.service';
import {AccountService} from './services/account.service';

import {APP_ROUTER_PROVIDERS} from './app.routes';

import {SlideService} from './services/slide.service';
import {Cate} from './models/models';

enableProdMode();

declare var $: JQueryStatic;

@RouteConfig(APP_ROUTER_PROVIDERS)

@Component({
    selector: 'my-app',
    templateUrl:'app/app.component.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [CateService, CardService, AccountService, SlideService]
})
export class AppComponent implements OnInit, AfterViewInit {

    errorMessage: string;
    cates: Cate[] = [];
    constructor(private _cateService: CateService,
        private _accountService: AccountService
    ) {
    }

    ngOnInit() {
        this.getCate();
    }
    getCate() {
        this._cateService.getChildCates()
            .subscribe(
            cates => this.cates = cates,
            error => this.errorMessage = <any>error);
    }

    logOff(): void {
        this._accountService.Logoff();
        console.log('Logout');
    }

    ngAfterViewInit() {
        $(document).ready(function () {
            $(".search-btn a").click(function () {
                $(".search-collapse").fadeOut(function () {
                    $(".search-box").fadeIn();
                });
            });

            $(".search-btn-close").click(function () {
                $(".search-box").fadeOut(function () {
                    $(".search-collapse").fadeIn();
                });
            });
        });
    }
}
