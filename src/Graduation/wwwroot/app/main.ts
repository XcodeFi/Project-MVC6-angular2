/// <reference path="../../typings/index.d.ts" />

import {disableDeprecatedForms, /*provideForms*/} from '@angular/forms';

import { bootstrap }        from '@angular/platform-browser-dynamic';
import { appRouterProviders } from './app.routes';
import {Location, LocationStrategy, HashLocationStrategy} from '@angular/common';
import { HTTP_PROVIDERS } from '@angular/http';

import { AppComponent }     from './app.component';

bootstrap(AppComponent, [
    appRouterProviders,//for router
    HTTP_PROVIDERS,//for service 
    disableDeprecatedForms(),
     { provide: LocationStrategy, useClass: HashLocationStrategy }//for router
]).catch(err => console.log(err));

