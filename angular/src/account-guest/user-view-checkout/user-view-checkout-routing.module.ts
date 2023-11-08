import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserViewCheckoutComponent } from './user-view-checkout.component';

const routes: Routes = [
    {
        path: '',
        component: UserViewCheckoutComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserViewCheckoutRoutingModule {}
