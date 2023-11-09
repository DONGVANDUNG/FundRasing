import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FundraisingLiveSitesRoutingModule } from './fundraising-live-sites-routing.module';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites.component';
import { LoadingComponent } from 'account-guest/loading';
@NgModule({
    imports: [
        CommonModule, FundraisingLiveSitesRoutingModule
    ],
    declarations: [FundraisingLiveSitesComponent
    ],
})
export class FundraisingLiveSitesModule { }
