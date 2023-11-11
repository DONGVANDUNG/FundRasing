import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDonationRoutingModule } from './app-user-donation-routing.module';
import { AppUserDonationComponent } from './app-user-donation.component';
import { DonationFundModule } from './donation-fund/donation-fund.module';
import { ViewAllFundModule } from './view-all-fund/view-all-fund.module';
@NgModule({
  imports: [
    CommonModule,AppUserDonationRoutingModule
  ],
  declarations: [AppUserDonationComponent
    ]
})
export class AppUserDonationModule { }
