import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { PaginationParamsModel } from '@app/shared/common/models/base.model';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, CreateOrEditFundPackageDto } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
    selector: 'app-create-or-edit-fund-package',
    templateUrl: './create-or-edit-fund-package.component.html',
    styleUrls: ['./create-or-edit-fund-package.component.less']
})
export class CreateOrEditFundPackageComponent extends AppComponentBase implements OnInit {

    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    listStatus = [{
        label: 'Hoạt động', value: true
    }, { label: 'Không hoạt động', value: false }];
    listDepartment = [];
    typeReport = 1;
    paginationParams: PaginationParamsModel;
    totalCounts: number;

    listUser = [];
    columnDefs = [];
    defaultColDef
    rowDataRepreson = [];
    active: boolean = false;
    saving: boolean = false;
    listTypePackage = [
        {
            label: 'Tuần', value: 'Tuần'
        }, {
            label: 'Tháng', value: 'Tháng'
        }, {
            label: 'Năm', value: 'Năm'
        }
    ]
    inputFundPackage: CreateOrEditFundPackageDto = new CreateOrEditFundPackageDto();
    response: any;
    constructor(
        injector: Injector,
        private _adminServiceProxy: AdminFundRaisingServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit() {

    }

    show(fundPackageId?: number): void {
        if (!fundPackageId) {
            this.inputFundPackage = new CreateOrEditFundPackageDto();
            this.inputFundPackage.id = fundPackageId;
            this.active = true;
            this.modal.show();
        } else {
            this._adminServiceProxy.getForEditFundPackage(fundPackageId)
                .subscribe((result) => {
                    this.inputFundPackage = result;
                    this.active = true;
                    this.modal.show();
                });
        }
    }
    save() {
        this.active = true;
        this._adminServiceProxy.createOrEditFundPackage(this.inputFundPackage)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l("SavedSuccessfully"));
                this.close();
                this.modalSave.emit(null);
                this.saving = false;
            })
    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }

}
