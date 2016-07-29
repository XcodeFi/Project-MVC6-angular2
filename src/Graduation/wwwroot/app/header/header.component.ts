
import { Component, AfterViewInit, OnInit} from '@angular/core';
import { Router, ROUTER_DIRECTIVES} from '@angular/router';
import {NotifyService} from '../utility/notify.service';

import {CateService} from '../cards/cards.service';
import {Cate} from '../models/models';
import {UserLogin} from '../models/account';
import {AccountService} from '../account/account.service';

@Component({
    selector: 'header-main',
    templateUrl: 'app/header/header.component.html',
    styleUrls: [`css/validate.css`],
    directives: [ROUTER_DIRECTIVES]
})
export class HeaderComponent implements OnInit, AfterViewInit {
    private cates: Cate[];
    private _user: UserLogin;


    constructor(private _cateService: CateService,
        private _notify: NotifyService,
        public _accountService: AccountService
    ) {
    }
    ngOnInit() {
        this._user = new UserLogin('', '', false);
        this.getAllCate();
    }

    isLogin(): boolean {
        return this._accountService.isUserAuthenticated();
    }

    onSubmit() {
        this._accountService.doLogin(this._user).subscribe(res => {
            if (res != 3) {
                let user = res;
                localStorage.setItem('user', JSON.stringify(res));
                this._notify.printSuccessMessage('Welcome back ' + res.email + '!');
            }
            else {
                this._notify.printErrorMessage('User name or password not truely');
            }
        })
    }

    logOut(event: any) {
        event.preventDefault();
        this._accountService.doLogout().subscribe(res => {
            if (res == 1) {
                localStorage.removeItem('user');
                this._notify.printSuccessMessage('Logout Success!');
            }
            else {
                this._notify.printErrorMessage('Something not truely!');
            }
        });
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
