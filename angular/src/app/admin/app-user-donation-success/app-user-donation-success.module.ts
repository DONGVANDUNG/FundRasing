import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDonationSuccessComponent } from './app-user-donation-success.component';
import { AppUserDonationSuccessRoutingModule } from './app-user-donation-success-routing.module';
@NgModule({
  imports: [
    CommonModule,AppUserDonationSuccessRoutingModule
  ],
  declarations: [AppUserDonationSuccessComponent
    ]
})
export class AppUserDonationSuccessModule { }
