import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewSigninRoutingModule } from './user-view-signin-routing.module';
import { UserViewSiginComponent } from './user-view-signin.component';

@NgModule({
    imports: [
        CommonModule,UserViewSigninRoutingModule
    ],
    declarations: [UserViewSiginComponent
    ]
})
export class UserViewSigninModule { }
