import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserDetailAuctionComponent } from './app-user-detail-auction.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserDetailAuctionComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserDetailAuctionRoutingModule {}
