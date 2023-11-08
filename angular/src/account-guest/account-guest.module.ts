import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AccountGuestComponent } from './account-guest.component';
import { UserViewHomeModule } from './user-view-home/user-view-home.module';
import { AccountGuestRoutingModule } from './account-guest-routing.module';

@NgModule({
    imports: [
        CommonModule,
        AppSharedModule,
        AccountGuestRoutingModule
       // UserViewHomeModule
    ],
    declarations: [AccountGuestComponent
        // ContactUsComponent,
        // FundPackageComponent, FundraisingLiveSitesComponent,
        // UserViewHomeComponent,LoadingComponent,
        // UserViewFundDetailComponent,UserViewRegisterComponent,UserViewSiginComponent
    ]
})
export class AccountGuestModule { }
