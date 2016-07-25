
import { Component, AfterViewInit, enableProdMode, OnInit} from '@angular/core';
import { Router, ROUTER_DIRECTIVES} from '@angular/router';
import {NotifyService} from './utility/notify.service';
import {NgForm} from '@angular/common';


import {CateService} from './cards/cards.service';
import {Cate} from './models/models';
import {UserLogin} from './models/account';
import {AccountService} from './account/account.service';

//enableProdMode()

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [CateService, NotifyService, AccountService],
})
export class AppComponent implements OnInit, AfterViewInit { 
    private cates: Cate[];
    private _user : UserLogin;


    constructor(private _cateService: CateService,
        private _notify: NotifyService,
        public _accountService: AccountService
    ) {
    }
    ngOnInit() {
        this._user = new UserLogin('', '', false);
        this.getAllCate();
    }

    onSubmit() {
        this._accountService.doLogin(this._user).subscribe(res => {
            if (res) {
                let user = res;
                localStorage.setItem('user', JSON.stringify(res));
                this._notify.printSuccessMessage('Welcome back ' + res.email + '!');
            }
            else {
                this._notify.printErrorMessage('User name or password not truely');
            }
        })
    }

    public logout() {
        localStorage.removeItem('user');

    }

    getAllCate() {

        this._cateService.getChildCates().subscribe(cate => this.cates = cate);
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
