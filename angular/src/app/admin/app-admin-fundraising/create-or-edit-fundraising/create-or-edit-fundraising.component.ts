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
    fundRaisingDate;
    fundRaisingEndDate;
    inputData: CreateOrEditFundRaisingDto = new CreateOrEditFundRaisingDto();
    listOption = [{
        code: true, name: 'Có'
    }, {
        code: false, name: 'Không'
    }]
    saving;
    // options = {
    //     precision: 0,
    //     align: "left",
    //     prefix: '',
    //     thousands: ',',
    //     inputMode: CurrencyMaskInputMode.FINANCIAL,
    // };
    constructor(injector: Injector, private _fundRaiser: FundRaiserServiceProxy) {
        super(injector)
    }
    show(fundId?) {
        if (!fundId) {
            this.modal.show();
            this.inputData.fundName = '';
            this.inputData.fundEndDate = null;
            this.inputData.fundRaisingDay = null;
            this.inputData.amountDonationTarget = null;
            this.fundRaisingDate = null;
            this.fundRaisingEndDate = null;
        }
        else
            this._fundRaiser.getForEditFundRaising(fundId).subscribe(re => {
                this.inputData = re;
                this.fundRaisingDate = re.fundRaisingDay;
                this.fundRaisingEndDate = re.fundEndDate;
                this.fundRaisingDate = new Date(this.fundRaisingDate);
                this.fundRaisingEndDate = new Date(this.fundRaisingEndDate);

            });
        this.modal.show();
    }
    close() {
        this.inputData = new CreateOrEditFundRaisingDto();
        this.modal.hide();
    }
    save() {
        // var input = new CreateOrEditFundRaisingDto();
        // input.fundName = this.inputData.fundName;
        // input.fundEndDate = this.inputData.fundEndDate;
        // input.fundRaisingDay = this.inputData.fundRaisingDay;
        // input.amountDonationTarget = this.inputData.amountDonationTarget;
        this.inputData.fundRaisingDay = DateTime.fromJSDate(this.fundRaisingDate);
        this.inputData.fundEndDate = DateTime.fromJSDate(this.fundRaisingEndDate);
        this._fundRaiser.createOrEditFundRaising(
            this.inputData
        ).subscribe(
            () => {
                if (this.inputData.id) {
                    this.notify.success("Sửa thông tin quỹ thành công");
                }
                else {
                    this.notify.success("Thêm thông tin quỹ thành công");
                }
                this.modalSave.emit(null);

                this.modal.hide();
            })
    }
    // onChangeDate(event, type) {
    //     if (type === 'start') {
    //         this.inputData.fundRaisingDay = event;
    //     }
    //     else this.inputData.fundEndDate = event
    // }

}
