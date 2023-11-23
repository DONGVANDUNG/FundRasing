import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserBankComponent } from './app-user-bank.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserBankComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserBankRoutingModule {}
