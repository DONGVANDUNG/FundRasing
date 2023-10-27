import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminFundPackageComponent } from './app-admin-fund-package.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminFundPackageComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminFundPackageRouteModule { }
