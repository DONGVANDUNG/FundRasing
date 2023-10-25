import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminFundRaiserComponent } from './app-admin-fundRaiser.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminFundRaiserComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminFundRaiserRoutingModule { }
