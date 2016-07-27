import {Component,OnInit} from '@angular/core';
import {ComingdaySharedComponent  } from '../shared-Components/coming-day.shared.component';

import {SliderComponent  } from '../shared-components/slider.shared.component';


@Component({
    templateUrl: 'app/home/home.component.html',
    directives: [ComingdaySharedComponent, SliderComponent] 
})
export class HomeComponent {

    errorMessage: string;

    constructor(
    ) { }
    
    
}