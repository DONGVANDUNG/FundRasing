import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from 'account-guest/loading';
import { UserViewRegisterRoutingModule } from './user-view-register-routing.module';
import { UserViewRegisterComponent } from './user-view-register.component';
@NgModule({
    imports: [
        CommonModule, UserViewRegisterRoutingModule
    ],
    declarations: [UserViewRegisterComponent],
})
export class UserViewRegisterModule { }
