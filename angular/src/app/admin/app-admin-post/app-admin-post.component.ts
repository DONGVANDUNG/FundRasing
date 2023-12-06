import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, FundRaiserServiceProxy, UserFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { ceil } from 'lodash-es';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { CreateOrEditFundraisingComponent } from '../app-admin-fundraising/create-or-edit-fundraising/create-or-edit-fundraising.component';

@Component({
    selector: 'app-app-admin-post',
    templateUrl: './app-admin-post.component.html',
    styleUrls: ['./app-admin-post.component.less']
})
export class AppAdminPostComponent extends AppComponentBase implements OnInit {

    @ViewChild("createOrEditPost") modalCreatePost: CreateOrEditPostComponent;
    @ViewChild("createOrEditFund") modalCreateFund: CreateOrEditFundraisingComponent;
    columnDefs;
    columnPostDefs;
    dateOfReport;
    rowData: any = [];
    rowDataPost: any = [];
    defaultColDef;
    filter: string = '';
    isPayFee;
    createdDateFilter;
    maxResultCount: number = 20;
    skipCount: number = 0;
    sorting: string = '';
    paginationParams: PaginationParamsModel;
    params: GridParams;

    advancedFiltersAreShown: boolean;
    createdDate;
    typePackage;
    listTypePackage = [
        { label: 'Tuần', value: 'Tuần' },
        { label: 'Tháng', value: 'Tháng' },
        { label: 'Năm', value: 'Năm' },

    ]
    selectedFund;
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
        private _funRaiser: FundRaiserServiceProxy,
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
                headerName: this.l('Chủ đề bài đăng'),
                headerTooltip: this.l('Chủ đề bài đăng'),
                field: 'postTopic',
                width: 250,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tạo bài đăng'),
                headerTooltip: this.l('Ngày tạo bài đăng'),
                field: 'postCreated',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.postCreated),
                width: 250,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Tên quỹ'),
                headerTooltip: this.l('Tên quỹ'),
                field: 'fundName',
                width: 250,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày tạo quỹ'),
                headerTooltip: this.l('Ngày tạo quỹ'),
                field: 'fundRaisingDay',
                valueGetter: params => this.dataFormatService.dateFormat(params.data.fundRaisingDay),
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Ngày kết thúc'),
                headerTooltip: this.l('Ngày kết thúc'),
                valueGetter: params => this.dataFormatService.dateFormat(params.data.fundEndDate),
                field: 'fundEndDate',
                width: 150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Mục tiêu quỹ'),
                headerTooltip: this.l('Mục tiêu quỹ'),
                field: 'amountOfTarget',
                valueGetter: params => this.dataFormatService.moneyFormat(params.data.amountOfTarget) + " VND",
                width:150,
                cellClass: ['text-left'],
            },
            {
                headerName: this.l('Trạng thái'),
                headerTooltip: this.l('Trạng thái'),
                field: 'status',
                width:120,
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
        return this._funRaiser.getAllFundRaiserPost(
            this.sorting ?? null,
            paginationParams ? paginationParams.skipCount : 0,
            paginationParams ? paginationParams.pageSize : 20
        );
    }
    onChangeSelection(paginationParams) {
        const selected = paginationParams.api.getSelectedRows()[0];
        if (selected) {
            this.selectedFund = selected.id;
        }
    }
    deleteFundPackage() {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._funRaiser
                    .closeFundRaising(this.selectedFund)
                    .subscribe(() => {
                        this.onGridReady(this.paginationParams);
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        })
    }
    createPost() {
        // if(!this.selectedFund){
        //     this.notify.warn("Vui lòng chọn một bản ghi gây quỹ");
        //     return;
        // }
        this.modalCreatePost.show(this.selectedFund);
    }
    createFund(){
        this.modalCreateFund.show()
    }
    editFund(){
        this.modalCreateFund.show(this.selectedFund)
    }
    editPost(){

    }
}
