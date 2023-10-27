import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminFundTransactionRouteModule } from './app-admin-fund-transaction-routing.module';
import { AppAdminFundTransactionComponent } from './app-admin-fund-transaction.component';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { ViewDetailTransactionComponent } from './view-detail-transaction/view-detail-transaction.component';

@NgModule({
  imports: [
    CommonModule,AppAdminFundTransactionRouteModule,AdminSharedModule,AppSharedModule
  ],
  declarations: [AppAdminFundTransactionComponent,ViewDetailTransactionComponent]
})
export class AppAdminFundTransactionModule { }
