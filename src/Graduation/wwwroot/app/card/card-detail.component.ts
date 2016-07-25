import {Component,
    Input,
    AfterViewInit,
    OnInit,
    OnDestroy} from '@angular/core';
import {Router, RouterLink, ActivatedRoute} from '@angular/router';
import {Card} from '../models/models';
import {Cate} from '../models/models';
import {CardService} from './card.service';

import {CateService} from '../cards/cards.service';


//import {WidgetSharedComponent} from './widget.shared.component';

declare var $: JQueryStatic;

@Component({
    templateUrl: 'app/card/card-detail.component.html',
    providers: [CardService]
})

export class CardDetailComponent implements OnInit, AfterViewInit {

    card: Card;
    error: any;
    navigated = false; // true if navigated here
    public cate: Cate;
    constructor(private _cardService: CardService,
        private _cateService: CateService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            let id = +params['id'];
            if (id !== undefined) {
                this.navigated = true;
                this._cardService.getCard(id).subscribe
                    (card => {
                        this.card = card;
                    });
            } else {
                this.navigated = false;
                //this.card = new Card();
            }
        })
    }
    goBack() {
        window.history.back();
    }

    ngAfterViewInit() {
        //$(function () {
        //    $('#eSendDate').datetimepicker({ format: 'MM/DD/YYYY', defaultDate: moment() });
        //});
        //$(".toggle-social-buttons").click(function () {
        //    var shareButtonRow = $(this).closest(".caption").find(".share-button-row");
        //    var socialButtonRow = $(this).closest(".caption").find(".social-button-row");
        //    if ($(shareButtonRow).hasClass("hidden")) {
        //        $(shareButtonRow).removeClass("hidden");
        //        $(socialButtonRow).addClass("hidden");
        //    }
        //    else {
        //        $(shareButtonRow).addClass("hidden");
        //        $(socialButtonRow).removeClass("hidden");
        //    }
        //});
        //$("fb-root").html=("<script>" +
        //                    "(function (d, s, id) {"+
        //                        var js, fjs = d.getElementsByTagName(s)[0];
        //                        if (d.getElementById(id)) return;
        //                        js = d.createElement(s); js.id = id;
        //                        js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5&appId=467222143374291";
        //                        fjs.parentNode.insertBefore(js, fjs);
        //                    }(document, 'script', 'facebook-jssdk'));</script>";
    }
}