import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminPostComponent } from './app-admin-post.component';
import { AppAdminPostRoutingModule } from './app-admin-post-routing.module';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { CarouselModule } from 'primeng/carousel';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';
@NgModule({
  imports: [
    CarouselModule,
    CommonModule,AppAdminPostRoutingModule,AdminSharedModule,AccordionModule,SliderModule
  ],
  declarations: [AppAdminPostComponent,CreateOrEditPostComponent]
})
export class AppAdminPostModule { }
