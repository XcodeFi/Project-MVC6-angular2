import {Component,OnInit} from '@angular/core';
import {ComingdaySharedComponent} from './coming-day.shared.component';
import {SlideService} from '../services/slide.service';
import {Slide} from '../models/models';


@Component({
    templateUrl: 'app/components/home.component.html',
    directives: [ComingdaySharedComponent]
})
export class HomeComponent implements OnInit {
    slides: Slide[] = [];

    errorMessage: string;

    constructor(
        private _slideService: SlideService
    ) { }
    
    ngOnInit() {
        this.getSlide();
    }
    getSlide() {
        this._slideService.getSlides()
            .subscribe(
            slides => this.slides = slides,
            error => this.errorMessage = <any>error);
    }
}