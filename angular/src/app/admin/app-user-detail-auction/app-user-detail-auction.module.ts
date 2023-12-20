import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserDetailAuctionComponent } from './app-user-detail-auction.component';
import { AppUserDetailAuctionRoutingModule } from './app-user-detail-auction-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppUserDepositAuctionComponent } from './app-user-deposit-auction/app-user-deposit-auction.component';
import { TabViewModule } from 'primeng/tabview';

@NgModule({
  imports: [
    CommonModule,AppUserDetailAuctionRoutingModule,AdminSharedModule,TabViewModule
  ],
  declarations: [AppUserDetailAuctionComponent,AppUserDepositAuctionComponent]
})
export class AppUserDetailAuctionModule { }
