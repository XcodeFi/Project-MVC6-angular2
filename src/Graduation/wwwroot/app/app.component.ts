import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';
import {CateService} from './services/cate.service';
import {CardService} from './services/card.service';
import {AccountService} from './services/account.service';
import {Cate} from './models/models';

@Component({
    selector: 'my-app',
    templateUrl:'app/app.component.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [CateService, CardService, AccountService]
})
export class AppComponent implements OnInit {
    errorMessage: string;
    cates: Cate[] = [];
    constructor(private _cateService: CateService,
        private _accountService: AccountService
    ) { }

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
}
