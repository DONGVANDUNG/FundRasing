import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewSiginComponent } from './user-view-signin.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewSiginComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewSigninRoutingModule {}
