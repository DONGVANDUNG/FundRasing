import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppUserAuctionRoutingModule } from './app-user-auction-routing.module';
import { AppUserAutionComponent } from './app-user-aution.component';

@NgModule({
  imports: [
    CommonModule,AppUserAuctionRoutingModule
  ],
  declarations: [AppUserAutionComponent]
})
export class AppUserAutionModule { }
