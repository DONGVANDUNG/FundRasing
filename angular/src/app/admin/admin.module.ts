import { NgModule } from '@angular/core';
import { AdminRoutingModule } from './admin-routing.module';
import { TreeDragDropService } from 'primeng/api';
import {
    BsDatepickerConfig,
    BsDatepickerModule,
    BsDaterangepickerConfig,
    BsLocaleService,
} from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from '../../assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { SubheaderModule } from '@app/shared/common/sub-header/subheader.module';
import { UsersModule } from './users/users.module';
import { AppAdminFundRaiserModule } from './app-admin-fundRaiser/app-admin-fundRaiser.module';
import { AppUserCheckoutModule } from './app-user-checkout/app-user-checkout.module';
import { AppUserDonationSuccessModule } from './app-user-donation-success/app-user-donation-success.module';
import { AppUserFundDetailModule } from './app-user-fund-detail/app-user-fund-detail.module';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    // suppressScrollX: true
};

@NgModule({
    imports: [
        AdminRoutingModule,
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        SubheaderModule,
        AppAdminFundRaiserModule,
        AppUserFundDetailModule,
        AppUserDonationSuccessModule,
        AppUserCheckoutModule
    ],
    declarations: [],
    exports: [],
    providers: [
        TreeDragDropService,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
        { provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG },
    ],
})
export class AdminModule {}
