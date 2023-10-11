import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '@app/admin/users/users.component';
import { AppUserHomeComponent } from './app-user-home.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserHomeComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserHomeRoutingModule {}
