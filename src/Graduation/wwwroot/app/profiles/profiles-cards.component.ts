import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, RouterOutlet, ActivatedRoute, ROUTER_DIRECTIVES} from '@angular/router';
import {CardService} from '../card/card.service';
import {NotifyService} from '../utility/notify.service';
import {AccountService} from '../account/account.service';

import {Cate, Card} from '../models/models';
import {UserLogin} from '../models/account';
declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/profiles/profiles-cards.component.html',
    directives: [RouterOutlet, ROUTER_DIRECTIVES],
    providers: [CardService, AccountService],
})
export class ProfilesCardsComponent implements OnInit, AfterViewInit {

    errorMessage: string;
    cates: Cate[] = [];
    cards: Card[] = [];

    userId: string;
    constructor(
        private router: Router,
        private _cardService: CardService,
        private _accountService: AccountService,
        private _notify: NotifyService
    ) { }

    ngOnInit() {
        this.loadCards();

    }

    loadCards() {
        this._accountService.getUserInfo().subscribe(res => {
            this._cardService.getCardsForUser(res.id).subscribe(cards => {
                this.cards = cards
            }
            );
        });
    }

    delete(id: number, title: string) {
        this._notify.printConfirmationDialog("Bạn thực sự muốn xóa thiệp này?",
            () => {
                this._accountService.getUserInfo().subscribe(res => {
                    this._cardService.deleteCard(id, res.id).subscribe(
                        res => {
                            if (res.succeeded) {
                                this._notify.printSuccessMessage("Thiệp " + title + " xóa thành công");
                            }
                        }
                    );
                })

            }
        );
    }
    ngAfterViewInit() {

    }
}