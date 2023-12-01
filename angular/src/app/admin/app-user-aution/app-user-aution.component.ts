import { Component, Injector, OnInit } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-app-user-aution',
    templateUrl: './app-user-aution.component.html',
    styleUrls: ['./app-user-aution.component.less']
})
export class AppUserAutionComponent extends AppComponentBase implements OnInit {
    dataAution = [];
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy,
        private _fundRaiser:FundRaiserServiceProxy) {
        super(injector);
    }

    ngOnInit() {
        // this._fundRaiser.getAllAuctionUser().subscribe(result=>{
        //     this.dataAution = result;
        // })
    }
    showFundDetail(){}
}
