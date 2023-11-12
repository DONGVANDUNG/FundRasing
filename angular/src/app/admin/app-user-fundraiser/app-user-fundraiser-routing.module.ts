import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserFundraiserComponent } from './app-user-fundraiser.component';

const routes: Routes = [
    {
        path: '',
        component: AppUserFundraiserComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppUserFundRaiserRoutingModule {}
