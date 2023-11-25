import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserPostComponent } from './app-user-post.component';
import { AppUserPostRoutingModule } from './app-user-post-routing.module';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';
import { AdminSharedModule } from '../shared/admin-shared.module';

@NgModule({
  imports: [
    CommonModule,AppUserPostRoutingModule,AdminSharedModule,AccordionModule,SliderModule
  ],
  declarations: [AppUserPostComponent]
})
export class AppUserPostModule { }
