import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetListFundRasingDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-checkout',
    templateUrl: './app-user-checkout.component.html',
    styleUrls: ['./app-user-checkout.component.less']
})
export class AppUserCheckoutComponent extends AppComponentBase implements OnInit {
    postId;
    imageUrl
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    listFundRaisingHistory = [];
    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy,
        private route: ActivatedRoute) {
        super(injector);
    }
    ngOnInit() {
        // this.postId = this.route.snapshot.params['postId'];
        this.getAllFundRaising();
    }
    getAllFundRaising() {
        this._userServiceProxy.getHistoryDonationForFund().subscribe((result) => {
            this.listFundRaisingHistory = result;
        })
    }

}
