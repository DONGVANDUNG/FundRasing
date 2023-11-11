import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonationFundComponent } from './donation-fund.component';

const routes: Routes = [
    {
        path: '',
        component: DonationFundComponent,
        pathMatch:'full'

    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DonationFundRoutingModule {}
