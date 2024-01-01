import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DataDonateForFundInput, FundRaiserServiceProxy, TransactionOfFundForDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-user-donation',
    templateUrl: './app-user-donation.component.html',
    styleUrls: ['./app-user-donation.component.less']
})
export class AppUserDonationComponent extends AppComponentBase implements OnInit {
    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave = new EventEmitter();
    fundId;
    isLoading;
    amountOfMoney;
    noteTransaction;
    activeIndex = 0;
    inforFundDetail;
    imageUrl;
    saving;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    listTransaction: TransactionOfFundForDto[] = []
    inputData: DataDonateForFundInput = new DataDonateForFundInput();
    constructor(injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private route: ActivatedRoute,
        private router: Router) {
        super(injector)
    }
    ngOnInit(): void {
        this.amountOfMoney = undefined;
        this.noteTransaction = undefined;
    }
    show(fundId?: number): void {
        this.fundId = fundId;
        this.modal.show();
    }
    limitedCharacter() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        var textLimited = document.querySelector<HTMLElement>('.note-input');
        console.log(input);
        textLimited.textContent = `${256 - input.length} character(s) left`
    }
    donateToFund() {
        this.inputData.amountOfMoney = this.amountOfMoney;
        this.inputData.fundId = this.fundId;
        this.inputData.noteTransaction = this.noteTransaction;
        if (this.amountOfMoney == null) {
            this.notify.warn("Nhập số tiền cần quyên góp");
            return;
        }
        this.isLoading = true;
        this._userServiceProxy.donateForFund(this.inputData).subscribe(
            () => {
                this.notify.success("Quyên góp thành công");
                this.isLoading = false;
                //this.router.navigateByUrl('app/admin/user-post');
                this.modal.hide();
                this.modalSave.emit(null);
            },
            (error => {
                this.isLoading = false;
            }
            )
        )
    }
    openDonateInterface() {
        if (this.activeIndex === 0) {
            this.activeIndex = 1;
        }
        else {
            this.activeIndex = 0
        }
    }
    close(){
        this.modal.hide();
    }
}
