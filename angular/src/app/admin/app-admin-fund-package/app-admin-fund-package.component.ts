import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { PaginationParamsModel, GridParams } from '@app/shared/common/models/base.model';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { ceil } from 'lodash-es';
import * as moment from 'moment';
import { CreateOrEditFundPackageComponent } from './create-or-edit-fund-package/create-or-edit-fund-package.component';
import { DateTime } from 'luxon';

@Component({
    selector: 'app-app-admin-fund-package',
    templateUrl: './app-admin-fund-package.component.html',
    styleUrls: ['./app-admin-fund-package.component.less']
})
export class AppAdminFundPackageComponent extends AppComponentBase implements OnInit {
    @ViewChild("createOrEdit") modalCreate: CreateOrEditFundPackageComponent;
    columnDefs;
    dateOfReport;
    rowData: any = [];
    defaultColDef;
    filter: string = '';
    maxResultCount: number = 20;
    skipCount: number = 0;
    sorting: string = '';
    paginationParams: PaginationParamsModel;
    params: GridParams;
    advancedFiltersAreShown: boolean;
    createdDate = new Date();
    typePackage;
    listTypePackage = [
        { label: 'Tuần', value: 'Tuần' },
        { label: 'Tháng', value: 'Tháng' },
        { label: 'Năm', value: 'Năm' },

    ]
    selectedPackage;
    sideBar = {
        toolPanels: [
            {
                id: 'columns',
                labelDefault: this.l('Columns'),
                labelKey: 'columns',
                iconKey: 'columns',
                toolPanel: 'agColumnsToolPanel',
                toolPanelParams: {
                    suppressRowGroups: true,
                    suppressValues: true,
                    suppressPivots: true,
                    suppressPivotMode: true,
                    suppressColumnFilter: false
                },
            },
        ],
        defaultToolPanel: '',
    };
    constructor(
        injector: Injector,
        private _adminServiceProxy: AdminFundRaisingServiceProxy,
        private dataFormatService: DataFormatService
    ) {
        super(injector);
        this.columnDefs = [
            {
                headerName: this.l('STT'),
                headerTooltip: this.l('STT'),
                cellRenderer: params => params.rowIndex + (this.paginationParams.pageNum! - 1) * this.paginationParams.pageSize! + 1,
                field: 'no',
                cellClass: ['text-left'],
                minWidth: 100,
            },
            {
                headerName: this.l('Loại quỹ'),
                headerTooltip: this.l('Loại quỹ'),
                field: 'duration',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Phí tham gia'),
                headerTooltip: this.l('Phí tham gia'),
                field: 'paymentFee',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.paymentFee) + " VND",
                flex: 3,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Chiết khấu'),
                headerTooltip: this.l('Chiết khấu'),
                field: 'commission',
                flex: 3,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tạo gói'),
                headerTooltip: this.l('Ngày tạo gói'),
                field: 'createdTime',
                flex: 4,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Mô tả'),
                headerTooltip: this.l('Mô tả'),
                field: 'description',
                flex: 4,
                cellClass: ['text-left'],
            },
        ];
        this.defaultColDef = {
            flex: 1,
            floatingFilter: false,
            filter: 'agTextColumnFilter',
            resizable: true,
            sortable: true,
            floatingFilterComponentParams: { suppressFilterButton: true },
            textFormatter: function (r) {
                if (r == null) return null;
                return r.toLowerCase();
            },
        };
    }

    callBackGrid(params) {
        this.params = params;
        this.params.api.paginationSetPageSize(this.paginationParams.pageSize);
    }
    ngOnInit() {
        this.paginationParams = { pageNum: 1, pageSize: 20, totalCount: 0 };
        this.onGridReady(this.paginationParams);
    }

    onGridReady(paginationParams) {
        this.rowData = [];
        this.paginationParams = paginationParams;
        this.paginationParams.skipCount = (paginationParams.pageNum - 1) * paginationParams.pageSize;
        this.maxResultCount = paginationParams.pageSize;
        this.getAll(this.paginationParams).subscribe((result) => {
            this.rowData = result.items;
            this.paginationParams.totalPage = ceil(result.totalCount / this.maxResultCount);
            this.paginationParams.totalCount = result.totalCount;
            this.params.api.setRowData(this.rowData);
        });
    }
    eventEnter(event) {
        if (event.keyCode === 13) {
            this.search();
        }
    }

    onChangeFilterShown() {
        this.advancedFiltersAreShown = !this.advancedFiltersAreShown
    }

    search() {
        this.onGridReady(this.paginationParams);
    }
    clearValueFilter() {
        this.onGridReady(this.paginationParams);
        this.createdDate = new Date();
        this.typePackage = null;
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this._adminServiceProxy.getListFundPackage(
            DateTime.fromJSDate(this.createdDate),
            this.typePackage,
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedPackage = selected.id;
        }
    }
    createFundPackage() {
        this.modalCreate.show();
    }
    editFundPackage(){
        if(!this.selectedPackage){
            this.notify.warn("Vui lòng chọn một bản ghi")
        }
        this.modalCreate.show(this.selectedPackage);
    }
}
