﻿import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {CardService} from '../card/card.service';
import {Cate} from '../models/models';
declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/profiles/profiles-cards.component.html',
    directives: [RouterOutlet, ROUTER_DIRECTIVES],
    providers: [CardService],
})
export class ProfilesCardsComponent implements OnInit, AfterViewInit {

    errorMessage: string;
    cates: Cate[] = [];

    constructor(
        private router: Router
    ) { }

    ngOnInit() {
    }

    ngAfterViewInit() {

    }
}