import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ContactUsRoutingModule } from './contact-us-routing.module';
import { ContactUsComponent } from './contact-us.component';
import { LoadingComponent } from 'account-guest/loading';
@NgModule({
    imports: [
        CommonModule, ContactUsRoutingModule
    ],
    declarations: [ContactUsComponent
    ],
})
export class ContactUsHomeModule { }
