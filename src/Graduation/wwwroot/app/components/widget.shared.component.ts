import {Component} from '@angular/core';
import {ComingdaySharedComponent} from './coming-day.shared.component';


@Component({
    templateUrl: 'app/components/widget.shared.component.html',
    directives: [ComingdaySharedComponent],
    selector:'widget'
})

export class WidgetSharedComponent { }