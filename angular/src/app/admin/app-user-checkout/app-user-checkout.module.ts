import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserCheckoutComponent } from './app-user-checkout.component';
import { AppUserCheckoutRoutingModule } from './app-user-checkout-routing.module';
@NgModule({
  imports: [
    CommonModule,AppUserCheckoutRoutingModule
  ],
  declarations: [AppUserCheckoutComponent
    ]
})
export class AppUserCheckoutModule { }
