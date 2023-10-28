import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminFundraisingComponent } from './app-admin-fundraising.component';
import { AppAdminFundRaisingRoutingModule } from './app-admin-fundraising-routing.module';

@NgModule({
  imports: [
    CommonModule,AppAdminFundRaisingRoutingModule
  ],
  declarations: [AppAdminFundraisingComponent]
})
export class AppAdminFundraisingModule { }
