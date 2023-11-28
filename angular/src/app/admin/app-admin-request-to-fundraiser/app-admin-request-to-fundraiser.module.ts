import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminRequestToFundraiserComponent } from './app-admin-request-to-fundraiser.component';
import { AppAdminRequestToFundRaiserRoutingModule } from './app-admin-request-to-fundraiser-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';

@NgModule({
  imports: [
    CommonModule,AppAdminRequestToFundRaiserRoutingModule,AdminSharedModule
  ],
  declarations: [AppAdminRequestToFundraiserComponent]
})
export class AppAdminRequestToFundraiserModule { }
