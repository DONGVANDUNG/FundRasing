import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppAdminRequestToFundraiserComponent } from './app-admin-request-to-fundraiser.component';

const routes: Routes = [
    {
        path: '',
        component: AppAdminRequestToFundraiserComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppAdminRequestToFundRaiserRoutingModule {}
