import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FundPackageComponent } from './fund-package/fund-package.component';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites/fundraising-live-sites.component';
import { UserHomeComponent } from './user-home/user-home.component';
import { UserLoginComponent } from './user-login/user-login.component';
// import { UserRoutingModule } from './user-routing.module';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [UserComponent,ContactUsComponent,
    FundPackageComponent, FundraisingLiveSitesComponent, UserHomeComponent
    , UserLoginComponent]
  // exports:[UserComponent, ContactUsComponent,
  //   FundPackageComponent, FundraisingLiveSitesComponent, UserHomeComponent
  //   , UserLoginComponent]
})
export class UserModule { }
