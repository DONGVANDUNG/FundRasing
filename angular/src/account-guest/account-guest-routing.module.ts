import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '@app/admin/users/users.component';
import { AccountGuestComponent } from './account-guest.component';

const routes: Routes = [
    {
        path: 'guest',
        component: AccountGuestComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AccountGuestRoutingModule {}
