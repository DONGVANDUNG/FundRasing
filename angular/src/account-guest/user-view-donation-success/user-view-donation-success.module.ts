import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewDonationSuccessComponent } from './user-view-donation-success.component';
import { UserViewDonationSuccessRoutingModule } from './user-view-donation-success-routing.module';
@NgModule({
  imports: [
    CommonModule,UserViewDonationSuccessRoutingModule
  ],
  declarations: [UserViewDonationSuccessComponent
    ]
})
export class UserViewDonationSuccessModule { }
