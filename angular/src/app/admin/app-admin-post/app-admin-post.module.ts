import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminPostComponent } from './app-admin-post.component';
import { AppAdminPostRoutingModule } from './app-admin-post-routing.module';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppSharedModule } from '@app/shared/app-shared.module';

@NgModule({
  imports: [
    CommonModule,AppAdminPostRoutingModule,AdminSharedModule
  ],
  declarations: [AppAdminPostComponent,CreateOrEditPostComponent]
})
export class AppAdminPostModule { }
