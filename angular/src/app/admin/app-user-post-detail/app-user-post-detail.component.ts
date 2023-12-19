import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DataDonateForFundInput, FundRaiserServiceProxy, GetFundsDetailByIdForUser, TransactionOfFundForDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
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
    isFundRaiser:boolean = false;
    isDonate: boolean = false;
    textButton = 'Quyên góp';
    constructor(injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private dataFormatService: DataFormatService,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private route: ActivatedRoute,
        private router:Router) {
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
            var progress = document.querySelector<HTMLElement>(".span-progress");
            progress.style.width = `${result.percentAchieved}%`
        });
        this._userServiceProxy.getListTransactionForFund(this.fundId).subscribe(rs => {
            this.listTransaction = rs;
            this.listTransaction.forEach(transaction => {
                //transaction.createdTime = this.dataFormatService.dateFormatTransaction(transaction.createdTime);
                transaction.amount = this.dataFormatService.moneyFormat(transaction.amount)
                //transaction.amount = this.dataFormatService.moneyFormat(transaction.amount);
            })
        });
        this._userServiceProxy.checkUserIsFundRaiser().subscribe(result=>{
            this.isFundRaiser = result;
        })
        // const link = 'https://openjavascript.info/2022/08/22/using-json-in-javascript/';
        // const msg = encodeURIComponent('Hey');
        // const title = encodeURIComponent(document.querySelector('title').textContent);
        // console.log([link,msg,title])

        // const buttonShare = document.querySelector<HTMLAnchorElement>('.button-share-fb');
        // buttonShare.href = `https://www/facebook.com/share.php?u=${link}`
        // console.log(buttonShare);
        this.init();
    }
    init() {
        const buttonShare = document.querySelector<HTMLAnchorElement>('.button-share-fb');
        let postUrl = encodeURI(document.location.href);
        let postTitle = encodeURI("Hi");
        buttonShare.setAttribute("href", `https://www.facebook.com/sharer.php?u=${postUrl}`)
    }
    donateFundRaiser() {
        if(this.inforFundDetail.isCloseFund === 2){
            this.notify.warn("Dự án đã kết thúc");
            return;
        }
        this._userServiceProxy.checkUserRegisterBankAccount().subscribe((re)=>{
            if(re === true && this.inforFundDetail.isCloseFund === 1){
                this.modal.show(this.inforFundDetail.fundId);
            }
        })
    }
    requestToFundRaiser(){
        this.router.navigateByUrl("app/admin/register-fundraiser")
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
    // shareFaceBook(){
    //     const link = encodeURI(window.location.href);
    //     const msg = encodeURIComponent('Hey');
    //     const title = encodeURIComponent(document.querySelector('title').textContent);
    //     console.log([link,msg,title])

    //     const buttonShare = document.querySelector<HTMLElement>('.button-share-fb')[0];
    //     buttonShare.href = `https://www/facebook.com/share.php?u=${link}`
    // }
}
