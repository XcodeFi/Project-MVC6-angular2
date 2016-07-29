import { Component, OnInit } from '@angular/core';
import {ROUTER_DIRECTIVES}from '@angular/router';


@Component({
    selector: 'footer-main',
    templateUrl: 'app/footer/footer.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class FooterComponent implements OnInit {
    constructor() { }
    ngOnInit() { }

}