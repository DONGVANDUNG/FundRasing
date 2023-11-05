import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '@app/admin/users/users.component';
import { AccountGuestComponent } from './account-guest.component';
import { UserViewListFundComponent } from './user-view-list-fund/user-view-list-fund.component';
import { UserViewRegisterComponent } from './user-view-register/user-view-register.component';
import { UserViewSiginComponent } from './user-view-sigin/user-view-sigin.component';
import { UserViewCheckoutComponent } from './user-view-checkout/user-view-checkout.component';
import { AccountRouteGuard } from '@account/auth/account-route-guard';

const routes: Routes = [
    {
        path: '',
        component: AccountGuestComponent,
        pathMatch: 'full',
        children: [
            {
                path: 'donation',
                loadChildren: () => import('./user-view-donation/user-view-donation.module').then((m) => m.UserViewDonationModule), //Lazy load account module
                data: { preload: true },
            },
            // {
            //     path: 'register',
            //     component: UserViewRegisterComponent,
            //     pathMatch: 'full',
            // },
            // {
            //     path: 'sigin',
            //     component: UserViewSiginComponent,
            //     pathMatch: 'full',
            // },
            // {
            //     path: 'checkout',
            //     component: UserViewCheckoutComponent,
            //     pathMatch: 'full',
            // },
        ]
    },
    // {
    //     path: 'account',
    //     loadChildren: () => import('account/account.module').then((m) => m.AccountModule), //Lazy load account module
    //     data: { preload: true },
    // },
    // { path: '', redirectTo: 'app/notification' , pathMatch: 'full' },
    // { path: '**', redirectTo: 'guest', pathMatch: 'full' },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AccountGuestRoutingModule { }
