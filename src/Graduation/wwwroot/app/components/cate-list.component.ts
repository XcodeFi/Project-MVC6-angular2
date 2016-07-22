import {Component,OnInit} from '@angular/core';
import {CateService} from '../services/cate.service';
import {Cate} from '../models/models';
@Component({
    selector: 'cate-list',
    templateUrl: 'app/components/cate-list.component.html',
    styleUrls:['css/cate-list.component.css']
})

export class CateListComponent implements OnInit {  

    errorMessage: string;
    cates: Cate[] = [];
    constructor(private _cateService: CateService) { }

    ngOnInit() {
        this.getCate();
    }
    getCate() {
        this._cateService.getCates()
            .subscribe(
            cates => this.cates = cates,
            error => this.errorMessage = <any>error);
    }
}