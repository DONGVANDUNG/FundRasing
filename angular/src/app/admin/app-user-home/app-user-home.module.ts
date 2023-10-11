import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserHomeComponent } from './app-user-home.component';
import { ImpersonationService } from '../users/impersonation.service';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { OrganizationUnitsRoutingModule } from '../organization-units/organization-units-routing.module';
import { AdminSharedModule } from '../shared/admin-shared.module';
import { AppUserHomeRoutingModule } from './app-user-home-routing.module';

@NgModule({
  imports: [AppSharedModule, AdminSharedModule, CommonModule, AppUserHomeRoutingModule],
  declarations: [AppUserHomeComponent],
})
export class AppUserHomeModule { }
