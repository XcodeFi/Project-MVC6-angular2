﻿import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {CardService} from '../card/card.service';
import {AuthGuard} from './auth.guard';
import {Cate} from '../models/models';
declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/profiles/profiles-center.component.html',
    directives: [RouterOutlet, ROUTER_DIRECTIVES],
    providers: [CardService, AuthGuard],
})
export class ProfilesCenterComponent implements OnInit, AfterViewInit {

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