import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserHistoryAuctionComponent } from './app-user-history-auction.component';
import { AppUserHistoryAuctionRoutingModule } from './app-user-history-auction-routing.module';

@NgModule({
  imports: [
    CommonModule,AppUserHistoryAuctionRoutingModule
  ],
  declarations: [AppUserHistoryAuctionComponent]
})
export class AppUserHistoryAuctionModule { }
