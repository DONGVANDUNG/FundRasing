import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FundraisingDetailComponent } from './fundraising-detail.component';
import { FundraisingDetailRoutingModule } from './fundraising-detail-routing.module';
@NgModule({
    imports: [
        CommonModule, FundraisingDetailRoutingModule
    ],
    declarations: [FundraisingDetailComponent
    ],
})
export class FundraisingDetailModule { }
