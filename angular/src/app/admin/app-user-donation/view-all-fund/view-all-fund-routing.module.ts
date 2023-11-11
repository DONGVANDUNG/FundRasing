import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewAllFundComponent } from './view-all-fund.component';

const routes: Routes = [
    {
        path: '',
        component: ViewAllFundComponent,
        pathMatch:'full'

    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ViewAllFundRoutingModule {}
