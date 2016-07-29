/// <reference path="../../typings/index.d.ts" />

import {disableDeprecatedForms, provideForms} from '@angular/forms';

import { bootstrap }        from '@angular/platform-browser-dynamic';
import { appRouterProviders } from './app.routes';
import {Location, LocationStrategy, HashLocationStrategy} from '@angular/common';
import { HTTP_PROVIDERS } from '@angular/http';

import { AppComponent }     from './app.component';

bootstrap(AppComponent, [
    HTTP_PROVIDERS,//for service 
    disableDeprecatedForms(),
    provideForms(),
    appRouterProviders,//for router
     { provide: LocationStrategy, useClass: HashLocationStrategy }//for router
]).then(success => console.log(`Bootstrap success`))
    .catch(error => console.log(error));

