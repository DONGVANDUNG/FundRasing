import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ViewAllFundRoutingModule } from './view-all-fund-routing.module';
import { ViewAllFundComponent } from './view-all-fund.component';
@NgModule({
  imports: [
    CommonModule,ViewAllFundRoutingModule
  ],
  declarations: [ViewAllFundComponent
    ]
})
export class ViewAllFundModule { }
