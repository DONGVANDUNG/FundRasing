import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserDonationSuccessComponent } from './app-user-donation-success.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserDonationSuccessComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserDonationSuccessRoutingModule {}
