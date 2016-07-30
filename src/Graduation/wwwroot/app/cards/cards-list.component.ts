import { Component, OnInit,AfterViewInit } from '@angular/core';
import {Router } from '@angular/router';

import {CardService} from '../card/card.service';
import {Card} from '../models/models';


@Component({
    templateUrl: 'app/cards/cards-list.component.html'
})
export class CardsListComponent implements OnInit, AfterViewInit {
    cards: Card[] = [];    
    errorMessage: string;

    constructor(
        private _cardService: CardService,
        private _router: Router
    ) { }

    ngOnInit() {
        this.getCard();
    }

    getCard() {
        this._cardService.getCards()
            .subscribe(cards => this.cards = cards,
            err => this.errorMessage = <any>err);
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