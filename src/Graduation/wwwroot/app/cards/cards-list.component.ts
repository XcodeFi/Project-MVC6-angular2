import { Component, OnInit,AfterViewInit } from '@angular/core';
import {Router } from '@angular/router';

import {CardService} from '../card/card.service';
import {Card} from '../models/models';
import { InfiniteScroll } from 'angular2-infinite-scroll';

@Component({
    templateUrl: 'app/cards/cards-list.component.html',
    directives: [InfiniteScroll],
    styles: [
        `.search-results {
            height: 270px;
            //overflow: scroll;
        }`
    ],
})
export class CardsListComponent implements OnInit, AfterViewInit {
    cards: Card[] = [];    
    errorMessage: string;

    sum: number = 0;

    constructor(
        private _cardService: CardService,
        private _router: Router
    ) { }

    ngOnInit() {
        this.getCard();
    }

    getCard() {
        this._cardService.getCards(0)
            .subscribe(cards => this.cards = cards,
            err => this.errorMessage = <any>err);
    }


    onScrollDown() {
        console.log('scrolled!!');
        this.sum += 1;
        this._cardService.getCards(this.sum)
            .subscribe(cards => {
                if (cards.length == 0) {
                    return;
                }
                for (let item of cards) {
                    this.cards.push(item);
                }
            },
            err => this.errorMessage = <any>err);
        
    }
    ngAfterViewInit() {
        $(document).ready(function () {
            //$(".toggle-social-buttons").click(function () {
                $("#shared").click(function () {
                console.log('click');
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