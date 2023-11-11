import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DonationSuccessRoutingModule } from './donation-success-routing.module';
import { DonationSuccessComponent } from './donation-success.component';

@NgModule({
  imports: [
    CommonModule,DonationSuccessRoutingModule
  ],
  declarations: [DonationSuccessComponent
    ]
})
export class DonationSuccessModule { }
