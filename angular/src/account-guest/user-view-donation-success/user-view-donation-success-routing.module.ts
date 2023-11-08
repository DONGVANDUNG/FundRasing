import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewDonationSuccessComponent } from './user-view-donation-success.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewDonationSuccessComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewDonationSuccessRoutingModule {}
