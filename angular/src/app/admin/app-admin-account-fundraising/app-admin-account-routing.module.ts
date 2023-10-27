import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminAccountFundraisingComponent } from './app-admin-account-fundraising.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminAccountFundraisingComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminAccountFundraisingRouteModule { }
