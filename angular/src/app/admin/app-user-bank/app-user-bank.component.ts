import { Component, Injector, OnInit } from '@angular/core';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { InforDetailBankAcountDto, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-app-user-bank',
    templateUrl: './app-user-bank.component.html',
    styleUrls: ['./app-user-bank.component.less']
})
export class AppUserBankComponent extends AppComponentBase implements OnInit {

    listBank = [
        { name: 'Argribank', code: 'Argribank' },
        { name: 'Tpbank', code: 'Tpbank' },
        { name: 'BIDV', code: 'BIDV' },
        { name: 'ViettinBank', code: 'ViettinBank' },
        { name: 'Sacombank', code: 'Sacombank' },
        { name: 'TechcomBank', code: 'TechcomBank' },
    ];
    isChangeBankInfo: boolean = false;
    dataInforBankUser;
    constructor(injector: Injector,
        private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormatService: DataFormatService) {
        super(injector);
    }

    ngOnInit() {
        this._userServiceProxy.getInforBankUser().subscribe(result => {
            if (result != null) {
                this.dataInforBankUser = result;
                this.dataInforBankUser.balance = this.dataFormatService.moneyFormat(result.balance);
            }
            else {
                this.dataInforBankUser.id = 0;
                this.dataInforBankUser.bankName = null;
                this.dataInforBankUser.bankNumber = null;
                this.dataInforBankUser.accountName = null;
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
        if (this.dataInforBankUser.bankName) {
            this.notify.warn("Vui lòng chọn ngân hàng")
        }
        if (this.dataInforBankUser.bankNumber) {
            this.notify.warn("Vui lòng nhập số tài khoản")
        }
        if (this.dataInforBankUser.accountName) {
            this.notify.warn("Vui lòng nhập tên tài khoản")
        }
        this._userServiceProxy.createOrEditAccountBank(this.dataInforBankUser).subscribe((re) => {
            if (this.dataInforBankUser.id != null) {
                this.notify.success("Sửa thông tin gây quỹ thành công")
            }
            else
                this.notify.success("Đăng ký tài khoản ngân hàng thành công")
            this.dataInforBankUser = re;
            this.dataInforBankUser.balance = this.dataFormatService.moneyFormat(re.balance);
        },
        );
    }
    toUpperCase() {
        this.dataInforBankUser.accountName = this.dataInforBankUser.accountName.toUpperCase();
    }
}
