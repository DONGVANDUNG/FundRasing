import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserDonationComponent } from './app-user-donation.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserDonationComponent,
        pathMatch:'full'
        // children:[
        //     {
        //         path:'success',
        //         loadChildren: () => import('../user-view-donation-success/user-view-donation-success.module').then((m) => m.AppUserDonationSuccessModule), //Lazy load account module
        //         data: { preload: true },
        //     }
        // ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserDonationRoutingModule {}
