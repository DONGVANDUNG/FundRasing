import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminFundraisingComponent } from './app-admin-fundraising.component';
import { AppAdminFundRaisingRoutingModule } from './app-admin-fundraising-routing.module';
import { CreateOrEditFundraisingComponent } from './create-or-edit-fundraising/create-or-edit-fundraising.component';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { CalendarModule } from 'primeng/calendar';
@NgModule({
  imports: [
    CommonModule,AppAdminFundRaisingRoutingModule,AdminSharedModule,AccordionModule,SliderModule,CalendarModule
  ],
  declarations: [AppAdminFundraisingComponent,CreateOrEditFundraisingComponent]
})
export class AppAdminFundraisingModule { }
