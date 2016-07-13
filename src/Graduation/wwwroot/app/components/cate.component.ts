import { Component,
    OnInit, trigger,
    state, style, group
    , animate, transition,
    AfterViewInit } from '@angular/core';
import {Router} from '@angular/router';


import { Cate }              from '../models/models';
import { CateService }       from '../services/cate.service';
import {CateListComponent} from './cate-list.component'
import {CardService} from '../services/card.service';
import {Card} from '../models/models';

declare var $: JQueryStatic;
@Component({
    templateUrl: 'app/components/cate.component.html',
    //styleUrls: ['css/cate.component.css'],
    directives: [CateListComponent],
    animations: [
        trigger('flyInOut', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateX(-100%)'
                }),
                animate('0.6s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateX(100%)'
                }))
            ])
        ])
    ]
})
export class CatesComponent implements OnInit, AfterViewInit {
    errorMessage: string;
    //cates: Cate[]=[];
    cards: Card[] = [];    

    constructor(private _cateService: CateService,
        private _cardService: CardService,
        private _router: Router
    ) { }

    ngOnInit() { this.getCard(); }

    //getCates() {
    //    this._cateService.getCates()
    //        .subscribe(
    //        cates => this.cates = cates.slice(1,5),
    //        error => this.errorMessage = <any>error);
    //}

    getCard() {
        this._cardService.getCards()
            .subscribe(cards => this.cards = cards,
            err => this.errorMessage = <any>err);
    }

    

    gotoDetail(card: Card) {
        let link = ['/detail', card.id];
        this._router.navigate(link);
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