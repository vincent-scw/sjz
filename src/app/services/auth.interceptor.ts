import { Injectable } from '@angular/core';
import {
  HttpInterceptor, HttpRequest,
  HttpHandler, HttpHeaderResponse, HttpProgressEvent,
  HttpResponse, HttpSentEvent, HttpUserEvent
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authSvc: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler)
    : Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    if (this.authSvc.isAccessTokenValid()) {
      const jwtReq = req.clone({ headers: req.headers.set('Authorization', `Bearer ${this.authSvc.accessToken}`) });
      return next.handle(jwtReq);
    }

    return next.handle(req);
  }
}