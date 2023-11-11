import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserDonationComponent } from './app-user-donation.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserDonationComponent,
       //pathMatch:'full'
        children:[
            //{ path: '**', redirectTo: 'view-fund' },
            {
                path:'donate/:id',
                loadChildren: () => import('./donation-fund/donation-fund.module').then((m) => m.DonationFundModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path:'view-fund',
                loadChildren: () => import('./view-all-fund/view-all-fund.module').then((m) => m.ViewAllFundModule), //Lazy load account module
                data: { preload: true },
            },
            {
                path:'donation-success/:fundId',
                loadChildren: () => import('./donation-success/donation-success.module').then((m) => m.DonationSuccessModule), //Lazy load account module
                data: { preload: true },
            },
            { path: '', redirectTo: 'view-fund',pathMatch:'full'},
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserDonationRoutingModule {}
