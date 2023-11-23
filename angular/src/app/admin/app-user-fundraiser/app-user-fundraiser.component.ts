import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, InforDetailBankAcountDto, RegisterInforFundRaiserDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { error } from 'console';

@Component({
  selector: 'app-app-user-fundraiser',
  templateUrl: './app-user-fundraiser.component.html',
  styleUrls: ['./app-user-fundraiser.component.less']
})
export class AppUserFundraiserComponent extends AppComponentBase implements OnInit {
  listPackage = [];
  isChangeBankInfo: boolean = false;
  dataFundRaiser: RegisterInforFundRaiserDto = new RegisterInforFundRaiserDto;
  dataInforBankUser: InforDetailBankAcountDto = new InforDetailBankAcountDto;
  constructor(injector: Injector, private fundRaisingRepo: FundRaiserServiceProxy,private _userServiceProxy: UserServiceProxy) {
    super(injector);
  }

  ngOnInit() {
    this.dataFundRaiser = new RegisterInforFundRaiserDto();
    this._userServiceProxy.getListFundPackageForUserDonation().subscribe(rs=>{
        this.listPackage = rs;
    })
    this._userServiceProxy.getForEditFundRaiser().subscribe(result => {
      this.dataFundRaiser = result;
      if(!this.dataFundRaiser.id){
        this.notify.warn("Vui lòng đăng ký thông tin để tham gia gây quỹ")
      }
    })
    this._userServiceProxy.getInforBankUser().subscribe(result=>{
            this.dataInforBankUser = result;
    })
  }
  changeInforBank() {
    this.isChangeBankInfo = !this.isChangeBankInfo;
    var blockFundRaiser = document.querySelector<HTMLElement>('.block-fundraiser');
    var listInput = blockFundRaiser.querySelectorAll<HTMLElement>('.input-bank');
    listInput.forEach((e) => {
      if (this.isChangeBankInfo) {
        e.classList.remove('input-disable');
      }
      else
        e.classList.add('input-disable');
    })
  }
  save() {
    if (!this.dataFundRaiser.id) {
      this._userServiceProxy.registerFundRaiser(this.dataFundRaiser).subscribe(() => {this.notify.success("Đăng ký gây quỹ thành công") },
       (error => { this.notify.error("Đã xảy ra lỗi") }));
    }
    else {
      this._userServiceProxy.updateInforFundRaiser(this.dataFundRaiser).subscribe(()=>{
        this.notify.success("Cập nhật thông tin thành công")
      },error=>{
        this.notify.error("Có lỗi xảy ra");
      })
    }
  }
}
