import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FundPackageRoutingModule } from './fund-package-routing.module';
import { FundPackageComponent } from './fund-package.component';
@NgModule({
    imports: [
        CommonModule, FundPackageRoutingModule
    ],
    declarations: [FundPackageComponent],
})
export class FundPackageModule { }
