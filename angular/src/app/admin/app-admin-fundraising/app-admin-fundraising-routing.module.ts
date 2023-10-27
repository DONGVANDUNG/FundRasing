import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminFundraisingComponent } from './app-admin-fundraising.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminFundraisingComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminFundRaisingRoutingModule { }
