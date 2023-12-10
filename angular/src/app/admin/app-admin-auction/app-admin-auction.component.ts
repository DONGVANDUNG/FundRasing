import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AdminFundRaisingServiceProxy, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { ceil } from 'lodash-es';
import { CreateOrEditAuctionComponent } from './create-or-edit-auction/create-or-edit-auction.component';
import { AppComponentBase } from '@shared/common/app-component-base';
@Component({
    selector: 'app-app-admin-auction',
    templateUrl: './app-admin-auction.component.html',
    styleUrls: ['./app-admin-auction.component.less']
})
export class AppAdminAuctionComponent extends AppComponentBase implements OnInit {
    @ViewChild("createOrEdit") modalCreate: CreateOrEditAuctionComponent;
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
    createdDate;
    typeAuction;
    listTypeAuction = [
        { label: 'Tuần', value: 'Tuần' },
        { label: 'Tháng', value: 'Tháng' },
        { label: 'Năm', value: 'Năm' },

    ]
    selectedAuction;
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
        private _fundRaiser: FundRaiserServiceProxy,
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
                width: 50,
            },
            // {
            //     headerName: this.l('Tiêu đề bài đăng'),
            //     headerTooltip: this.l('Tiêu đề bài đăng'),
            //     field: 'titleAuction',
            //     width: 240,
            //     cellClass: ['text-left'],
            // },
            {
                headerName: this.l('Tên vật phẩm'),
                headerTooltip: this.l('Tên vật phẩm'),
                field: 'itemName',
                width: 290,
                cellClass: ['text-left'],
            },
            // {
            //     headerName: this.l('Giới thiệu vật phẩm'),
            //     headerTooltip: this.l('Giới thiệu vật phẩm'),
            //     field: 'introduceItem',
            //     width: 280,
            //     cellClass: ['text-left'],

            // },
            {
                headerName: this.l('Bước nhảy tối thiểu'),
                headerTooltip: this.l('Bước nhảy tối thiểu'),
                field: 'amountJumpMin',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.amountJumpMin) +" VND",
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Bước nhảy tối đa'),
                headerTooltip: this.l('Bước nhảy tối đa'),
                field: 'amountJumpMax',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.amountJumpMax) +" VND",
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Giá khởi điểm'),
                headerTooltip: this.l('Giá khởi điểm'),
                field: 'startingPrice',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.startingPrice) +" VND",
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Giá hiện tại'),
                headerTooltip: this.l('Giá hiện tại'),
                field: 'auctionPresentAmount',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.auctionPresentAmount) +" VND",
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Số lượng'),
                headerTooltip: this.l('Số lượng'),
                field: 'amount',
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày bắt đầu'),
                headerTooltip: this.l('Ngày bắt đầu'),
                field: 'startDate',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.startDate),
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày kết thúc'),
                headerTooltip: this.l('Ngày kết thúc'),
                field: 'endDate',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.endDate),
                width: 130,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Trạng thái'),
                headerTooltip: this.l('Trạng thái'),
                field: 'status',
                width: 130,
                cellClass: ['text-left'],
            },
        ];
        this.defaultColDef = {
            minwidth:100,
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
        this.typeAuction = null;
    }

    getAll(paginationParams: PaginationParamsModel) {
        return this._fundRaiser.getAllAuctionAdmin(
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedAuction = selected.id;
        }
    }
    deleteFundAuction() {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._fundRaiser
                    .deleteAuction(this.selectedAuction)
                    .subscribe(() => {
                        this.onGridReady(this.paginationParams);
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        })
    }
    createAuction() {
        this.modalCreate.show();
    }
    editAuction(){
        this.modalCreate.show(this.selectedAuction)
    }

}
