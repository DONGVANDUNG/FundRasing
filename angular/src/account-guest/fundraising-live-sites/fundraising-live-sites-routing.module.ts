import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites.component';

const routes: Routes = [
    {
        path: '',
        component: FundraisingLiveSitesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class FundraisingLiveSitesRoutingModule {}
