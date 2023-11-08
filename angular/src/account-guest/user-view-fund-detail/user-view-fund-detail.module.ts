import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewFundDetailComponent } from './user-view-fund-detail.component';
import { UserViewFundDetailRoutingModule } from './user-view-fund-detail-routing.module';

@NgModule({
  imports: [
    CommonModule,UserViewFundDetailRoutingModule
  ],
  declarations: [UserViewFundDetailComponent]
})
export class UserViewFundDetailModule { }
