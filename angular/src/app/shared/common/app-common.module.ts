import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppLocalizationService } from '@app/shared/common/localization/app-localization.service';
import { AppNavigationService } from '@app/shared/layout/nav/app-navigation.service';
import { prodCommonModule } from '@shared/common/common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AgGridModule } from 'ag-grid-angular';
import {
    BsDatepickerModule,
    BsDatepickerConfig,
    BsDaterangepickerConfig,
    BsLocaleService,
} from 'ngx-bootstrap/datepicker';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';
import { CommonLookupModalComponent } from './lookup/common-lookup-modal.component';
import { EntityTypeHistoryModalComponent } from './entityHistory/entity-type-history-modal.component';
import { EntityChangeDetailModalComponent } from './entityHistory/entity-change-detail-modal.component';
import { DateRangePickerInitialValueSetterDirective } from './timing/date-range-picker-initial-value.directive';
import { DatePickerInitialValueSetterDirective } from './timing/date-picker-initial-value.directive';
import { DateTimeService } from './timing/date-time.service';
import { TimeZoneComboComponent } from './timing/timezone-combo.component';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { CountoModule } from 'angular2-counto';
import { AppBsModalModule } from '@shared/common/appBsModal/app-bs-modal.module';
import { SingleLineStringInputTypeComponent } from './input-types/single-line-string-input-type/single-line-string-input-type.component';
import { ComboboxInputTypeComponent } from './input-types/combobox-input-type/combobox-input-type.component';
import { CheckboxInputTypeComponent } from './input-types/checkbox-input-type/checkbox-input-type.component';
import { MultipleSelectComboboxInputTypeComponent } from './input-types/multiple-select-combobox-input-type/multiple-select-combobox-input-type.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PasswordInputWithShowButtonComponent } from './password-input-with-show-button/password-input-with-show-button.component';
import { KeyValueListManagerComponent } from './key-value-list-manager/key-value-list-manager.component';




import { DashboardViewConfigurationService } from './customizable-dashboard/dashboard-view-configuration.service';
import { DataFormatService } from './services/data-format.service';

import { TmssSelectGridModalComponent } from './grid/tmss-select-grid-modal/tmss-select-grid-modal.component';
import { TmssMultiColumnDropdownComponent } from './grid/tmss-multi-column-dropdown/tmss-multi-column-dropdown.component';
import { TmssDatepickerComponent } from './input-types/tmss-datepicker/tmss-datepicker.component';
import { TmssComboboxComponent } from './input-types/tmss-combobox/tmss-combobox.component';
import { TmssCheckboxComponent } from './input-types/tmss-checkbox/tmss-checkbox.component';
import { TmssTextareaComponent } from './input-types/tmss-textarea/tmss-textarea.component';
import { TmssTextInputComponent } from './input-types/tmss-text-input/tmss-text-input.component';
import { TmssSearchInputComponent } from './input-types/tmss-search-input/tmss-search-input.component';

import { AgDatepickerRendererComponent } from './grid/ag-datepicker-renderer/ag-datepicker-renderer.component';
import { SimpleAgGridComponent } from './ag-grid-custom/simple-ag-grid/simple-ag-grid.component';
import { AgFloatingFilterGridComponent } from './grid/ag-floating-filter-grid/ag-floating-filter-grid.component';
import { AgFilterGridComponent } from './grid/ag-filter-grid/ag-filter-grid.component';
import { NewCheckboxComponent } from './input-types/new-checkbox/new-checkbox.component';
import { AgCellButtonTransparentRendererComponent } from './grid/ag-cell-button-transparent-renderer/ag-cell-button-transparent-renderer.component';

import { GridTableComponent } from './ag-grid-custom/grid-table/grid-table.component';
import { GridPaginationComponent } from './ag-grid-custom/grid-pagination/grid-pagination.component';
import { TmssTooltipComponent } from './tmss-tooltip/tmss-tooltip.component';
import { AgCheckboxRendererComponent } from '@app/shared/common/grid/ag-checkbox-renderer/ag-checkbox-renderer.component';
import { AgDropdownRendererComponent } from './grid/ag-dropdown-renderer/ag-dropdown-renderer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        UtilsModule,
        prodCommonModule,
        TableModule,
        PaginatorModule,
        TabsModule.forRoot(),
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        PerfectScrollbarModule,
        CountoModule,
        AppBsModalModule,
        AutoCompleteModule,
        AgGridModule,
        BrowserAnimationsModule
    ],
    declarations: [
        TimeZoneComboComponent,
        CommonLookupModalComponent,
        EntityTypeHistoryModalComponent,
        EntityChangeDetailModalComponent,
        DateRangePickerInitialValueSetterDirective,
        DatePickerInitialValueSetterDirective,
        SingleLineStringInputTypeComponent,
        ComboboxInputTypeComponent,
        CheckboxInputTypeComponent,
        MultipleSelectComboboxInputTypeComponent,
        PasswordInputWithShowButtonComponent,
        KeyValueListManagerComponent,
        SimpleAgGridComponent,
        TmssComboboxComponent,
        TmssCheckboxComponent,
        NewCheckboxComponent,
        TmssTextareaComponent,
        TmssTextInputComponent,
        TmssSearchInputComponent,
        TmssSelectGridModalComponent,
        TmssMultiColumnDropdownComponent,
        TmssTooltipComponent,
        TmssSelectGridModalComponent,
        TmssMultiColumnDropdownComponent,
        TmssDatepickerComponent,
        TmssComboboxComponent,
        TmssCheckboxComponent,
        TmssTextareaComponent,
        TmssTextInputComponent,
        TmssSearchInputComponent,
        AgDatepickerRendererComponent,
        AgFloatingFilterGridComponent,
        AgFilterGridComponent,
        NewCheckboxComponent,
        AgCellButtonTransparentRendererComponent,
        GridTableComponent,
        GridPaginationComponent,
        TmssTooltipComponent,
        AgCheckboxRendererComponent,
        AgDropdownRendererComponent,

    ],
    exports: [
        TimeZoneComboComponent,
        CommonLookupModalComponent,
        EntityTypeHistoryModalComponent,
        EntityChangeDetailModalComponent,
        DateRangePickerInitialValueSetterDirective,
        DatePickerInitialValueSetterDirective,
        PasswordInputWithShowButtonComponent,
        KeyValueListManagerComponent,
        AgGridModule,

        SimpleAgGridComponent,
        TmssComboboxComponent,
        TmssCheckboxComponent,
        NewCheckboxComponent,
        TmssTextareaComponent,
        TmssTextInputComponent,
        TmssSearchInputComponent,
        TmssSelectGridModalComponent,
        TmssMultiColumnDropdownComponent,
        TmssTooltipComponent,
        TmssSelectGridModalComponent,
        TmssMultiColumnDropdownComponent,
        TmssDatepickerComponent,
        TmssComboboxComponent,
        TmssCheckboxComponent,
        TmssTextareaComponent,
        TmssTextInputComponent,
        TmssSearchInputComponent,
        AgDatepickerRendererComponent,
        AgFloatingFilterGridComponent,
        AgFilterGridComponent,
        NewCheckboxComponent,
        AgCellButtonTransparentRendererComponent,
        GridTableComponent,
        GridPaginationComponent,
        TmssTooltipComponent,
        AgCheckboxRendererComponent,
        AgDropdownRendererComponent,
    ],
    providers: [
        DateTimeService,
        AppLocalizationService,
        AppNavigationService,
        DashboardViewConfigurationService,
        DataFormatService,

        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
    ]
})
export class AppCommonModule {
    static forRoot(): ModuleWithProviders<AppCommonModule> {
        return {
            ngModule: AppCommonModule,
            providers: [AppAuthService, AppRouteGuard],
        };
    }
}
