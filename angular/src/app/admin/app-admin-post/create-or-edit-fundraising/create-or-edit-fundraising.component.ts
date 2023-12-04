import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrEditFundRaisingDto, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import * as moment from 'moment';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-create-or-edit-fundraising',
    templateUrl: './create-or-edit-fundraising.component.html',
    styleUrls: ['./create-or-edit-fundraising.component.less']
})
export class CreateOrEditFundraisingComponent extends AppComponentBase {
    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    isLoading;
    text;
    selectedCity;
    inputData: CreateOrEditFundRaisingDto = new CreateOrEditFundRaisingDto();
    listOption = [{
        code: true, name: 'Có'
    }, {
        code: false, name: 'Không'
    }]
    saving
    constructor(injector: Injector, private _fundRaiser: FundRaiserServiceProxy) {
        super(injector)
    }
    show(postId?) {
        this.modal.show();
        this.inputData.fundName = '';
        this.inputData.fundEndDate = null;
        this.inputData.fundRaisingDay = null;
        this.inputData.amountDonationTarget = 0;
    }
    close() {
        this.modal.hide();
    }
    save() {
        // var input = new CreateOrEditFundRaisingDto();
        // input.fundName = this.inputData.fundName;
        // input.fundEndDate = this.inputData.fundEndDate;
        // input.fundRaisingDay = this.inputData.fundRaisingDay;
        // input.amountDonationTarget = this.inputData.amountDonationTarget;
        this._fundRaiser.createFundRaising(
            this.inputData
        ).subscribe(
            () => {
                this.notify.success("Thêm quỹ thành công");
                this.modalSave.emit(null);

                this.modal.hide();
            }, (error) => {
                this.notify.error("Đã xảy ra lỗi")
            })
    }
    // onChangeDate(event, type) {
    //     if (type === 'start') {
    //         this.inputData.fundRaisingDay = event;
    //     }
    //     else this.inputData.fundEndDate = event
    // }
}
