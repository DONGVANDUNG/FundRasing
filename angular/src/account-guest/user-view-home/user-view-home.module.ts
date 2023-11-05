import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewHomeComponent } from './user-view-home.component';
import { UserViewHomeRoutingModule } from './user-view-home-routing.module';
import { LoadingComponent } from 'account-guest/loading';
@NgModule({
    imports: [
        CommonModule, UserViewHomeRoutingModule
    ],
    declarations: [UserViewHomeComponent, LoadingComponent
    ],
    exports: [UserViewHomeComponent]
})
export class UserViewHomeModule { }
