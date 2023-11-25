import { NgModule } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'fundRaiser',
                        loadChildren: () => import('./app-admin-fundRaiser/app-admin-fundRaiser.module').then((m) => m.AppAdminFundRaiserModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'accountGuest',
                        loadChildren: () => import('./app-admin-account-guest/app-admin-account-guest.module').then((m) => m.AppAdminFundRaiserModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'fundPackage',
                        loadChildren: () => import('./app-admin-fund-package/app-admin-fund-package.module').then((m) => m.AppAdminFundPackageModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'fundRaising',
                        loadChildren: () => import('./app-admin-fundraising/app-admin-fundraising.module').then((m) => m.AppAdminFundraisingModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'transaction',
                        loadChildren: () => import('./app-admin-fund-transaction/app-admin-fund-transaction.module').then((m) => m.AppAdminFundTransactionModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'donation/:fundId',
                        loadChildren: () => import('./app-user-donation/app-user-donation.module').then((m) => m.AppUserDonationModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'fund-detail',
                        loadChildren: () => import('./app-user-fund-detail/app-user-fund-detail.module').then((m) => m.AppUserFundDetailModule  ),
                        data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'checkout',
                        loadChildren: () => import('./app-user-checkout/app-user-checkout.module').then((m) => m.AppUserCheckoutModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    { //vãi lều sai chỗ nào chưa? path quá đảk, nok boy  gon nghẻ nhé =)))
                        path: 'users',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        data: { permission: 'Pages.Administration' },
                    },
                    {
                        path: 'roles',
                        loadChildren: () => import('./roles/roles.module').then((m) => m.RolesModule),
                        data: { permission: 'Pages.Administration' },
                    },
                    {
                        path: 'admin-post',
                        loadChildren: () => import('./app-admin-post/app-admin-post.module').then((m) => m.AppAdminPostModule),
                        //data: { permission: 'Pages.FundRaising' },
                    },
                    {
                        path: 'register-bank',
                        loadChildren: () => import('./app-user-bank/app-user-bank.module').then((m) => m.AppUserBankModule),
                        //data: { permission: 'Pages.FundRaising' },
                    },
                    {
                        path: 'register-fundraiser',
                        loadChildren: () => import('./app-user-fundraiser/app-user-fundraiser.module').then((m) => m.AppAdminRegisterFundraiserModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                    {
                        path: 'auction-admin',
                        loadChildren: () => import('./app-admin-auction/app-admin-auction.module').then((m) => m.AppAdminAuctionModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                    {
                        path: 'auction-user',
                        loadChildren: () => import('./app-user-aution/app-user-aution.module').then((m) => m.AppUserAutionModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                    {
                        path: 'auction-detail/:auctionId',
                        loadChildren: () => import('./app-user-detail-auction/app-user-detail-auction.module').then((m) => m.AppUserDetailAuctionModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                    {
                        path: 'user-post',
                        loadChildren: () => import('./app-user-post/app-user-post.module').then((m) => m.AppUserPostModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                    {
                        path: 'user-post-detail/:postId',
                        loadChildren: () => import('./app-user-post-detail/app-user-post-detail.module').then((m) => m.AppUserPostDetailModule),
                        //data: { permission: 'Pages.UserDonate.FundRaising' },
                    },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AdminRoutingModule {
    constructor(private router: Router) {
        router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }
}
