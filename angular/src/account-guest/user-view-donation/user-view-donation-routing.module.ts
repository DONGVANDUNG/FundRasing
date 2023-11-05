import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewDonationComponent } from './user-view-donation.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewDonationComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewDonationRoutingModule {}
