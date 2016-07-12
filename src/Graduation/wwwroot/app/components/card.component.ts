// Observable Version
import { Component, OnInit } from '@angular/core';
import { Card }              from '../models/models';
import { CardService }       from '../services/card.service';
@Component({
    selector: 'card-list',
    //templateUrl: 'cates/index',
    templateUrl: 'cates/cate',
    styleUrls:['css/cate.component.css']
})
export class CardListComponent implements OnInit {
    errorMessage: string;
    cards: Card[];
    mode = 'Observable';

    constructor(private _cardService: CardService) { }

    ngOnInit() { this.getCards(); }

    getCards() {
        this._cardService.getCards()
            .subscribe(
            cards => this.cards = cards,
            error => this.errorMessage = <any>error);
    }

    //addCard(name: string) {
    //    if (!name) { return; }
    //    this.heroService.addHero(name)
    //        .subscribe(
    //        hero => this.heroes.push(hero),
    //        error => this.errorMessage = <any>error);
    //}
}
