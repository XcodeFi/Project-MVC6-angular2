/// <reference path="../../typings/index.d.ts" />

import {disableDeprecatedForms, provideForms} from '@angular/forms';
// The usual bootstrapping imports
import { bootstrap }    from '@angular/platform-browser-dynamic';
import { AppComponent } from './app.component';
import {APP_ROUTER_PROVIDERS} from './app.routes'

import { HTTP_PROVIDERS } from '@angular/http';

bootstrap(AppComponent,
    [APP_ROUTER_PROVIDERS, HTTP_PROVIDERS,
        disableDeprecatedForms(),
        provideForms()
    ]).catch(err => console.log(err));
