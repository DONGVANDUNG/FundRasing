import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { PaginationParamsModel } from '@app/shared/common/models/base.model';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { GridParams } from '@app/shared/common/models/base.model';
import { ceil } from 'lodash-es';
import * as moment from 'moment';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { ViewDetailTransactionComponent } from './view-detail-transaction/view-detail-transaction.component';

@Component({
    selector: 'app-app-admin-fund-transaction',
    templateUrl: './app-admin-fund-transaction.component.html',
    styleUrls: ['./app-admin-fund-transaction.component.less']
})
export class AppAdminFundTransactionComponent extends AppComponentBase implements OnInit {
    @ViewChild("viewDetailTransaction")  viewDetail : ViewDetailTransactionComponent;
    columnDefsFundRaising;
    columnDefsTransaction;
    typeOfReport = 0;
    dateOfReport;
    rowDataFundRaising: any = [];
    rowDataTransaction: any = [];
    defaultColDef;
    filter: string = '';
    maxResultCountFundRaising: number = 20;
    maxResultCountTransaction: number = 20;
    skipCountFundRaising: number = 0;
    skipCountTransaction: number = 0;
    sorting: string = '';
    paginationParamsFundRaising: PaginationParamsModel;
    paginationParamsTransaction: PaginationParamsModel;
    selectedFundRaising;
    selectedTransaction;
    paramsFundRaising: GridParams;
    paramsTransaction: GridParams;
    advancedFiltersAreShown: boolean;
    fromDate = moment().add(-30, 'day');
    toDate = moment();
    selectionFundRaising;
    selectionTransaction;
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
        // private dataFormatService: DataFormatService
    ) {
        super(injector);
        this.columnDefsFundRaising = [
            {
                headerName: this.l('STT'),
                headerTooltip: this.l('STT'),
                cellRenderer: params => params.rowIndex + (this.paginationParamsFundRaising.pageNum! - 1) * this.paginationParamsFundRaising.pageSize! + 1,
                field: 'no',
                cellClass: ['text-left'],
                minWidth: 100,
            },
            {
                headerName: this.l('Tên quỹ'),
                headerTooltip: this.l('Tên quỹ'),
                field: 'fundName',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Người tạo quỹ'),
                headerTooltip: this.l('Người tạo quỹ'),
                field: 'fundRaiser',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tạo quỹ'),
                headerTooltip: this.l('Ngày tạo quỹ'),
                field: 'fundRaisingDay',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.fundRaisingDay),
                flex: 2,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Mức donate'),
                headerTooltip: this.l('Mức donate'),
                field: 'amountOfMoney',
                flex: 3,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Trạng thái'),
                headerTooltip: this.l('Trạng thái'),
                field: 'status',
                flex: 4,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày kết thúc quỹ'),
                headerTooltip: this.l('Ngày kết thúc quỹ'),
                field: 'fundFinishDay',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.fundFinishDay),
                flex: 4,
                cellClass: ['text-left'],
            },
        ];
        this.columnDefsTransaction = [
            {
                headerName: this.l('STT'),
                headerTooltip: this.l('STT'),
                cellRenderer: params => params.rowIndex + (this.paginationParamsTransaction.pageNum! - 1) * this.paginationParamsTransaction.pageSize! + 1,
                field: 'no',
                cellClass: ['text-left'],
                minWidth: 100,
            },
            {
                headerName: this.l('Người gửi'),
                headerTooltip: this.l('Người gửi'),
                field: 'sender',
                flex: 3,
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Người nhận'),
                headerTooltip: this.l('Người nhận'),
                field: 'receiver',
                flex: 2,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Số tiền donate'),
                headerTooltip: this.l('Số tiền donate'),
                field: 'amount',
                flex: 3,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Nội dung chuyển tiền'),
                headerTooltip: this.l('Nội dung chuyển tiền'),
                field: 'content',
                flex: 4,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày chuyển tiền'),
                headerTooltip: this.l('Ngày chuyển tiền'),
                field: 'createdTime',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.createdTime),
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

    ngOnInit() {
        this.paginationParamsFundRaising = { pageNum: 1, pageSize: 20, totalCount: 0 };
        this.onGridReadyFundRaising(this.paginationParamsFundRaising);

        this.paginationParamsTransaction = { pageNum: 1, pageSize: 20, totalCount: 0 };
        //this.onGridReadyTransaction(this.paginationParamsTransaction);
    }
    callBackGridFundRaising(params) {
        this.paramsFundRaising = params;
        this.paramsFundRaising.api.paginationSetPageSize(this.paginationParamsFundRaising.pageSize);
    }

    onGridReadyFundRaising(paginationParams) {
        this.rowDataFundRaising = [];
        this.paginationParamsFundRaising = paginationParams;
        this.paginationParamsFundRaising.skipCount = (paginationParams.pageNum - 1) * paginationParams.pageSize;
        this.maxResultCountFundRaising = paginationParams.pageSize;
        this.getAllFundRaising(this.paginationParamsFundRaising).subscribe((result) => {
            this.rowDataFundRaising = result.items;
            this.paginationParamsFundRaising.totalPage = ceil(result.totalCount / this.maxResultCountFundRaising);
            this.paginationParamsFundRaising.totalCount = result.totalCount;
            this.paramsFundRaising.api.setRowData(this.rowDataFundRaising);
            this.selectedFundRaising = null;
            this.onGridReadyTransaction(this.paginationParamsTransaction);
        });
    }
    getAllFundRaising(paginationParams: PaginationParamsModel) {
        return this.fundRaising.getListFundRaising(
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelectionFundRaising(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectionFundRaising = selected.id;
            this.onGridReadyTransaction(this.paginationParamsTransaction);
        }
    }
    // callBackGridTransaction(params) {
    //     this.paramsTransaction = params;
    //     this.paramsTransaction.api.paginationSetPageSize(this.paginationParamsTransaction.pageSize);
    //     this.onGridReadyTransaction(this.paginationParamsTransaction);
    // }
    onGridReadyTransaction(paginationParams) {
        this.rowDataTransaction = [];
        this.paginationParamsTransaction = paginationParams;
        this.paginationParamsTransaction.skipCount = (paginationParams.pageNum - 1) * paginationParams.pageSize;
        this.maxResultCountTransaction = paginationParams.pageSize;
        this.getAllTransaction(this.paginationParamsTransaction).subscribe((result) => {
            this.rowDataTransaction = result;
            this.paginationParamsTransaction.totalPage = ceil(result.totalCount / this.maxResultCountTransaction);
            this.paginationParamsTransaction.totalCount = result.totalCount;
            this.paramsTransaction.api.setRowData(this.rowDataTransaction);
            this.selectedTransaction = null;
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

    getAllTransaction(paginationParams: PaginationParamsModel) {
        return this.fundRaising.getListTransactionForFund(
            this.selectedFundRaising,
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelectionTransaction(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectionTransaction = selected.id;
        }
    }
    search() {
        this.onGridReadyFundRaising(this.paginationParamsFundRaising);
    }
    clearValueFilter() {
        this.onGridReadyFundRaising(this.paginationParamsFundRaising);
    }
    viewDetailsTransaction(selectedTransaction){
            this.viewDetail.show(selectedTransaction);
    }
}
