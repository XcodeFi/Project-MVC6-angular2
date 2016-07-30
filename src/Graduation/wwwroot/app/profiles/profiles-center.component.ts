import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {CardService} from '../card/card.service';
import {Cate} from '../models/models';
import {AccountService} from '../account/account.service';
declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/profiles/profiles-center.component.html',
    directives: [RouterOutlet, ROUTER_DIRECTIVES],
    providers: [CardService],
})
export class ProfilesCenterComponent implements OnInit, AfterViewInit {

    errorMessage: string;
    cates: Cate[] = [];

    constructor(
        private router: Router,
        private _accountService: AccountService
    
    ) { }

    ngOnInit() {
        if (!this._accountService.isUserAuthenticated())
        {
            this.router.navigate(['/home']);
        }
    }

    ngAfterViewInit() {
        
    }
}