import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminListRegisterFundpackageComponent } from './app-admin-list-register-fundpackage.component';
import { AppAdminListRegisterFundPackageRoutingModule } from './app-admin-list-register-fundpackage-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppCommonModule } from '@app/shared/common/app-common.module';

@NgModule({
  imports: [
    CommonModule,AppAdminListRegisterFundPackageRoutingModule,AppCommonModule, AdminSharedModule,
  ],
  declarations: [AppAdminListRegisterFundpackageComponent]
})
export class AppAdminListRegisterFundpackageModule { }
