import { NgModule } from '@angular/core';
import { AppAdminFundRaiserComponent } from './app-admin-fundRaiser.component';
import { CommonModule } from '@angular/common';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppAdminFundRaiserRoutingModule } from './app-admin-fundRaiser-routing.module';


@NgModule({
    declarations: [AppAdminFundRaiserComponent],
    imports: [AppSharedModule, AdminSharedModule, CommonModule, AppAdminFundRaiserRoutingModule],
})
export class AppAdminFundRaiserModule {}
