import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserPostDetailComponent } from './app-user-post-detail.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserPostDetailComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserPostDetailRoutingModule {}
