import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy, InforDetailBankAcountDto, RegisterInforFundRaiserDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
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
    isRegister: boolean = false;
    constructor(injector: Injector, private fundRaisingRepo: FundRaiserServiceProxy,
        private _userServiceProxy: UserFundRaisingServiceProxy) {
        super(injector);
    }

    ngOnInit() {
        this.dataFundRaiser = new RegisterInforFundRaiserDto();
        this._userServiceProxy.getListFundPackageCombobox().subscribe(rs => {
            this.listPackage = rs;
        })
        this._userServiceProxy.getForEditFundRaiser().subscribe(result => {
            this.dataFundRaiser = result;
            this.isRegister = result.isRequest;
            if (result == null) {
                this.notify.warn("Vui lòng đăng ký thông tin để tham gia gây quỹ")
            }
            if (!this.dataFundRaiser.fundPackageId) {
                this.dataFundRaiser.id = 0;
            }
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
            this._userServiceProxy.registerFundRaiser(this.dataFundRaiser).subscribe((result) => {
                this.notify.success("Đăng ký thành công.Yêu cầu của bạn sẽ sớm được phê duyệt!");
                this.dataFundRaiser = result;
                this.isRegister = true;
            },
            );
        }
    }
}
