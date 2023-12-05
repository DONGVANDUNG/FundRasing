import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminPostComponent } from './app-admin-post.component';
import { AppAdminPostRoutingModule } from './app-admin-post-routing.module';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { CarouselModule } from 'primeng/carousel';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';

import { CreateOrEditFundraisingComponent } from './create-or-edit-fundraising/create-or-edit-fundraising.component';
@NgModule({
  imports: [
    CarouselModule,
    CommonModule,AppAdminPostRoutingModule,AdminSharedModule,AccordionModule,SliderModule,
  ],
  declarations: [AppAdminPostComponent,CreateOrEditPostComponent,CreateOrEditFundraisingComponent]
})
export class AppAdminPostModule { }
