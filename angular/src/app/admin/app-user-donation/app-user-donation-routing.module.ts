import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserDonationComponent } from './app-user-donation.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserDonationComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserDonationRoutingModule {}
