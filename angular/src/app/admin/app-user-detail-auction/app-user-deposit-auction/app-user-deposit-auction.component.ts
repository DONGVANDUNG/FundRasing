import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { InformationAuctionDepositDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
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
  auctionId;
  inforDepost: InformationAuctionDepositDto = new InformationAuctionDepositDto();
  constructor(injector: Injector,private userServiceProxy: UserServiceProxy) {
    super(injector);
  }

  ngOnInit() {
  }
  show(autionId): void {
    this.modal.show();
    this.auctionId = autionId;
    this.userServiceProxy.getInforAuctionDeposit(autionId).subscribe(re=>{
      this.inforDepost = re;
    })
  }
  close() {
    this.modal.hide();
  }
  depositAuction(){
    this.userServiceProxy.userDepositAuction(this.depositAmount,this.auctionId).subscribe(()=>{
      this.notify.success("Đặt cọc thành công.Bạn có thể tham gia phiên đấu giá ngay bây giờ.");
      this.modalSave.emit(null);
      this.modal.hide();
    })
  }
}
