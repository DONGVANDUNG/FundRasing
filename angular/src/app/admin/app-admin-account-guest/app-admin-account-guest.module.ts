import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppAdminAccountGuestRouteModule } from './app-admin-account-guest-routing.module';
import { AppAdminAccountGuestComponent } from './app-admin-account-guest.component';


@NgModule({
    declarations: [AppAdminAccountGuestComponent],
    imports: [AppSharedModule, AdminSharedModule, CommonModule, AppAdminAccountGuestRouteModule],
})
export class AppAdminFundRaiserModule {}
