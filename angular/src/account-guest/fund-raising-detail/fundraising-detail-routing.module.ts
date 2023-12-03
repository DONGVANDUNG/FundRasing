import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FundraisingDetailComponent } from './fundraising-detail.component';

const routes: Routes = [
    {
        path: '',
        component: FundraisingDetailComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class FundraisingDetailRoutingModule {}
