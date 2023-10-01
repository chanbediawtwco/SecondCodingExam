import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { IUser } from 'src/app/interface/user.interface';
import { IToken } from 'src/app/interface/token.interface';
import { ILogin } from 'src/app/interface/login.interface';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  constructor(private _httpClient: HttpClient, 
    public router: Router) { }

  isUserLoggedIn: Subject<any> = new Subject<any>();

  register(user: IUser) {  
    return this._httpClient.post(`${environment.uri}/account/register`, user)
      .subscribe(response => { return response });
  }

  login(user: ILogin) {
    return this._httpClient.post<any>(`${environment.uri}/account/login`, user)
      .subscribe((res: IToken) => {
        localStorage.setItem('access_token', res.token)
        this.router.navigate(['/benefit']);
      })
  }

  getAccessToken() {
    return localStorage.getItem('access_token');
  }

  isLoggedIn(): boolean {
    let authToken = localStorage.getItem('access_token');
    this.isUserLoggedIn.next(authToken !== null);
    return authToken !== null;
  }

  logout() {
    if (localStorage.removeItem('access_token') == null) {
      this.router.navigate(['/home']);
    }
  }
}
