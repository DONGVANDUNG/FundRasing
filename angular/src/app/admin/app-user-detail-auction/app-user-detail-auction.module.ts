import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDetailAuctionComponent } from './app-user-detail-auction.component';
import { AppUserDetailAuctionRoutingModule } from './app-user-detail-auction-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';

@NgModule({
  imports: [
    CommonModule,AppUserDetailAuctionRoutingModule,AdminSharedModule
  ],
  declarations: [AppUserDetailAuctionComponent]
})
export class AppUserDetailAuctionModule { }
