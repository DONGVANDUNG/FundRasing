import { Component, Injector, OnInit } from '@angular/core';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { UserServiceProxy, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-app-user-history-auction',
  templateUrl: './app-user-history-auction.component.html',
  styleUrls: ['./app-user-history-auction.component.less']
})
export class AppUserHistoryAuctionComponent extends AppComponentBase implements OnInit {

    dataAution;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy,
        private _fundRaiser:FundRaiserServiceProxy,private dataFormatService : DataFormatService) {
        super(injector);
    }

    ngOnInit() {
        this._fundRaiser.getAllHistoryAuctionUser().subscribe(result=>{
            this.dataAution = result;
            this.dataAution.forEach((item)=>{
                item.auctionPresentAmount = this.dataFormatService.moneyFormat(item.auctionPresentAmount);
                item.startingPrice = this.dataFormatService.moneyFormat(item.startingPrice);
            })
        })
    }
}
