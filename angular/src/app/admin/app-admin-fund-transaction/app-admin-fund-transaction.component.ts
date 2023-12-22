import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { PaginationParamsModel } from '@app/shared/common/models/base.model';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { GridParams } from '@app/shared/common/models/base.model';
import { ceil } from 'lodash-es';
import * as moment from 'moment';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { ViewDetailTransactionComponent } from './view-detail-transaction/view-detail-transaction.component';
import { DateTime } from 'luxon';

@Component({
    selector: 'app-app-admin-fund-transaction',
    templateUrl: './app-admin-fund-transaction.component.html',
    styleUrls: ['./app-admin-fund-transaction.component.less']
})
export class AppAdminFundTransactionComponent extends AppComponentBase implements OnInit {
    @ViewChild("viewDetailTransaction")  viewDetail : ViewDetailTransactionComponent;
    columnDefsTransaction;
    createdDate;
    rowDataFundRaising: any = [];
    rowDataTransaction: any = [];
    defaultColDef;
    filter: string = '';
    maxResultCountTransaction: number = 20;
    skipCountTransaction: number = 0;
    sorting: string = '';
    paginationParamsTransaction: PaginationParamsModel;
    fundId = 0;
    transactionDate = new Date();
    selectedTransaction;
    paramsTransaction: GridParams;
    advancedFiltersAreShown: boolean;
    fromDate = moment().add(-30, 'day');
    toDate = moment();
    selectionFundRaising;
    selectionTransaction;
    listFundName = [];
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
        private dataFormatService: DataFormatService,
        private adminFundRaising :AdminFundRaisingServiceProxy
        // private dataFormatService: DataFormatService
    ) {
        super(injector);
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
                field: 'userDonate',
                minWidth: 190,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Số tiền donate'),
                headerTooltip: this.l('Số tiền donate'),
                field: 'amount',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.amount) + " VND",
                minWidth: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày chuyển tiền'),
                headerTooltip: this.l('Ngày chuyển tiền'),
                field: 'createdTime',
                minWidth: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Nội dung chuyển tiền'),
                headerTooltip: this.l('Nội dung chuyển tiền'),
                field: 'content',
                minWidth:500,
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
        this.paginationParamsTransaction = { pageNum: 1, pageSize: 20, totalCount: 0 };
        this.onGridReadyTransaction(this.paginationParamsTransaction);
        this.adminFundRaising.getListFundName().subscribe(re=>{
            this.listFundName = re;
        })
    }
    callBackGridTransaction(params) {
        this.paramsTransaction = params;
        this.paramsTransaction.api.paginationSetPageSize(this.paginationParamsTransaction.pageSize);
        this.onGridReadyTransaction(this.paginationParamsTransaction);
    }
    onGridReadyTransaction(paginationParams) {
        this.rowDataTransaction = [];
        this.paginationParamsTransaction = paginationParams;
        this.paginationParamsTransaction.skipCount = (paginationParams.pageNum - 1) * paginationParams.pageSize;
        this.maxResultCountTransaction = paginationParams.pageSize;
        this.getAllTransaction(this.paginationParamsTransaction).subscribe((result) => {
            this.rowDataTransaction = result.items;
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
    changePageFundTransaction(paginationParamsTransaction){
        this.onChangeSelectionTransaction(paginationParamsTransaction)
    }

    getAllTransaction(paginationParams: PaginationParamsModel) {
        return this.adminFundRaising.getListTransactionForFund(
            DateTime.fromJSDate(this.transactionDate),
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
        this.onGridReadyTransaction(this.paginationParamsTransaction);
    }
    clearValueFilter() {
    }
    // viewDetailsTransaction(selectedTransaction){
    //         this.viewDetail.show(selectedTransaction);
    // }
}
