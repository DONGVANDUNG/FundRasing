import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewHomeComponent } from './user-view-home.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewHomeComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewHomeRoutingModule {}
