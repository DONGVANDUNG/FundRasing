import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserPostComponent } from './app-user-post.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserPostComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserPostRoutingModule {}
