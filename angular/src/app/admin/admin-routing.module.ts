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
                        path: 'donation',
                        loadChildren: () => import('./app-user-donation/app-user-donation.module').then((m) => m.AppUserDonationModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'donate-fund',
                        loadChildren: () => import('./app-user-fund-detail/app-user-fund-detail.module').then((m) => m.AppUserFundDetailModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'donation-success',
                        loadChildren: () => import('./app-user-donation-success/app-user-donation-success.module').then((m) => m.AppUserDonationSuccessModule),
                        // data: { permission: 'Pages.Administration.Users' },
                    },
                    {
                        path: 'checkout',
                        loadChildren: () => import('./app-user-checkout/app-user-checkout.module').then((m) => m.AppUserCheckoutModule),
                        // data: { permission: 'Pages.Administration.Users' },
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
