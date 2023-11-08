import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewRegisterComponent } from './user-view-register.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewRegisterComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewRegisterRoutingModule {}
