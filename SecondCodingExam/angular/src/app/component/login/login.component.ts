import { Component } from '@angular/core';
import { Constants } from 'src/app/constant';
import { ILogin } from 'src/app/interface/login.interface';
import { AuthorizationService } from 'src/app/service/authorization/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  userCredentials: ILogin = {
    email: Constants.string.Empty,
    password: Constants.string.Empty
  };
  isLoading: boolean = false;
  hasError: boolean = false;
  
  constructor(private _authorizationService: AuthorizationService) { }
  async userLogin() {
    this.hasError = this.userCredentials.email == Constants.string.Empty || this.userCredentials.password == Constants.string.Empty;

    if (!this.hasError) {
      await this._authorizationService.login(this.userCredentials);
    }
  }
}
