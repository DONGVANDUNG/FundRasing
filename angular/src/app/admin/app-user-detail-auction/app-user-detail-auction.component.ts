import { Component, Injector, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { error } from 'console';

@Component({
    selector: 'app-app-user-detail-auction',
    templateUrl: './app-user-detail-auction.component.html',
    styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent extends AppComponentBase implements OnInit {
    private hubConnection: signalR.HubConnection;
    auctionId;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    // dataAuction: GetAllAuctionDto = new GetAllAuctionDto;
    // userAuction: UserAuction = new UserAuction;
    constructor(private route: ActivatedRoute,
        private injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        //private auctionService: AuctionService,
        public _zone: NgZone
    ) {
        super(injector);
    }

    ngOnInit() {
        this.init();
        this.auctionId = this.route.snapshot.params['auctionId'];
        //this.userAuction.auctionId = this.auctionId;
        // this._fundRaiser.getAuctionById(this.auctionId).subscribe(re => {
        //     this.dataAuction = re;
        // })

    }

    placeBid() {
        // if (this.userAuction.amountAuction === undefined) {
        //     this.notify.warn("Vui lòng nhập số tiền đấu giá");
        //     return;
        // }

        // this.auctionService.updateAuction(this.userAuction.amountAuction,this.dataAuction.id,this.userAuction.isPublic, () => {
        //     this.notify.success("Đấu giá vật phẩm thành công");
        // });
    }


    init() {
        // this.subscribeToEvent('app.chat.updateAmountAuction', (amountPresent, amountJumnpMin, amountJumnpMax) => {
        //     this.dataAuction.auctionPresentAmount = amountPresent;
        //     this.dataAuction.amountJumpMin = amountJumnpMin;
        //     this.dataAuction.amountJumpMax = amountJumnpMax;
        // });
    }
}
