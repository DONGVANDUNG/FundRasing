import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppUserFundRaiserRoutingModule } from './app-user-fundraiser-routing.module';
import { AppUserFundraiserComponent } from './app-user-fundraiser.component';

@NgModule({
  imports: [
    CommonModule,AppUserFundRaiserRoutingModule
  ],
  declarations: [AppUserFundraiserComponent]
})
export class AppAdminRegisterFundraiserModule { }
