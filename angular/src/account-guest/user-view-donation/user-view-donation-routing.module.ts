import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewDonationComponent } from './user-view-donation.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewDonationComponent,
        pathMatch:'full'
        // children:[
        //     {
        //         path:'success',
        //         loadChildren: () => import('../user-view-donation-success/user-view-donation-success.module').then((m) => m.UserViewDonationSuccessModule), //Lazy load account module
        //         data: { preload: true },
        //     }
        // ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewDonationRoutingModule {}
