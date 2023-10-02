import { Component } from '@angular/core';
import { Constants } from 'src/app/constant';
import { IUser } from 'src/app/interface/user.interface';
import { AuthorizationService } from 'src/app/service/authorization/authorization.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  constructor(private _authorizationService: AuthorizationService) {}
  userRegistration: IUser = {
    firstname: Constants.string.Empty,
    lastname: Constants.string.Empty,
    email: Constants.string.Empty,
    password: Constants.string.Empty,
  };
  hasError: boolean = false;
  errorMessage: string = Constants.string.Empty;
  register(){
    if (this.userRegistration.firstname == Constants.string.Empty
    || this.userRegistration.lastname == Constants.string.Empty 
    || this.userRegistration.email == Constants.string.Empty 
    || this.userRegistration.password == Constants.string.Empty) {
      this.setErrors(Constants.Error.MissingInformation);
    }
    
    if (!this.hasError) {
      this._authorizationService.register(this.userRegistration);
    }
  }

  setErrors(error: string) {
    this.hasError = true;
    this.errorMessage = error;
  }
}
