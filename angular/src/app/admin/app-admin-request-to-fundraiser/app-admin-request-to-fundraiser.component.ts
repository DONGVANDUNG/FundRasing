import { Component, Injector, OnInit } from '@angular/core';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { ceil } from 'lodash-es';

@Component({
    selector: 'app-app-admin-request-to-fundraiser',
    templateUrl: './app-admin-request-to-fundraiser.component.html',
    styleUrls: ['./app-admin-request-to-fundraiser.component.less']
})
export class AppAdminRequestToFundraiserComponent extends AppComponentBase implements OnInit {
    columnDefs;
    dateOfReport;
    rowData: any = [];
    defaultColDef;
    saving;
    filter: string = '';
    maxResultCount: number = 20;
    skipCount: number = 0;
    sorting: string = '';
    paginationParams: PaginationParamsModel;
    params: GridParams;
    advancedFiltersAreShown: boolean;
    createdDate;
    typePackage;
    selectedRequest;
    isApprove;
    userId;
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
                headerName: this.l('Tài khoản yêu cầu'),
                headerTooltip: this.l('Tài khoản yêu cầu'),
                field: 'userName',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày yêu cầu'),
                headerTooltip: this.l('Ngày yêu cầu'),
                field: 'requestTime',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.requestTime),
                flex: 2,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Trạng thái'),
                headerTooltip: this.l('Trạng thái'),
                field: 'isApprove',
                valueGetter: params => params.data.isApprove === true ? " Đã phê duyệt" : "Chưa phê duyệt",
                flex: 3,
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
        this.createdDate = null;
        this.typePackage = null;
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this._adminServiceProxy.getAllRequestToFundRaiser(
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedRequest = selected.id;
            this.isApprove = selected.isApprove;
            this.userId = selected.userId;
        }
    }
    approve() {
        if (this.selectedRequest) {
            if (this.isApprove !== true) {
                this.saving = true;
                this._adminServiceProxy.approveFundRaiser(this.userId).subscribe(() => {
                    this.notify.success("Phê duyệt thành công");
                    this.saving = false;
                    this.onGridReady(this.paginationParams);
                })
            }
            else
                this.notify.warn("Yêu cầu này đã được phê duyệt");
                this.saving = false;
        }
        else
            this.notify.warn("Vui lòng chọn một bản ghi phê duyệt");
    }
}
