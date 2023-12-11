import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { ceil } from 'lodash-es';
import * as moment from 'moment';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { DateTime } from 'luxon';
@Component({
  selector: 'app-admin-fundRaiser',
  templateUrl: './app-admin-fundRaiser.component.html',
  styleUrls: ['./app-admin-fundRaiser.component.less']
})
export class AppAdminFundRaiserComponent extends AppComponentBase implements OnInit {

    // @ViewChild('createOrEdit') createOrEditModal: CreateOrEditVodReportComponent;
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
    listStatusAccount = [
        {
            value:true, label: 'Hoạt động',
        },
        {
            value: false, label: 'Không hoạt động',
        }
    ]
    createdJoin = new Date();
    status = true;
    email;
    params: GridParams;
    advancedFiltersAreShown: boolean;
    fromDate = moment().add(-30,'day');
    toDate = moment();
    selectedReport;
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
                cellClass: ['text-left'],
                width: 80,
            },
            {
                headerName: this.l('Tên đăng nhập'),
                headerTooltip: this.l('Tên đăng nhập'),
                field: 'userName',
                width: 200,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tạo tài khoản'),
                headerTooltip: this.l('Ngày tạo tài khoản'),
                field: 'created',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.created),
                width: 160,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Email'),
                headerTooltip: this.l('Email'),
                field: 'email',
                width: 210,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tham gia gây quỹ'),
                headerTooltip: this.l('Ngày tham gia gây quỹ'),
                field: 'fundRaiserDate',
                width: 180,
                valueGetter: params => this.dataFormatService.dateFormat(params.data.fundRaiserDate),
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Trạng thái'),
                headerTooltip: this.l('Trạng thái'),
                field: 'status',
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Gói quỹ'),
                headerTooltip: this.l('Gói quỹ'),
                field: 'fundPackage',
                width: 180,
                cellClass: ['text-left'],
            },
        ];
        this.defaultColDef = {
            minwidth: 100,
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

    callBackGridDepartment(params) {
        this.params = params;
        this.params.api.paginationSetPageSize(this.paginationParams.pageSize);
        this.onGridReady(this.paginationParams);
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
        this.createdJoin = new Date();
        this.status = true;
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this.fundRaising.getAllListFundRaiser(
            DateTime.fromJSDate(this.createdJoin),
            this.status,
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedReport = selected.id;
        }
    }
}
