import { Injectable }             from '@angular/core';
import { CanActivate,
    Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot }    from '@angular/router';
import { AccountService }            from '../account/account.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AccountService, private router: Router) { }

    canActivate(
        // Not using but worth knowing about
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ) {
        this.authService.isUserAuthenticated();
        if (this.authService.isUserAuthenticated()) { return true; }
        this.router.navigate(['/home']);
        return false;
    }
}


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/