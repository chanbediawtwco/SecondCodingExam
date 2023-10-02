import { Component } from '@angular/core';
import { AuthorizationService } from 'src/app/service/authorization/authorization.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {
  constructor(private _authorizationService: AuthorizationService) {}
  
  isLoggedIn: Boolean = false;

  ngOnInit() {
    this._authorizationService.isUserLoggedIn.subscribe((value) => {
      this.isLoggedIn = value;
    });
  }
  
  // register() {
  //   this._authorizationService.registerUser();
  // }
  
  logout() {
    this._authorizationService.logout();
    this.isLoggedIn = this._authorizationService.isLoggedIn();
  }
}
