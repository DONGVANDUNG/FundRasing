import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserBankComponent } from './app-user-bank.component';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppUserBankRoutingModule } from './app-user-bank-routing.module';

@NgModule({
  imports: [
    CommonModule,AdminSharedModule,AppUserBankRoutingModule
  ],
  declarations: [AppUserBankComponent]
})
export class AppUserBankModule { }
