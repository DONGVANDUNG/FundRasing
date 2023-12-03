import { Component, Injector, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, GetAllAuctionDto, GetAuctionDetailDto, UserAuction } from '@shared/service-proxies/service-proxies';
import { error } from 'console';
import { AppUserDepositAuctionComponent } from './app-user-deposit-auction/app-user-deposit-auction.component';
import { AuctionService } from '@app/shared/layout/chat/auction-hub.service';

@Component({
    selector: 'app-app-user-detail-auction',
    templateUrl: './app-user-detail-auction.component.html',
    styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent extends AppComponentBase implements OnInit {
    @ViewChild("deposit") modalDeposit : AppUserDepositAuctionComponent;
    private hubConnection: signalR.HubConnection;
    auctionId;
    isDeposit :boolean = false;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    dataAuction :GetAuctionDetailDto;
    userAuction:UserAuction = new UserAuction();
    constructor(private route: ActivatedRoute,
        injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private auctionService: AuctionService,
        public _zone: NgZone
    ) {
        super(injector);
    }

    ngOnInit() {
        this.init();
        this.auctionId = this.route.snapshot.params['auctionId'];
        this.userAuction.auctionId = this.auctionId;
        this._fundRaiser.getAuctionById(this.auctionId).subscribe(re => {
            this.dataAuction = re;
        })
        this.checkUserDeposit();
    }
    checkUserDeposit(){
        this._fundRaiser.checkUserDepositAuction().subscribe(re=>{
            this.isDeposit = re;
        })
    }
    placeBid() {
        if (this.userAuction.amountAuction === undefined) {
            this.notify.warn("Vui lòng nhập số tiền đấu giá");
            return;
        }
        this.auctionService.updateAuction(this.userAuction.amountAuction,this.dataAuction.id,this.userAuction.isPublic, () => {
            this.notify.success("Đấu giá vật phẩm thành công");
        });
    }


    init() {
        this.subscribeToEvent('app.chat.updateAmountAuction', (amountPresent, amountJumnpMin, amountJumnpMax) => {
            this.dataAuction.auctionPresentAmount = amountPresent;
            this.dataAuction.amountJumpMin = amountJumnpMin;
            this.dataAuction.amountJumpMax = amountJumnpMax;
        });
    }
    depositAuction(){
        this.modalDeposit.show(this.auctionId);
    }
}
