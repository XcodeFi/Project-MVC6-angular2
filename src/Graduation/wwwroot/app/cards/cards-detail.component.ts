import { Component, OnInit, AfterViewInit,OnDestroy } from '@angular/core';
import {ActivatedRoute, Router } from '@angular/router';

import {CardService} from '../card/card.service';
import {CateService} from './cards.service';

import {Card} from '../models/models';
import {CateDetail} from '../models/models';

@Component({
    templateUrl: 'app/cards/cards-detail.component.html'
})
export class CardsDetailComponent implements OnInit, AfterViewInit, OnDestroy {
    cards: Card[] = [];
    errorMessage: string;
    cateName: string;
    _id: string;
    private sub: any;

    constructor(
        private _cardService: CardService,
        private _router: Router,
        private _cateService: CateService,
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.sub = this.route
            .params
            .subscribe(params => {
                let url = params['url'];
                this._id = url;
                this._cateService.getCateUrl(url).subscribe(cate => {
                    if (cate) {
                        this.cateName = cate.name;
                        this.cards = cate.cards;
                    } else { // id not found
                        this._router.navigate(['/**']);
                    }
                });
            })
    }


    ngOnDestroy() {
        if (this.sub) {
            this.sub.unsubscribe();
        }
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