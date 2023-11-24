import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDonationRoutingModule } from './app-user-donation-routing.module';
import { AppUserDonationComponent } from './app-user-donation.component';
import { AccordionModule } from 'primeng/accordion';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { SliderModule } from 'primeng/slider';
@NgModule({
  imports: [
    CommonModule,AppUserDonationRoutingModule,AccordionModule,AdminSharedModule,SliderModule
  ],
  declarations: [AppUserDonationComponent
    ]
})
export class AppUserDonationModule { }
