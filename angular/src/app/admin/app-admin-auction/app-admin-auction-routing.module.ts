import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppAdminAuctionComponent } from './app-admin-auction.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppAdminAuctionComponent
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppAdminFundPackageRouteModule { }
