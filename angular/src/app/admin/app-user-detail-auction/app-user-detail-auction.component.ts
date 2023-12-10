import { Component, Injector, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, GetAllAuctionDto, GetAuctionDetailDto, UserAuction } from '@shared/service-proxies/service-proxies';
import { error } from 'console';
import { AppUserDepositAuctionComponent } from './app-user-deposit-auction/app-user-deposit-auction.component';
// import { AuctionService } from '@app/shared/layout/chat/auction-hub.service';
import { DataFormatService } from '@app/shared/common/services/data-format.service';

@Component({
    selector: 'app-app-user-detail-auction',
    templateUrl: './app-user-detail-auction.component.html',
    styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent extends AppComponentBase implements OnInit {
    @ViewChild("deposit") modalDeposit : AppUserDepositAuctionComponent;
    private hubConnection: signalR.HubConnection;
    auctionId:number;
    auctionItemId;
    isDeposit :boolean = false;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    dataAuction;
    userAuction:UserAuction = new UserAuction();
    constructor(private route: ActivatedRoute,
        injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        //private auctionService: AuctionService,
        public _zone: NgZone,
        private dataFormateService: DataFormatService
    ) {
        super(injector);
    }

    ngOnInit() {
        //this.init();
        this.auctionId = this.route.snapshot.params['auctionId'];
        this.userAuction.auctionItemId = this.auctionId;
        this.userAuction.isPublic = false;
        this._fundRaiser.getAuctionById(this.auctionId).subscribe(re => {
            this.dataAuction = re;
            this.dataAuction.nextMinimumBid = this.dataFormateService.moneyFormat(re.nextMinimumBid);
            this.dataAuction.nextMaximumBid= this.dataFormateService.moneyFormat(re.nextMaximumBid);
            this.dataAuction.auctionPresentAmount= this.dataFormateService.moneyFormat(re.auctionPresentAmount);
        })
        //this.checkUserDeposit();
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
        // this.auctionService.updateAuction(this.userAuction.amountAuction,this.userAuction.auctionItemId,this.userAuction.isPublic, () => {
        //     this.notify.success("Đấu giá vật phẩm thành công");
        // });
    }


    // init() {
    //     this.subscribeToEvent('app.chat.updateAmountAuction', (amountPresent, amountJumnpMin, amountJumnpMax) => {
    //         this.dataAuction.auctionPresentAmount = amountPresent;
    //         this.dataAuction.amountJumpMin = amountJumnpMin;
    //         this.dataAuction.amountJumpMax = amountJumnpMax;
    //     });
    // }
    depositAuction(){
        this.modalDeposit.show(this.auctionId);
    }
}
