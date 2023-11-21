import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminPostComponent } from './app-admin-post.component';
import { AppAdminPostRoutingModule } from './app-admin-post-routing.module';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AppAdminViewDetailPostComponent } from './app-admin-view-detail-post/app-admin-view-detail-post.component';
import { CarouselModule } from 'primeng/carousel';

@NgModule({
  imports: [
    CarouselModule,
    CommonModule,AppAdminPostRoutingModule,AdminSharedModule,
  ],
  declarations: [AppAdminPostComponent,CreateOrEditPostComponent,AppAdminViewDetailPostComponent]
})
export class AppAdminPostModule { }
