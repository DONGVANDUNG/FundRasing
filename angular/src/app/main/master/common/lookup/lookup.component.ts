import { GridApi, GridReadyEvent } from '@ag-grid-enterprise/all-modules';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { CustomColDef, FrameworkComponent, GridParams, PaginationParamsModel } from '@app/shared/common/models/base.model';
import { GridTableService } from '@app/shared/common/services/grid-table.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { MstCmmLookupDto, MstCmmLookupServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/internal/operators/finalize';
import { CreateOrEditLookupModalComponent } from './create-or-edit-lookup-modal.component';
import { DatePipe } from '@angular/common';
import { FileDownloadService } from '@shared/utils/file-download.service';
import ceil from 'lodash-es/ceil';
import { Paginator } from 'primeng/paginator';
import { AgCellButtonRendererComponent } from '@app/shared/common/grid/ag-cell-button-renderer/ag-cell-button-renderer.component';

@Component({
    templateUrl: './lookup.component.html',
    styleUrls: ['./lookup.component.less'],
})
export class LookupComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModalLookup', { static: true }) createOrEditModalLookup: | CreateOrEditLookupModalComponent | undefined;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    defaultColDefs: CustomColDef[] = [];
    paginationParams: PaginationParamsModel = {
        pageNum: 1,
        pageSize: 20,
        totalCount: 0,
        skipCount: 0,
        sorting: '',
        totalPage: 1,
    };

    selectedRow: MstCmmLookupDto = new MstCmmLookupDto();
    saveSelectedRow: MstCmmLookupDto = new MstCmmLookupDto();
    datas: MstCmmLookupDto = new MstCmmLookupDto();
    isLoading: boolean = false;
    dataParams: GridParams | undefined;
    rowData: any[] = [];
    gridApi!: GridApi;
    rowSelection = 'multiple';
    filter: string = '';
    pipe = new DatePipe('en-US');

    domainCode: string = '';
    itemCode: string = '';
    itemValue: string = '';
    itemOrder: number = 0;
    description: string = '';
    isUse: string = '';
    isRestrict: string = '';
    frameworkComponents: FrameworkComponent;

    defaultColDef = {
        resizable: true,
        sortable: true,
        floatingFilterComponentParams: { suppressFilterButton: true },
        filter: true,
        floatingFilter: true,
        suppressHorizontalScroll: true,
        textFormatter: function (r: any) {
            if (r == null) {
                return null;
            }
            return r.toLowerCase();
        },
        tooltip: (params) => params.value,
    };

    constructor(
        injector: Injector,
        private _service: MstCmmLookupServiceProxy,
        private gridTableService: GridTableService,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
        this.defaultColDefs = [
            {
                headerName: this.l('STT'),
                headerTooltip: this.l('STT'),
                cellRenderer: (params) => params.rowIndex + 1 + this.paginationParams.pageSize * (this.paginationParams.pageNum - 1), cellClass: ['text-center'], width: 60,
            },
            {
                headerName: this.l('Domain'),
                headerTooltip: this.l('Domain'),
                // width: 1800,
                children: [
                    {
                        headerName: this.l('Code'),
                        headerTooltip: this.l('Code'),
                        field: 'domainCode',
                        flex: 1
                    },
                    {
                        headerName: this.l('Item Code'),
                        headerTooltip: this.l('Item Code'),
                        field: 'itemCode',
                        flex: 1
                    }

                ]
            },
            {
                headerName: this.l('Item Value'),
                headerTooltip: this.l('Item Value'),
                field: 'itemValue',
                flex: 1
            },
            {
                headerName: this.l('Order'),
                headerTooltip: this.l('Order'),
                field: 'itemOrder',
                cellClass: ['text-right'],
                flex: 1
            },
            {
                headerName: this.l('Description'),
                headerTooltip: this.l('Description'),
                field: 'description',
                flex: 1
            },
            {
                headerName: this.l('Is Use'),
                headerTooltip: this.l('Is Use'),
                field: 'isUse',
                width: 120,
                cellClass: ['text-center'],
                cellRenderer: 'agCellButtonComponent',
                buttonDefTwo: {
                    text: params => (params.data?.isUse === 'Y') ? 'isUse' : 'InUse',
                    iconName: 'fa fa-circle',
                    className: params => (params.data?.isUse === 'Y') ? 'btnActive' : 'btnInActive',
                },

            },
            {
                headerName: this.l('Is Restrict'),
                headerTooltip: this.l('Is Restrict'),
                field: 'isRestrict',
                width: 120,
                cellClass: ['text-center'],
                cellRenderer: 'agCellButtonComponent',
                buttonDefTwo: {
                    text: params => (params.data?.isRestrict === 'Y') ? 'Restrict' : 'InRestrict',
                    iconName: 'fa fa-circle',
                    className: params => (params.data?.isRestrict === 'Y') ? 'btnActive' : 'btnInActive',
                },

            },
        ];
        this.frameworkComponents = {

            agCellButtonComponent: AgCellButtonRendererComponent,
        };
    }

    ngOnInit(): void {
        this.paginationParams = { pageNum: 1, pageSize: 20, totalCount: 0 };
    }

    searchDatas(): void {
        // this.paginator.changePage(this.paginator.getPage());
        this._service.getAll(
            this.domainCode,
            this.itemCode,
            this.itemValue,
            this.itemOrder,
            this.description,
            this.isUse,
            this.isRestrict,
            '',
            this.paginationParams.skipCount,
            this.paginationParams.pageSize
        )
            .pipe(finalize(() => this.gridTableService.selectFirstRow(this.dataParams!.api)))
            .subscribe((result) => {
                this.paginationParams.totalCount = result.totalCount;
                this.rowData = result.items;
                this.paginationParams.totalPage = ceil(result.totalCount / (this.paginationParams.pageSize ?? 0));
            });
    }

    clearTextSearch() {
        this.domainCode = '',
            this.itemCode = '',
            this.itemValue = '',
            this.itemOrder = 0,
            this.description = '',
            this.isUse = '',
            this.isRestrict = '',
            this.searchDatas();
    }

    onSelectionMulti(params) {
        var selectedRows = this.gridApi.getSelectedRows();
        var selectedRowsString = '';
        var maxToShow = 5;
        selectedRows.forEach(function (selectedRow, index) {
            if (index >= maxToShow) {
                return;
            }
            if (index > 0) {
                selectedRowsString += ', ';
            }
            selectedRowsString += selectedRow.athlete;
        });
        if (selectedRows.length > maxToShow) {
            var othersCount = selectedRows.length - maxToShow;
            selectedRowsString += ' and ' + othersCount + ' other' + (othersCount !== 1 ? 's' : '');
        }
        (document.querySelector('#selectedRows') as any).innerHTML = selectedRowsString;
    }

    getDatas(paginationParams?: PaginationParamsModel) {
        return this._service.getAll(
            this.domainCode,
            this.itemCode,
            this.itemValue,
            this.itemOrder,
            this.description,
            this.isUse,
            this.isRestrict,
            '',
            this.paginationParams.skipCount,
            this.paginationParams.pageSize
        );
    }

    onChangeRowSelection(params: { api: { getSelectedRows: () => MstCmmLookupDto[] } }) {
        this.saveSelectedRow = params.api.getSelectedRows()[0] ?? new MstCmmLookupDto();
        this.selectedRow = Object.assign({}, this.saveSelectedRow);
    }

    changePage(paginationParams) {
        this.isLoading = true;
        this.paginationParams = paginationParams;
        this.paginationParams.skipCount = (paginationParams.pageNum - 1) * paginationParams.pageSize;
        // this.maxResultCount = paginationParams.pageSize;
        this.getDatas(this.paginationParams).subscribe((result) => {
            this.paginationParams.totalCount = result.totalCount;
            this.rowData = result.items;
            this.paginationParams.totalPage = ceil(result.totalCount / (this.paginationParams.pageSize ?? 0));
            this.isLoading = false;
        });
    }

    callBackDataGrid(params: GridParams) {
        this.isLoading = true;
        this.dataParams = params;
        params.api.paginationSetPageSize(this.paginationParams.pageSize);
        this.paginationParams.skipCount =
            ((this.paginationParams.pageNum ?? 1) - 1) * (this.paginationParams.pageSize ?? 0);
        this.paginationParams.pageSize = this.paginationParams.pageSize;
        this.getDatas(this.paginationParams)
            .pipe(finalize(() => this.gridTableService.selectFirstRow(this.dataParams!.api)))
            .subscribe((result) => {
                this.paginationParams.totalCount = result.totalCount;
                this.rowData = result.items ?? [];
                this.paginationParams.totalPage = ceil(result.totalCount / (this.paginationParams.pageSize ?? 0));
                this.isLoading = false;
            });
    }

    deleteRow(system: MstCmmLookupDto): void {
        this.message.confirm(this.l('AreYouSureToDelete'), 'Delete Row', (isConfirmed) => {
            if (isConfirmed) {
                this._service.delete(system.id).subscribe(() => {
                    this.callBackDataGrid(this.dataParams!);
                    this.notify.success(this.l('SuccessfullyDeleted'));
                    this.notify.info(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }
    exportToExcel(): void {
        this.loaderVisible();
        this._service.getLookupToExcel(
            this.domainCode,
            this.itemCode,
            this.itemValue,
            this.itemOrder,
            this.description,
            this.isUse,
            this.isRestrict,
        )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
                this.loaderHidden();
            });
    }
    loaderVisible(){
        document.querySelectorAll<HTMLElement>('.lds-hourglass')[0].style.visibility = "visible";
        (<HTMLInputElement> document.getElementById("exportToExcel")).disabled = true;
    }
    loaderHidden(){
        document.querySelectorAll<HTMLElement>('.lds-hourglass')[0].style.visibility = "hidden";
        (<HTMLInputElement> document.getElementById("exportToExcel")).disabled = false;
    }
}
