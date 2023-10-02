import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizationService } from './authorization.service';
import { SharedService } from '../shared/shared.service';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationInterceptorService implements HttpInterceptor {
  constructor(private _authorizationService: AuthorizationService,
    private _sharedService: SharedService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
      const accessToken = this._authorizationService.getAccessToken();
      req = accessToken ? req.clone({
          headers: req.headers.set("Authorization", "Bearer " + accessToken) 
      }) : req;
      return next.handle(req)
      .pipe(
          catchError(this._sharedService.handleError)
      );
  }
}
