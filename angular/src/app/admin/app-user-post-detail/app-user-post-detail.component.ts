import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DataDonateForFundInput, FundRaiserServiceProxy, TransactionOfFundForDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppUserDonationComponent } from './app-user-donation/app-user-donation.component';
import { DataFormatService } from '@app/shared/common/services/data-format.service';

@Component({
    selector: 'app-app-user-post-detail',
    templateUrl: './app-user-post-detail.component.html',
    styleUrls: ['./app-user-post-detail.component.less']
})
export class AppUserPostDetailComponent extends AppComponentBase implements OnInit {
    inputData: DataDonateForFundInput = new DataDonateForFundInput();
    activeIndex = 1;
    @ViewChild("donate", { static: true }) modal: AppUserDonationComponent;
    // @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    postId;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    inforFundDetail;
    imageUrl;
    isLoading;
    responsiveOptions
    saving;
    fundId;
    feeFund;
    totalAmount;
    listTransaction;
    amountOfMoney;
    noteTransaction;
    isDonate: boolean = false;
    textButton = 'Quyên góp';
    constructor(injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private dataFormatService: DataFormatService,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private route: ActivatedRoute) {
        super(injector)
    }
    ngOnInit(): void {
        this.postId = this.route.snapshot.params['postId'];
        this.fundId = this.route.snapshot.params['fundId'];
        this._userServiceProxy.getInforPostById(this.postId).subscribe(result => {
            this.inforFundDetail = result;
            this.inforFundDetail.amountDonatePresent = this.dataFormatService.moneyFormat(result.amountDonatePresent);
            this.inforFundDetail.amountDonateTarget = this.dataFormatService.moneyFormat(result.amountDonateTarget);
            this.imageUrl = this.baseUrl + this.inforFundDetail.listImageUrl[0];
        })
        this._userServiceProxy.getListTransactionForFund(this.fundId).subscribe(rs=>{
            this.listTransaction = rs;
            this.listTransaction.forEach((item)=>{
                item.amount = this.dataFormatService.moneyFormat(item.amount)
            })
        })
    }
    donateFundRaiser() {
        this.modal.show(this.inforFundDetail.fundId);
    }
    //   show(fundId) {
    //       this.inforFundDetail = new DataDonateForFundInput;
    //       this.amountOfMoney = undefined;
    //       this.fundId = fundId;
    //       this.noteTransaction = undefined;
    //       this._userServiceProxy.getInforFundRaisingById(fundId).subscribe(result => {
    //           this.inforFundDetail = result;
    //           this.imageUrl = this.baseUrl + result.listImageUrl[0];
    //       })
    //       this.modal.show();
    //   }
    //   close() {
    //       this.modal.hide();
    //   }
    //   limitedCharacter() {
    //       var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
    //       var textLimited = document.querySelector<HTMLElement>('.note-input');
    //       console.log(input);
    //       textLimited.textContent = `${256 - input.length} character(s) left`
    //   }
    openDonateInterface() {
        // if (this.activeIndex === 0) {
        //     this.activeIndex = 1;
        //     this.textButton = 'Quyên góp';
        // }
        // else {
        //     this.activeIndex = 0
        //     this.textButton = 'Hủy';
        // }
    }
    //   donateToFund() {
    //       this.inputData.amountOfMoney = this.amountOfMoney;
    //       this.inputData.fundId = this.fundId;
    //       this.inputData.noteTransaction = this.noteTransaction;
    //       if (this.amountOfMoney == null) {
    //           this.notify.warn("Nhập số tiền cần quyên góp");
    //           return;
    //       }
    //       this.isLoading = true;
    //       this._userServiceProxy.donateForFund(this.inputData).subscribe(
    //           () => {
    //               this.notify.success("Quyên góp thành công");
    //               this.isLoading = false;
    //               this.modal.hide();
    //           },
    //           (error => {
    //               this.notify.error(error);
    //               this.isLoading = false;
    //           }
    //           )
    //       )
    //   }
}
