import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, TransactionOfFundForDto } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-view-detail-transaction',
    templateUrl: './view-detail-transaction.component.html',
    styleUrls: ['./view-detail-transaction.component.less']
})
export class ViewDetailTransactionComponent extends AppComponentBase implements OnInit {

    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active: boolean = false;
    saving: boolean = false;
    transaction;
    constructor(
        injector: Injector,
        private fundRaising: AdminFundRaisingServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit() {

    }

    show(transactionId?: number): void {
        this.transaction = new TransactionOfFundForDto();
        this.active = true;
        this.fundRaising.getInforTransactionById(transactionId)
            .subscribe((result) => {
                this.transaction = result;
                this.active = true;
                this.modal.show();
            });
        this.modal.show();
    }
    close() {
        this.active = false;
        this.modal.hide();
    }
}

