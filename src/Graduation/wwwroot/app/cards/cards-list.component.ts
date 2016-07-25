import { Component, OnInit } from '@angular/core';
import {Router } from '@angular/router';

import {CardService} from '../card/card.service';
import {Card} from '../models/models';


@Component({
    templateUrl: 'app/cards/cards-list.component.html'
})
export class CardsListComponent implements OnInit {
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

}