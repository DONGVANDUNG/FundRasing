import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminFundTransactionComponent } from './app-admin-fund-transaction.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminFundTransactionComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminFundTransactionRouteModule { }
