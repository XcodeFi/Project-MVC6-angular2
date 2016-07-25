import {Component,OnInit} from '@angular/core';
import {ComingdaySharedComponent  } from '../shared-Components/coming-day.shared.component';

@Component({
    templateUrl: 'app/home/home.component.html',
    directives: [ComingdaySharedComponent] 
})
export class HomeComponent {

    errorMessage: string;

    constructor(
    ) { }
    
    
}