import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ValidationService } from './validate.service';

@Component({
    selector: 'control-messages',
    template: `<small *ngIf="errorMessage !== null" class="text-danger"><i class="glyphicon glyphicon-exclamation-sign"></i>{{errorMessage}}</small>`
})
export class ControlMessages {
    @Input() control: FormControl;
    constructor() { }

    get errorMessage() {
        for (let propertyName in this.control.errors) {
            if (this.control.errors.hasOwnProperty(propertyName) && this.control.touched) {
                return ValidationService.getValidatorErrorMessage(propertyName, this.control.errors[propertyName]);
            }
        }

        return null;
    }
}