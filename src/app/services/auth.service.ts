import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { AuthProviderBase } from './auth-provider-base';
import { LinkedInAuthProvider } from './linkedin-auth.provider';

import { environment } from '../../environments/environment';
import { UserWithToken, User } from '../models/user.model';
import { Router } from '@angular/router';

const AccessToken_CacheKey = 'access_token';
const User_CacheKey = 'user_info';

@Injectable()
export class AuthService {
  isLoggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  redirectUrl: string;

  public static getProvider(accountType: string, authService: AuthService)
    : AuthProviderBase {
    switch (accountType) {
      case 'linkedin':
        return new LinkedInAuthProvider(authService);
    }
  }

  constructor(private http: HttpClient, private router: Router) {
    this.isLoggedIn$.next(this.isAccessTokenValid());
    //this.isLoggedIn$.next(true);
  }

  get accessToken(): string {
    return localStorage.getItem(AccessToken_CacheKey);
  }

  set accessToken(token: string) {
    localStorage.setItem(AccessToken_CacheKey, token);
  }

  get user(): User {
    const userInfo = localStorage.getItem(User_CacheKey);
    return userInfo == null ? null : JSON.parse(userInfo);
  }

  // After updating user info, allow store user, so make it public
  set user(value: User) {
    localStorage.setItem(User_CacheKey, JSON.stringify(value));
    // this.user.next(user);
  }

  login(): Observable<boolean> {
    return null;
  }

  logout(): void {
    localStorage.removeItem(AccessToken_CacheKey);
    localStorage.removeItem(User_CacheKey);
    this.isLoggedIn$.next(false);
    this.router.navigate(['']);
  }

  public async fetchUserInfo(type: string, code: string) {
    const userWithToken = await this.http.post<UserWithToken>(
      `${environment.apiServerUrl}/api/Auth`, { accountType: type, code: code }).toPromise();
    
    if (userWithToken != null && userWithToken.user != null) {
      this.user = userWithToken.user;
      this.accessToken = userWithToken.accessToken;
      this.isLoggedIn$.next(true);
    }
  }

  public isAccessTokenValid(): boolean {
    const accessToken = this.accessToken;
    if (accessToken == null) {
      return false;
    } else {
      return tokenNotExpired(null, accessToken);
    }
  }
}