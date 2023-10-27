import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { ceil } from 'lodash-es';
import * as moment from 'moment';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
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
    listVodTypeReport = [
        {
            value: 1, label: 'Tuần',
        },
        {
            value: 2, label: 'Tháng',
        },
        {
            value: 3, label: 'Năm',
        }

    ]
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
        // private dataFormatService: DataFormatService
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
                headerName: this.l('Description'),
                headerTooltip: this.l('Description'),
                field: 'description',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Name'),
                headerTooltip: this.l('Name'),
                field: 'name',
                flex: 2,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Position'),
                headerTooltip: this.l('Position'),
                field: 'position',
                flex: 3,
                cellClass: ['text-left'],
                // valueFormatter: (params) => {
                //     return this.dataFormatService.dateFormat(params.value)
                // },
            },
            {
                headerName: this.l('StatusAccount'),
                headerTooltip: this.l('StatusAccount'),
                field: 'statusAccount',
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
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this.fundRaising.getListFundRaiser(
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
