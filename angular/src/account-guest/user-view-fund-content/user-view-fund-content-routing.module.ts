import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewFundContentComponent } from 'account-guest/user-view-fund-content/user-view-fund-content.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewFundContentComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewFundContentRoutingModule {}
