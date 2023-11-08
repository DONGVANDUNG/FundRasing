import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDonationRoutingModule } from './app-user-donation-routing.module';
import { AppUserDonationComponent } from './app-user-donation.component';
@NgModule({
  imports: [
    CommonModule,AppUserDonationRoutingModule
  ],
  declarations: [AppUserDonationComponent
    ]
})
export class AppUserDonationModule { }
