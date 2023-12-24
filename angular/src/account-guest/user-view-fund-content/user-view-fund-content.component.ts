import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppConsts } from '@shared/AppConsts';
import { UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-view-list-fund',
    templateUrl: './user-view-fund-content.component.html',
    styleUrls: ['./user-view-fund-content.component.less']
})
export class UserViewFundContentComponent implements OnInit {
    activeIndex = 1;
    listFundPackage = [];
    listFundActive = [];
    listFundClose = [];
    isLogin = localStorage.getItem("isLogin");
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    constructor(private router: Router, private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormatService: DataFormatService) { }

    ngOnInit() {
        this._userServiceProxy.getListFundPackage().subscribe(result => {
            this.listFundPackage = result;
            this.listFundPackage.forEach(packages => {
                packages.paymentFee = this.dataFormatService.moneyFormat(packages.paymentFee);
                packages.commission = this.dataFormatService.moneyFormat(packages.commission);
            })
        })
        this._userServiceProxy.getAllFundRaisingIsActive().subscribe(result=>{
            this.listFundActive = result;
            this.listFundActive.forEach((item) => {
                item.amountDonateTarget = this.dataFormatService.moneyFormat(item.amountDonateTarget);
                item.amountDonatePresent = this.dataFormatService.moneyFormat(item.amountDonatePresent);
            })
        })
        this._userServiceProxy.getAllFundRaisingIsClose().subscribe(result=>{
            this.listFundClose = result;
            this.listFundClose.forEach((item) => {
                item.amountDonateTarget = this.dataFormatService.moneyFormat(item.amountDonateTarget);
                item.amountDonatePresent = this.dataFormatService.moneyFormat(item.amountDonatePresent);
            })
        })
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("/home");
        }
        // if (option === 2) {
        //     this.router.navigateByUrl("/project");
        // }
        if (option === 3) {
            this.router.navigateByUrl("/fund-package");
        }
        if (option === 4) {
            this.router.navigateByUrl("/about-us");
        }
    }
    routerLink(){
        this.router.navigateByUrl('/app/notifications');
    }
}
