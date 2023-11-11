import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppAdminPostComponent } from './app-admin-post.component';

const routes: Routes = [
    {
        path: '',
        component: AppAdminPostComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppAdminPostRoutingModule {}
