import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { InformationAuctionDepositDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-user-deposit-auction',
  templateUrl: './app-user-deposit-auction.component.html',
  styleUrls: ['./app-user-deposit-auction.component.less']
})
export class AppUserDepositAuctionComponent extends AppComponentBase implements OnInit {
  @ViewChild("deposit", { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  saving;
  active;
  depositAmount;
  auctionItemId:number;
  inforDepost;
  constructor(injector: Injector,private userServiceProxy: UserFundRaisingServiceProxy,
    private dataFormatService:DataFormatService) {
    super(injector);
  }

  ngOnInit() {
  }
  show(autionItemId:number): void {
    this.modal.show();
    this.auctionItemId = autionItemId;
    this.userServiceProxy.getInforAuctionDeposit(this.auctionItemId).subscribe(re=>{
      this.inforDepost = re;
      this.inforDepost.maxAmountDepost = this.dataFormatService.moneyFormat(re.maxAmountDepost);
      this.inforDepost.minAmountDepost = this.dataFormatService.moneyFormat(re.minAmountDepost);
    })
  }
  close() {
    this.modal.hide();
  }
  depositAuction(){
    this.userServiceProxy.userDepositAuction(this.depositAmount,this.auctionItemId).subscribe(()=>{
      this.notify.success("Đặt cọc thành công. Bạn có thể tham gia phiên đấu giá ngay bây giờ.");
      this.modalSave.emit(null);
      this.modal.hide();
    })
  }
}
