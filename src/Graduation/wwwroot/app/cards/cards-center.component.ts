import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {CateService} from './cards.service';
import {CardService} from '../card/card.service';
import {Cate} from '../models/models';
declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/cards/cards-center.component.html',
    directives: [RouterOutlet, ROUTER_DIRECTIVES],
    providers: [CateService,CardService],
    styleUrls: [`css/cards-list.component.css`],
    
})
export class CardsCenterComponent implements OnInit, AfterViewInit {

    errorMessage: string;
    cates: Cate[] = [];

    constructor(private _cateService: CateService,
        private router: Router
    ) { }

    ngOnInit() {
        this.getCate();
    }
    getCate() {
        this._cateService.getCates()
            .subscribe(
            cates => this.cates = cates,
            error => this.errorMessage = <any>error);
    }

   

    ngAfterViewInit() {
        $(document).ready(function () {
            $(".toggle-social-buttons").click(function () {

                var shareButtonRow = $(this).closest(".caption").find(".share-button-row");
                var socialButtonRow = $(this).closest(".caption").find(".social-button-row");

                if ($(shareButtonRow).hasClass("hidden")) {
                    $(shareButtonRow).removeClass("hidden");
                    $(socialButtonRow).addClass("hidden");
                }
                else {
                    $(shareButtonRow).addClass("hidden");
                    $(socialButtonRow).removeClass("hidden");
                }
            });
            $(".scroll-to-top").click(function (e) {
                e.preventDefault();
                $("html,body").animate({ scrollTop: 0 }, 500);
            })
        });
    }
}