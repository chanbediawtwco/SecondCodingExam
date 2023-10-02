import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { BenefitsComponent } from './component/benefits/benefits.component';
import { AuthorizationGuardService } from './service/authorization/authorization.guard.service';
import { ErrorComponent } from './component/error/error.component';
import { CustomersComponent } from './component/customers/customers.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: 'home', component: HomeComponent },
  { path: 'benefit', component: BenefitsComponent, canActivate: [AuthorizationGuardService] },
  { path: 'customer', component: CustomersComponent, canActivate: [AuthorizationGuardService] },
  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
