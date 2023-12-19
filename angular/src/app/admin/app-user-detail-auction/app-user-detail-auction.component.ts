import { Component, Injector, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, GetAllAuctionDto, GetAuctionDetailDto, UserAuction, UserFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { error } from 'console';
import { AppUserDepositAuctionComponent } from './app-user-deposit-auction/app-user-deposit-auction.component';
// import { AuctionService } from '@app/shared/layout/chat/auction-hub.service';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AuctionService } from '@app/shared/layout/chat/auction-hub.service';

@Component({
    selector: 'app-app-user-detail-auction',
    templateUrl: './app-user-detail-auction.component.html',
    styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent extends AppComponentBase implements OnInit {
    @ViewChild("deposit") modalDeposit: AppUserDepositAuctionComponent;
    private hubConnection: signalR.HubConnection;
    auctionItemId: number;
    isDeposit: boolean = false;
    nextMinimumBid;
    nextMaximumBid
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    dataAuction;
    auction;
    userAuction: UserAuction = new UserAuction();
    isValidAuction: boolean = true;
    constructor(private route: ActivatedRoute,
        injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private auctionService: AuctionService,
        public _zone: NgZone,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormateService: DataFormatService
    ) {
        super(injector);
    }

    ngOnInit() {
        this.init();
        if (!this.isValidAuction) {
            this.notify.warn("Mức tiều đấu giá không hợp lệ");
        }
        this.auctionItemId = this.route.snapshot.params['auctionId'];
        this.userAuction.auctionItemId = this.auctionItemId;
        this.userAuction.isPublic = false;
        this._fundRaiser.getAuctionById(this.auctionItemId).subscribe(re => {
            this.dataAuction = re;
            this.nextMinimumBid = this.dataAuction.nextMinimumBid;
            this.nextMaximumBid = this.dataAuction.nextMaximumBid;
            this.dataAuction.nextMinimumBid = this.dataFormateService.moneyFormat(re.nextMinimumBid);
            this.dataAuction.nextMaximumBid = this.dataFormateService.moneyFormat(re.nextMaximumBid);
            this.dataAuction.auctionPresentAmount = this.dataFormateService.moneyFormat(re.auctionPresentAmount);
        })
        this.checkUserDeposit();
    }
    checkUserDeposit() {
        this._fundRaiser.checkUserDepositAuction().subscribe(re => {
            this.isDeposit = re;
        })
    }
    placeBid() {
        if (this.userAuction.amountAuction === undefined) {
            this.notify.warn("Vui lòng nhập số tiền đấu giá");
            return;
        }
        if (this.nextMinimumBid > this.userAuction.amountAuction || this.nextMaximumBid < this.userAuction.amountAuction) {
            this.notify.warn("Mức tiền đấu giá không hợp lệ");
            return;
        }
        this.auction = true;
        this.auctionService.updateAuction(this.userAuction.amountAuction, this.userAuction.auctionItemId, this.userAuction.isPublic, () => {
            this.auction = false;
        });
    }


    init() {
        this.subscribeToEvent('app.chat.updateAmountAuction', (amountPresent, amountJumnpMin, amountJumnpMax, isValidAuction) => {
            this.dataAuction.auctionPresentAmount = this.dataFormateService.moneyFormat(amountPresent);
            this.dataAuction.nextMinimumBid = this.dataFormateService.moneyFormat(amountJumnpMin);
            this.dataAuction.nextMaximumBid = this.dataFormateService.moneyFormat(amountJumnpMax);
            this.isValidAuction = isValidAuction;
            this.nextMinimumBid = amountJumnpMin;
            this.nextMaximumBid = amountJumnpMax;
        });
    }
    depositAuction() {
        this._userServiceProxy.checkUserRegisterBankAccount().subscribe(re => {
            if (re === true) {
                this.modalDeposit.show(this.auctionItemId);
            }
        })
    }
}
