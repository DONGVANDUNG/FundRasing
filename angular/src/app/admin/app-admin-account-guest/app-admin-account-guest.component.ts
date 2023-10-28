import { Component, Injector, OnInit } from '@angular/core';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { ceil } from 'lodash-es';

@Component({
    selector: 'app-app-admin-account-guest',
    templateUrl: './app-admin-account-guest.component.html',
    styleUrls: ['./app-admin-account-guest.component.less']
})
export class AppAdminAccountGuestComponent extends AppComponentBase implements OnInit {

    columnDefs;
    typeOfReport = 0;
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
    createFillter;
    statusFillter;
    // fromDate = moment().add(-30,'day');
    // toDate = moment();
    selectedAccountGuest;
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
        private fundRaising: AdminFundRaisingServiceProxy,
        private dataFormatService: DataFormatService
    ) {
        super(injector);
        this.columnDefs = [
            {
                headerName: this.l('STT'),
                headerTooltip: this.l('STT'),
                cellRenderer: params => params.rowIndex + (this.paginationParams.pageNum! - 1) * this.paginationParams.pageSize! + 1,
                field: 'no',
                cellClass: ['text-center'],
                minWidth: 100,
            },
            {
                headerName: this.l('Tên đăng nhập'),
                headerTooltip: this.l('Tên đăng nhập'),
                field: 'userName',
                flex: 2,
                width: 130,
                cellClass: ['text-center'],
            },
            {
                headerName: this.l('Email'),
                headerTooltip: this.l('Email'),
                field: 'email',
                flex: 3,
                cellClass: ['text-center'],
            },
            {
                headerName: this.l('Trạng Thái'),
                headerTooltip: this.l('Trạng Thái'),
                field: 'status',
                flex: 2,
                cellClass: ['text-center'],
            },
            {
                headerName: this.l('Ngày tạo tài khoản'),
                headerTooltip: this.l('Ngày tạo tài khoản'),
                field: 'created',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.created),
                flex: 4,
                cellClass: ['text-center'],
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
            this.selectedAccountGuest = null;
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
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this.fundRaising.getAllListAccount(
            this.createFillter,
            this.statusFillter,
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedAccountGuest = selected.id;
        }
    }
}
