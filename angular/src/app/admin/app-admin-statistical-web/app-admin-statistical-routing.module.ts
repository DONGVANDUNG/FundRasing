import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppAdminStatisticalWebComponent } from './app-admin-statistical-web.component';
// import { AppAdminStatisticalWebComponent } from './app-admin-statistical-web.component';

const routes: Routes = [
    {
        path: '',
        component: AppAdminStatisticalWebComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppAdminStatisticalRoutingModule {}
