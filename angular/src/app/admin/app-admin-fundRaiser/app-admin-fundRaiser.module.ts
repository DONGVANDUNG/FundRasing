import { NgModule } from '@angular/core';
import { AppAdminFundRaiserComponent } from './app-admin-fundRaiser.component';
import { CommonModule } from '@angular/common';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppAdminFundRaiserRoutingModule } from './app-admin-fundRaiser-routing.module';
import { AppCommonModule } from '@app/shared/common/app-common.module';


@NgModule({
    declarations: [AppAdminFundRaiserComponent],
    imports: [AppSharedModule,AppCommonModule, AdminSharedModule, CommonModule, AppAdminFundRaiserRoutingModule],
})
export class AppAdminFundRaiserModule {}
