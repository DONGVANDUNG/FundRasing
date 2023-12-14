import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '@app/admin/users/users.component';
import { AccountGuestComponent } from './account-guest.component';

const routes: Routes = [
    {
        path: '',
        component: AccountGuestComponent,
        // pathMatch: 'full',
        children: [
            { path: '', redirectTo: 'home' , pathMatch: 'full' },
            {
                path: 'home',
                loadChildren: () => import('./user-view-home/user-view-home.module').then((m) => m.UserViewHomeModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'fund-package',
                loadChildren: () => import('./fund-package/fund-package.module').then((m) => m.FundPackageModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'about-us',
                loadChildren: () => import('./contact-us/contact-us.module').then((m) => m.ContactUsHomeModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'project',
                loadChildren: () => import('./user-view-fund-content/user-view-fund-content.module').then((m) => m.UserViewFundContentModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path: 'user-fund-detail/:postId/:fundId',
                loadChildren: () => import('./user-view-fund-detail/user-view-fund-detail.module').then((m) => m.UserViewFundDetailModule), //Lazy load account module
                data: { preload: true },
            },
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
    //  { path: '**', redirectTo: 'guest', pathMatch: 'full' },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AccountGuestRoutingModule { }
