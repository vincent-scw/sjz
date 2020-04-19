import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable ,  Subscription } from 'rxjs';


import { AuthService } from '../services/auth.service';
import { AuthProviderBase } from '../services/auth-provider-base';

@Component({
  template: ``
})

export class AuthCallbackComponent implements OnInit, OnDestroy {
  private routeSubscription: Subscription;
  private typeSubscription: Subscription;

  authProvider: AuthProviderBase;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.typeSubscription = this.activatedRoute.paramMap.subscribe((params: Params) =>
      this.authProvider = this.getAuthProvider(params.get('type'))
    );
    this.routeSubscription = this.activatedRoute.queryParams.subscribe(async (params: Params) => {
      const code = this.authProvider.validateParams(params);
      if (code != null) {
        await this.authService.fetchUserInfo(this.authProvider.type, code);
      }

      this.router.navigateByUrl('');
    });
  }

  ngOnDestroy() {
    if (!!this.routeSubscription) { this.routeSubscription.unsubscribe(); }
    if (!!this.typeSubscription) { this.typeSubscription.unsubscribe(); }
  }

  private getAuthProvider(type: string): AuthProviderBase {
    const provider = AuthService.getProvider(type, this.authService);
    if (provider == null) { this.router.navigateByUrl(''); }
    return provider;
  }
}
