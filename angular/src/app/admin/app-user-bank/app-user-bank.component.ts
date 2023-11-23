import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { InforDetailBankAcountDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-app-user-bank',
    templateUrl: './app-user-bank.component.html',
    styleUrls: ['./app-user-bank.component.less']
})
export class AppUserBankComponent extends AppComponentBase implements OnInit {

    listBank = [
        {name:'Argribank',code:'Argribank'},
        {name:'Tpbank',code:'Tpbank'},
        {name:'BIDV',code:'BIDV'},
        {name:'ViettinBank',code:'ViettinBank'},
        {name:'Sacombank',code:'Sacombank'},
        {name:'TechcomBank',code:'TechcomBank'},
    ];
    isChangeBankInfo: boolean = false;
    dataInforBankUser: InforDetailBankAcountDto = new InforDetailBankAcountDto;
    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy) {
        super(injector);
    }

    ngOnInit() {
        this._userServiceProxy.getInforBankUser().subscribe(result => {
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
        this._userServiceProxy.createOrEditAccountBank(this.dataInforBankUser).subscribe(() => {
            if(this.dataInforBankUser.id != null){
            this.notify.success("Sửa thông tin gây quỹ thành công")
            }
            else
                this.notify.success("Đăng ký thông tin gây quỹ thành công")
        },
            (error => { this.notify.error("Đã xảy ra lỗi") }));
    }
}
