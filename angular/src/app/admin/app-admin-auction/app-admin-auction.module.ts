import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminAuctionComponent } from './app-admin-auction.component';
import { AppAdminFundPackageRouteModule } from './app-admin-auction-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { CreateOrEditAuctionComponent } from './create-or-edit-auction/create-or-edit-auction.component';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';
import { UploadComponent } from '../p-fileupload/upload-image.component';
import { UploadImageAuctionComponent } from '../p-fileupload/p-fileupload.component';

@NgModule({
  imports: [
    CommonModule,AppAdminFundPackageRouteModule,AdminSharedModule,AccordionModule,SliderModule
  ],
  declarations: [AppAdminAuctionComponent,CreateOrEditAuctionComponent,UploadImageAuctionComponent]
})
export class AppAdminAuctionModule { }
