import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserCheckoutComponent } from './app-user-checkout.component';
import { AppUserCheckoutRoutingModule } from './app-user-checkout-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AccordionModule } from 'primeng/accordion';
@NgModule({
  imports: [
    CommonModule,AppUserCheckoutRoutingModule,AdminSharedModule,AccordionModule
  ],
  declarations: [AppUserCheckoutComponent
    ]
})
export class AppUserCheckoutModule { }
