import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserPostDetailComponent } from './app-user-post-detail.component';
import { AppUserPostDetailRoutingModule } from './app-user-post-detail-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { SliderModule } from 'primeng/slider';
import { LoadingAdminComponent } from '../app-admin-post/loadingAdmin';
import { AccordionModule } from 'primeng/accordion';
import { TabViewModule } from 'primeng/tabview';
@NgModule({
  imports: [
    CommonModule,AppUserPostDetailRoutingModule,AdminSharedModule,AccordionModule,SliderModule,TabViewModule
  ],
  declarations: [AppUserPostDetailComponent,LoadingAdminComponent]
})
export class AppUserPostDetailModule { }
