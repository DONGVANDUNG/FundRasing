import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { UserFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-fund-package',
    templateUrl: './fund-package.component.html',
    styleUrls: ['./fund-package.component.less']
})
export class FundPackageComponent implements OnInit {
    listFundPackage = [];
    isLogin = localStorage.getItem("isLogin");
    constructor(private router: Router, private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormatService: DataFormatService) { }

    ngOnInit() {
        this._userServiceProxy.getListFundPackage().subscribe(result => {
            this.listFundPackage = result;
            this.listFundPackage.forEach(packages => {
                packages.paymentFee = this.dataFormatService.moneyFormat(packages.paymentFee);
                //packages.commission = this.dataFormatService.moneyFormat(packages.commission);
            })
        })
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("account-guest/home");
        }
        if (option === 2) {
            this.router.navigateByUrl("/project");
        }
        // if (option === 3) {
        //     this.router.navigateByUrl("account-guest/fund-package");
        // }
        if (option === 4) {
            this.router.navigateByUrl("account-guest/about-us");
        }
    }
    routerLink(){
        this.router.navigateByUrl('/app/notifications');
    }
}
