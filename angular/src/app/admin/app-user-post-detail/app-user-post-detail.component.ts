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
    responsiveOptions = [];
    saving;
    fundId;
    feeFund;
    totalAmount;
    listTransaction;
    filterText;
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
        this.getAllFilter();
        this._userServiceProxy.checkUserIsFundRaiser().subscribe(result=>{
            this.isFundRaiser = result;
        })
        this.init();
        this.responsiveOptions = [
            {
                breakpoint: '300px',
                numVisible: 1,
                numScroll: 1
            },
            {
                breakpoint: '600px',
                numVisible: 2,
                numScroll: 1
            }
        ];
    }
    init() {
        const buttonShare = document.querySelector<HTMLAnchorElement>('.button-share-fb-1');
        let postUrl = encodeURI(document.location.href);
        let postTitle = encodeURI("Hi");
        buttonShare.setAttribute("href", `https://www.facebook.com/sharer.php?u=${postUrl}`);
    }
    donateFundRaiser() {
        // if(this.inforFundDetail.isCloseFund === 3){
        //     this.notify.warn("Dự án đã kết thúc");
        //     return;
        // }
        this._userServiceProxy.checkUserRegisterBankAccount().subscribe((re)=>{
            if(re === true && this.inforFundDetail.isCloseFund === 2){
                this.modal.show(this.inforFundDetail.fundId);
            }
        })
    }
    requestToFundRaiser(){
        this.router.navigateByUrl("app/admin/register-fundraiser")
    }
   getAllFilter(){
    this._userServiceProxy.getListTransactionForFund(this.fundId,this.filterText).subscribe(rs => {
        this.listTransaction = rs;
        this.listTransaction.forEach(transaction => {
            //transaction.createdTime = this.dataFormatService.dateFormatTransaction(transaction.createdTime);
            transaction.amount = this.dataFormatService.moneyFormat(transaction.amount)
            //transaction.amount = this.dataFormatService.moneyFormat(transaction.amount);
        })
    });
   }
}
