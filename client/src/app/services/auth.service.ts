import { Injectable, OnDestroy } from '@angular/core';
import {
  OidcSecurityService, OpenIdConfiguration, AuthWellKnownEndpoints,
  AuthorizationResult, AuthorizationState
} from 'angular-auth-oidc-client';
import { Subscription, Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { User } from '../models/user.model';

@Injectable()
export class AuthService implements OnDestroy {
  isAuthorized = false;
  private isAuthorizedSubscription: Subscription = new Subscription;

  constructor(private oidcSecurityService: OidcSecurityService,
    private router: Router) { }

  ngOnDestroy(): void {
    if (this.isAuthorizedSubscription) { this.isAuthorizedSubscription.unsubscribe(); }
  }

  public initAuth() {
    const authUrl = environment.authSvcUrl;
    const openIdConfiguration: OpenIdConfiguration = {
      stsServer: authUrl,
      redirect_url: environment.selfUrl,
      client_id: 'spa',
      response_type: 'code',
      scope: 'openid profile timelineapi ups',
      post_login_route: '/',
      forbidden_route: '/forbidden',
      unauthorized_route: '/unauthorized',
      post_logout_redirect_uri: environment.selfUrl,
      silent_renew: true,
      silent_renew_url: `${environment.selfUrl}/silent-renew.html`,
      silent_renew_offset_in_seconds: 60,
      history_cleanup_off: true,
      auto_userinfo: true,
      log_console_warning_active: true,
      log_console_debug_active: false,
      max_id_token_iat_offset_allowed_in_seconds: 10
    };

    const authWellKnownEndpoints: AuthWellKnownEndpoints = {
      issuer: authUrl,
      jwks_uri: authUrl + '/.well-known/openid-configuration/jwks',
      authorization_endpoint: authUrl + '/connect/authorize',
      token_endpoint: authUrl + '/connect/token',
      userinfo_endpoint: authUrl + '/connect/userinfo',
      end_session_endpoint: authUrl + '/connect/endsession',
      check_session_iframe: authUrl + '/connect/checksession',
      revocation_endpoint: authUrl + '/connect/revocation',
      introspection_endpoint: authUrl + '/connect/introspect'
    };

    this.oidcSecurityService.setupModule(openIdConfiguration, authWellKnownEndpoints);

    if (this.oidcSecurityService.moduleSetup) {
      this.doCallbackLogicIfRequired();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.doCallbackLogicIfRequired();
      });
    }
    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe((isAuthorized => {
      this.isAuthorized = isAuthorized;
    }));

    this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
        this.onAuthorizationResultComplete(authorizationResult);
      });
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
    if (authorizationResult.authorizationState === AuthorizationState.unauthorized) {
      if (window.parent) {
        // sent from the child iframe, for example the silent renew
        this.router.navigate(['/unauthorized']);
      } else {
        window.location.href = '/unauthorized';
      }
    }
  }

  private doCallbackLogicIfRequired() {

    this.oidcSecurityService.authorizedCallbackWithCode(window.location.toString());
  }

  get isAuthorized$(): Observable<boolean> {
    return this.oidcSecurityService.getIsAuthorized();
  }

  get userData$(): Observable<User>{
    return this.oidcSecurityService.getUserData<{ sub: string, name: string }>();
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  get accessToken(): string {
    const token = this.oidcSecurityService.getToken();
    return token;
  }


}