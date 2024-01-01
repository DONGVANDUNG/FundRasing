import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewFundDetailComponent } from './user-view-fund-detail.component';
import { UserViewFundDetailRoutingModule } from './user-view-fund-detail-routing.module';
import { TabViewModule } from 'primeng/tabview';
import { CarouselModule } from 'primeng/carousel';
import { AppSharedModule } from '@app/shared/app-shared.module';

@NgModule({
  imports: [
    CommonModule,UserViewFundDetailRoutingModule,TabViewModule,CarouselModule,AppSharedModule
  ],
  declarations: [UserViewFundDetailComponent]
})
export class UserViewFundDetailModule { }
