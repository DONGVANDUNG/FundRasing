import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserHistoryAuctionComponent } from './app-user-history-auction.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserHistoryAuctionComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserHistoryAuctionRoutingModule {}
