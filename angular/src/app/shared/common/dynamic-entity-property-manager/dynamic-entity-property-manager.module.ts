import { NgModule } from '@angular/core';
import { DynamicEntityPropertyValueModule } from '@app/admin/dynamic-properties/dynamic-entity-properties/value/dynamic-entity-property-value.module';
import { DynamicEntityPropertyManagerComponent } from './dynamic-entity-property-manager.component';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';

@NgModule({
    declarations: [
        DynamicEntityPropertyManagerComponent
    ],
    imports: [DynamicEntityPropertyValueModule,AppSharedModule, AdminSharedModule],
    exports: [DynamicEntityPropertyValueModule, 
        DynamicEntityPropertyManagerComponent
    ],
})
export class DynamicEntityPropertyManagerModule {}
