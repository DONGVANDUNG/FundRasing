import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminAccountGuestComponent } from './app-admin-account-guest.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminAccountGuestComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminAccountGuestRouteModule { }
