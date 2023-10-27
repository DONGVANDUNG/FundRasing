import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminFundPackageComponent } from './app-admin-fund-package.component';
import { AppAdminFundPackageRouteModule } from './app-admin-fund-package-routing.module';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { CreateOrEditFundPackageComponent } from './create-or-edit-fund-package/create-or-edit-fund-package.component';

@NgModule({
  imports: [
    CommonModule,AppAdminFundPackageRouteModule,AdminSharedModule,AppSharedModule
  ],
  declarations: [AppAdminFundPackageComponent,CreateOrEditFundPackageComponent]
})
export class AppAdminFundPackageModule { }
