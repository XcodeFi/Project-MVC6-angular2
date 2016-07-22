/// <reference path="../../typings/index.d.ts" />

import {disableDeprecatedForms, provideForms} from '@angular/forms';
// The usual bootstrapping imports
import { bootstrap }    from '@angular/platform-browser-dynamic';
import { AppComponent } from './app.component';
import {ROUTER_PROVIDERS} from '@angular/router-deprecated';
import {Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

import { HTTP_PROVIDERS } from '@angular/http';

bootstrap(AppComponent,
    [ROUTER_PROVIDERS, HTTP_PROVIDERS,
        disableDeprecatedForms(),
        provideForms(),
        { provide: LocationStrategy, useClass: PathLocationStrategy }
    ]).catch(err => console.log(err));
