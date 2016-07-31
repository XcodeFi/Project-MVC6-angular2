import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {NotifyService} from '../utility/notify.service';
import {ChangePassword} from '../models/account';
import {AccountService} from '../account/account.service';
import {EqualValidator} from '../utility/equal-validator.directive';
import {FORM_DIRECTIVES, NgForm, CORE_DIRECTIVES} from '@angular/common';


declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/profiles/profiles-account-setting.component.html',
    //templateUrl: 'account/login',
    directives: [RouterOutlet, ROUTER_DIRECTIVES, EqualValidator, FORM_DIRECTIVES, CORE_DIRECTIVES],
    styleUrls: ['css/validate.css']
})
export class ProfileAccountSettingComponent implements OnInit, AfterViewInit {
      
    errorMessage: string;
    model: ChangePassword;


    constructor(
        private router: Router,
        private _notify: NotifyService,
        private _accountService: AccountService
    ) { }

    ngOnInit() {
        this.model = new ChangePassword('', '', '');
    }

    onSubmit() {
        console.log(this.model);
        this._accountService.changePassword(this.model).subscribe(res => {

            if (res.succeeded) {
                this._notify.printSuccessMessage(res.message);
            }
            else {
                this._notify.printErrorMessage(res.message);
            }
        },
            error => {
                if (error.status == 400) {
                    let data = error.json();
                    this._notify.printErrorMessage(data);
                }
            }
        )
    }

    ngAfterViewInit() {

    }
}