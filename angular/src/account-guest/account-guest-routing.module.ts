import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '@app/admin/users/users.component';
import { AccountGuestComponent } from './account-guest.component';
import { UserFundDetailComponent } from './user-fund-detail/user-fund-detail.component';

const routes: Routes = [
    {
        path: 'guest',
        component: AccountGuestComponent,
        pathMatch: 'full',
    },
    {
        path: 'fund',
        component: UserFundDetailComponent,
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AccountGuestRoutingModule {}
