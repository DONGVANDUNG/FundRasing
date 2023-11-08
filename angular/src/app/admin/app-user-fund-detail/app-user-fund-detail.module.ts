import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserFundDetailComponent } from './app-user-fund-detail.component';
import { AppUserFundDetailRoutingModule } from './app-user-fund-detail-routing.module';

@NgModule({
  imports: [
    CommonModule,AppUserFundDetailRoutingModule
  ],
  declarations: [AppUserFundDetailComponent]
})
export class AppUserFundDetailModule { }
