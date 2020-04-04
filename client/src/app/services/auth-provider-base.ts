import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { environment } from '../../environments/environment';

export abstract class AuthProviderBase {
  public abstract get type(): string;
  public abstract signIn();

  public validateParams(params: Params): string {
    const state = params['state'];
    if (state != null && environment.state) {
      const code = params['code'];
      if (code != null) {
        return code;
      }
    }

    return null;
  }
}
