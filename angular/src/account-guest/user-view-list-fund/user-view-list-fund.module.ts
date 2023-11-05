import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewListFundComponent } from './user-view-list-fund.component';
import { UserViewHomeRoutingModule } from 'account-guest/user-view-home/user-view-home-routing.module';
@NgModule({
    imports: [
        CommonModule, UserViewHomeRoutingModule
    ],
    declarations: [UserViewListFundComponent
    ],
})
export class UserViewListFundModule { }
