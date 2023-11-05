import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewListFundComponent } from 'account-guest/user-view-list-fund/user-view-list-fund.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewListFundComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewListFundRoutingModule {}
