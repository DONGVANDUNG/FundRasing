import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserAutionComponent } from './app-user-aution.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserAutionComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserBankRoutingModule {}
