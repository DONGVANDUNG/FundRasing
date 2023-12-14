import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewFundDetailComponent } from './user-view-fund-detail.component';
import { UserViewFundDetailRoutingModule } from './user-view-fund-detail-routing.module';
import { TabViewModule } from 'primeng/tabview';
import { CarouselModule } from 'primeng/carousel';

@NgModule({
  imports: [
    CommonModule,UserViewFundDetailRoutingModule,TabViewModule,CarouselModule
  ],
  declarations: [UserViewFundDetailComponent]
})
export class UserViewFundDetailModule { }
