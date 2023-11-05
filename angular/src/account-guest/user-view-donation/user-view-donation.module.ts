import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewDonationComponent } from './user-view-donation.component';
import { UserViewDonationRoutingModule } from './user-view-donation-routing.module';
@NgModule({
  imports: [
    CommonModule,UserViewDonationRoutingModule
  ],
  declarations: [UserViewDonationComponent
    ]
})
export class UserViewDonationModule { }
