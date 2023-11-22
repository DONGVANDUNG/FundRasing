import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewHomeComponent } from './user-view-home.component';
import { UserViewHomeRoutingModule } from './user-view-home-routing.module';
@NgModule({
    imports: [
        CommonModule, UserViewHomeRoutingModule
    ],
    declarations: [UserViewHomeComponent
    ],
    exports: [UserViewHomeComponent]
})
export class UserViewHomeModule { }
