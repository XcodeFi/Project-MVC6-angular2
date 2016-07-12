import {Component, Input,OnInit,OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Card} from '../models/models';
import {Cate} from '../models/models';

import {CardService} from '../services/card.service';
import {CateService} from '../services/cate.service';


import {WidgetSharedComponent} from './widget.shared.component';


@Component({
    templateUrl: 'app/components/card-detail.component.html',
    directives: [WidgetSharedComponent]
})

export class CardDetailComponent implements OnInit, OnDestroy {

    @Input()
    card: Card;
    error: any;
    sub: any;
    navigated = false; // true if navigated here
    public cate: Cate;
    constructor(private _cardService: CardService,
        private _cateService: CateService,
        private _router: ActivatedRoute
    ) { }

    ngOnInit() {
        this.sub = this._router.params.subscribe(params => {
            if (params['id'] !== undefined) {
                let id = +params['id'];
                this.navigated = true;
                this._cardService.getCard(id).subscribe
                    (card => {
                        this.card = card;
                    });
            } else {
                this.navigated = false;
                //this.card = new Card();
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    goBack() {
        window.history.back();
    }
}