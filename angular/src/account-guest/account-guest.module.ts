import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountGuestComponent } from './account-guest.component';
import { AccountGuestRoutingModule } from './account-guest-routing.module';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FundPackageComponent } from './fund-package/fund-package.component';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites/fundraising-live-sites.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { UserViewHomeComponent } from './user-view-home/user-view-home.component';
import { LoadingComponent } from './loading';
import { UserViewFundDetailComponent } from './user-view-donation-success/user-view-donation-success.component';
import { UserViewRegisterComponent } from './user-view-register/user-view-register.component';
import { UserViewSiginComponent } from './user-view-sigin/user-view-sigin.component';
import { UserViewDonationModule } from './user-view-donation/user-view-donation.module';
import { UserViewHomeModule } from './user-view-home/user-view-home.module';
import { AppSharedModule } from '@app/shared/app-shared.module';

@NgModule({
    imports: [
        CommonModule, AccountGuestRoutingModule, TabsModule.forRoot(), UserViewDonationModule, UserViewHomeModule,
        AppSharedModule
    ],
    declarations: [AccountGuestComponent
        // ContactUsComponent,
        // FundPackageComponent, FundraisingLiveSitesComponent,
        // UserViewHomeComponent,LoadingComponent,
        // UserViewFundDetailComponent,UserViewRegisterComponent,UserViewSiginComponent
    ]
})
export class AccountGuestModule { }
