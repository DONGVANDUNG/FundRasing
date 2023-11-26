import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuctionService } from '@app/shared/layout/chat/auction-hub.service';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, GetAllAuctionDto, UserAuction } from '@shared/service-proxies/service-proxies';
import { error } from 'console';

@Component({
    selector: 'app-app-user-detail-auction',
    templateUrl: './app-user-detail-auction.component.html',
    styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent extends AppComponentBase implements OnInit {
    auctionId;
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    dataAuction: GetAllAuctionDto = new GetAllAuctionDto;
    constructor(private route: ActivatedRoute,
         private injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private auctionService : AuctionService
        ) {
        super(injector)
    }
    userAuction: UserAuction = new UserAuction;
    ngOnInit() {
        this.auctionId = this.route.snapshot.params['auctionId'];
        this.userAuction.auctionId = this.auctionId;
        this._fundRaiser.getAuctionById(this.auctionId).subscribe(re => {
            this.dataAuction = re;
        })
    }
    placeBid() {
        if (this.userAuction.amountAuction === undefined) {
            this.notify.warn("Vui lòng nhập số tiền đấu giá");
            return;
        }
        this._fundRaiser.userAuction(this.userAuction).subscribe(() => {
            this.notify.success("Đấu giá vật phẩm thành công");
            this.auctionService.getAmountAution().subscribe(result=>{
                this.dataAuction.auctionPresentAmount = result;
            })
        },
            (error) => {
                //this.notify.error("Đã xảy ra lỗi khi đấu giá");
            })
    }
}
