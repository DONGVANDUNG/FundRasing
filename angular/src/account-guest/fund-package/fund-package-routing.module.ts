import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FundPackageComponent } from './fund-package.component';

const routes: Routes = [
    {
        path: '',
        component: FundPackageComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class FundPackageRoutingModule {}
