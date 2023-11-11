import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonationFundComponent } from './donation-fund.component';
import { DonationFundRoutingModule } from './donation-fund-routing.module';

@NgModule({
  imports: [
    CommonModule,DonationFundRoutingModule
  ],
  declarations: [DonationFundComponent]
})
export class DonationFundModule { }
