import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { SessionStorageService } from 'angular-web-storage';

@Injectable({ providedIn: 'root' })
export class EditorGuard implements CanActivate {
  constructor(
    private router: Router,
    private sessin: SessionStorageService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.sessin.get('loggedUser');
    if (currentUser.role == "Editor") {

      return true;
    }

    this.router.navigate(['/'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
