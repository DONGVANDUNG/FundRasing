import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewHomeComponent } from './user-view-home.component';
import { UserViewDonationComponent } from 'account-guest/user-view-donation/user-view-donation.component';
import { UserViewRegisterComponent } from 'account-guest/user-view-register/user-view-register.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewHomeComponent,
        // pathMatch: 'full',
        children: [
            {
                path: 'donation',
                loadChildren: () => import('../user-view-donation/user-view-donation.module').then((m) => m.UserViewDonationModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'donation-success',
                loadChildren: () => import('../user-view-donation-success/user-view-donation-success.module').then((m) => m.UserViewDonationSuccessModule), //Lazy load account module
                data: { preload: true },
            },
            // { path: '**', redirectTo: 'home',pathMatch:'full'},
            {
                path: 'register',
                loadChildren: () => import('../user-view-register/user-view-register.module').then((m) => m.UserViewRegisterModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'sign-in',
                loadChildren: () => import('../user-view-sigin/user-view-signin.module').then((m) => m.UserViewSigninModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'checkout',
                loadChildren: () => import('../user-view-checkout/user-view-checkout.module').then((m) => m.UserViewCheckoutModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'fund-detail',
                loadChildren: () => import('../user-view-fund-detail/user-view-fund-detail.module').then((m) => m.UserViewFundDetailModule), //Lazy load account module
                data: { preload: true },
            },
         ]
    },
    { path: '', redirectTo: 'home' , pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewHomeRoutingModule {}
