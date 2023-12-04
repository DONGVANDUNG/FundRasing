import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrEditFundRaisingDto, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
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
    inputData: {
        fundName: string,
        fundRaisingDay: DateTime,
        fundEndDate: DateTime,
        amountDonationTarget: number
    } = {
        fundName: null,
        fundRaisingDay: null,
        fundEndDate: null,
        amountDonationTarget: null,
        }
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

    }
    close() {
        this.modal.hide();
    }
    save() {
        var input = new CreateOrEditFundRaisingDto();
        input.fundName = this.inputData.fundName;
        input.fundEndDate = this.inputData.fundEndDate;
        input.fundRaisingDay = this.inputData.fundRaisingDay;
        input.amountDonationTarget = this.inputData.amountDonationTarget;
        this._fundRaiser.createFundRaising(
            input
        ).subscribe(
            () => {
                this.notify.success("Thêm quỹ thành công");
                this.modalSave.emit(null);

                this.modal.hide();
            }, (error) => {
                this.notify.error("Đã xảy ra lỗi")
            })
    }
    onChangeDate(event, type) {
        if (type === 'start') {
            this.inputData.fundRaisingDay = event;
        }
        else this.inputData.fundEndDate = event
    }
}
