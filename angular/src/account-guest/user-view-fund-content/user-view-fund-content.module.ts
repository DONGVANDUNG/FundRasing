import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserViewFundContentComponent } from './user-view-fund-content.component';
import { UserViewFundContentRoutingModule } from './user-view-fund-content-routing.module';
import { TabViewModule } from 'primeng/tabview';

@NgModule({
    imports: [
        CommonModule, UserViewFundContentRoutingModule,TabViewModule
    ],
    declarations: [UserViewFundContentComponent
    ],
})
export class UserViewFundContentModule { }
