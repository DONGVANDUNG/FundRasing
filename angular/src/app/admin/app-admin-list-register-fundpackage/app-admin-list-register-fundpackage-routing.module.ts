import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminListRegisterFundpackageComponent } from './app-admin-list-register-fundpackage.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminListRegisterFundpackageComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminListRegisterFundPackageRoutingModule { }
