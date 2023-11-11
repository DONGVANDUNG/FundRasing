import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonationSuccessComponent } from './donation-success.component';

const routes: Routes = [
    {
        path: '',
        component: DonationSuccessComponent,
        pathMatch:'full'

    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DonationSuccessRoutingModule {}
