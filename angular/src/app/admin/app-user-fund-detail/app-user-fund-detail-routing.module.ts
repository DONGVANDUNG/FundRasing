import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserFundDetailComponent } from './app-user-fund-detail.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserFundDetailComponent,
        pathMatch:'full'
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserFundDetailRoutingModule {}
