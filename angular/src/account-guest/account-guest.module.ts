import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountGuestComponent } from './account-guest.component';
import { AccountGuestRoutingModule } from './account-guest-routing.module';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FundPackageComponent } from './fund-package/fund-package.component';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites/fundraising-live-sites.component';
import { UserHomeComponent } from './user-home/user-home.component';
import { UserLoginComponent } from './user-login/user-login.component';

@NgModule({
  imports: [
    CommonModule,AccountGuestRoutingModule
  ],
  declarations: [AccountGuestComponent,ContactUsComponent,
    FundPackageComponent, FundraisingLiveSitesComponent, UserHomeComponent
    , UserLoginComponent]
})
export class AccountGuestModule { }
