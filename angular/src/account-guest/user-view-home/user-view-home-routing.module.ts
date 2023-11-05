import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewHomeComponent } from './user-view-home.component';
import { UserViewDonationComponent } from 'account-guest/user-view-donation/user-view-donation.component';

const routes: Routes = [
    {
        path: 'home',
        component: UserViewHomeComponent,
        // pathMatch: 'full',
    },
    {
        path: 'donation',
        component: UserViewDonationComponent,
        pathMatch: 'full',
    },
    // {
    //     path: '/register',
    //     component: UserViewRegisterComponent,
    //     pathMatch: 'full',
    // },
    // {
    //     path: '/sigin',
    //     component: UserViewSiginComponent,
    //     pathMatch: 'full',
    // },
    // {
    //     path: '/checkout',
    //     component: UserViewListFundComponent,
    //     pathMatch: 'full',
    // },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewHomeRoutingModule {}
