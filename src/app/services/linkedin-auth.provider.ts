import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { AuthProviderBase } from './auth-provider-base';

export class LinkedInAuthProvider extends AuthProviderBase {
  public type = 'linkedin';
  constructor(private authService: AuthService) {
    super();
  }

  signIn() {
    const clientId = environment.linkedIn.clientId;
    const redirect_uri = `${location.href.substring(0, location.href.indexOf(location.pathname))}/oauth2/linkedin`;
    const scope = environment.linkedIn.scope;
    const state = environment.state;
    window.location.href = 'https://www.linkedin.com/uas/oauth2/authorization?'
      + 'response_type=code'
      + `&client_id=${clientId}`
      + `&redirect_uri=${redirect_uri}`
      + `&state=${state}&scope=${scope}`;
  }
}
