import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppAdminAccountFundraisingComponent } from './app-admin-account-fundraising.component';
import { AppAdminAccountFundraisingRouteModule } from './app-admin-account-routing.module';
import { AppAdminWarningAccountComponent } from './app-admin-warning-account/app-admin-warning-account.component';


@NgModule({
    declarations: [AppAdminAccountFundraisingComponent,AppAdminWarningAccountComponent],
    imports: [AppSharedModule, AdminSharedModule, CommonModule, AppAdminAccountFundraisingRouteModule],
})
export class AppAdminFundRaiserModule {}
