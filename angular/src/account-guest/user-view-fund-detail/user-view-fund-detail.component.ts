import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { UserFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-view-fund-detail',
    templateUrl: './user-view-fund-detail.component.html',
    styleUrls: ['./user-view-fund-detail.component.less']
})
export class UserViewFundDetailComponent extends AppComponentBase implements OnInit {
    isLoading: boolean = false;
    postId;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    isFundRaiser;
    dataPost;
    activeIndex = 0;
    filterText;
    listTransaction= [];
    fundId;
    isLogin = localStorage.getItem("isLogin");
    responsiveOptions: any[]
    constructor(injector: Injector, private router: Router, private route: ActivatedRoute,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormatService: DataFormatService) {
        super(injector);
    }
    ngOnInit(): void {
        this.postId = this.route.snapshot.params['postId'];
        this.fundId = this.route.snapshot.params['fundId'];
        this._userServiceProxy.getInforPostById(this.postId).subscribe(result => {
            this.dataPost = result;
            this.dataPost.amountDonatePresent = this.dataFormatService.moneyFormat(result.amountDonatePresent);
            this.dataPost.amountDonateTarget = this.dataFormatService.moneyFormat(result.amountDonateTarget);
           // this.imageUrl = this.baseUrl + this.dataPost.listImageUrl[0];
            var progress = document.querySelector<HTMLElement>(".span-progress");
            progress.style.width = `${result.percentAchieved}%`
        });
       this.getAllFilter();
        this._userServiceProxy.checkUserIsFundRaiser().subscribe(result => {
            this.isFundRaiser = result;
        });
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
        this.init();
    }
    init() {
        const buttonShare = document.querySelector<HTMLAnchorElement>('.button-share-fb');
        let postUrl = encodeURI(document.location.href);
        let postTitle = encodeURI("Hi");
        buttonShare.setAttribute("href", `https://www.facebook.com/sharer.php?u=${postUrl}`);
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("account-guest/home");
        }
        // if (option === 2) {
        //     this.router.navigateByUrl("/project");
        // }
        if (option === 3) {
            this.router.navigateByUrl("account-guest/fund-package");
        }
        if (option === 4) {
            this.router.navigateByUrl("account-guest/about-us");
        }
    }
    requestToFundRaiser(){
        this.router.navigateByUrl("app/admin/register-fundraiser")
    }
    donateFundRaiser(){
        this.router.navigate(["/app/admin/user-post-detail",this.postId])
    }
    routerLink(){
        this.router.navigateByUrl('/app/notifications');
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
