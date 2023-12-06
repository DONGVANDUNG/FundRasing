import { Component, Injector, OnInit } from '@angular/core';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, GetAllAuctionDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-app-user-aution',
    templateUrl: './app-user-aution.component.html',
    styleUrls: ['./app-user-aution.component.less']
})
export class AppUserAutionComponent extends AppComponentBase implements OnInit {
    dataAution:GetAllAuctionDto[] = [];
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy,
        private _fundRaiser:FundRaiserServiceProxy,private dataFormatService : DataFormatService) {
        super(injector);
    }

    ngOnInit() {
        this._fundRaiser.getAllAuctionUser().subscribe(result=>{
            this.dataAution = result;
            this.dataAution.forEach((item)=>{
                //item.auctionPresentAmount = this.dataFormatService.moneyFormat(item.auctionPresentAmount);
            })
        })
    }
    showFundDetail(){}
}
