import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {

  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    let url: string = state.url;
    return true;
  }

  // async checkLogin(url: string): Promise<boolean> {
  //   if (this.authService.isAuthorized$) { return true; }

  //   this.authService.redirectUrl = url;
  //   this.router.navigate(['']);
  //   return false;
  // }
}