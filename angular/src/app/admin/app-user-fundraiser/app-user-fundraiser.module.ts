import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppUserFundRaiserRoutingModule } from './app-user-fundraiser-routing.module';
import { AppUserFundraiserComponent } from './app-user-fundraiser.component';
import { AdminSharedModule } from '../shared/admin-shared.module';

@NgModule({
  imports: [
    CommonModule,AppUserFundRaiserRoutingModule,AdminSharedModule
  ],
  declarations: [AppUserFundraiserComponent]
})
export class AppAdminRegisterFundraiserModule { }
