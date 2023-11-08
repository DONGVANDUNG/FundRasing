import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserCheckoutComponent } from './app-user-checkout.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserCheckoutComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserCheckoutRoutingModule {}
