import
{
    Component,
    OnInit,
    AfterViewInit,
} from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';
import {CateService} from './services/cate.service';
import {CardService} from './services/card.service';
import {AccountService} from './services/account.service';
import {Cate} from './models/models';

declare var $: JQueryStatic;

@Component({
    selector: 'my-app',
    templateUrl:'app/app.component.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [CateService, CardService, AccountService]
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
