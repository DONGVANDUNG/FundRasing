import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserAutionComponent } from './app-user-aution.component';
import { AppUserBankRoutingModule } from './app-user-auction-routing.module';

@NgModule({
  imports: [
    CommonModule,AppUserBankRoutingModule
  ],
  declarations: [AppUserAutionComponent]
})
export class AppUserAutionModule { }
