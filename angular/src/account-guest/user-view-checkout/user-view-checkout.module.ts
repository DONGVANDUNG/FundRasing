import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewCheckoutComponent } from './user-view-checkout.component';
import { UserViewCheckoutRoutingModule } from './user-view-checkout-routing.module';
@NgModule({
  imports: [
    CommonModule,UserViewCheckoutRoutingModule
  ],
  declarations: [UserViewCheckoutComponent
    ]
})
export class UserViewCheckoutModule { }
