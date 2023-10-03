import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import { LookupRoutingModule } from './lookup-routing.module';
import { LookupComponent } from './lookup.component';
import { CreateOrEditLookupModalComponent } from './create-or-edit-lookup-modal.component';

@NgModule({
    declarations: [
       LookupComponent, 
        CreateOrEditLookupModalComponent
      
    ],
    imports: [
        AppSharedModule, LookupRoutingModule]
})
export class LookupModule {}
