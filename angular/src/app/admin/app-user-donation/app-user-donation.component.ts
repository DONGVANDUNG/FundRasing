import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DataDonateForFundInput, FundRaiserServiceProxy, TransactionOfFundForDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-user-home',
  templateUrl: './app-user-donation.component.html',
  styleUrls: ['./app-user-donation.component.less']
})
export class AppUserDonationComponent extends AppComponentBase implements OnInit {
    fundId;
    isLoading;
    amountOfMoney;
    noteTransaction;
    activeIndex = 0;
    listTransaction : TransactionOfFundForDto[] = []
    inputData: DataDonateForFundInput = new DataDonateForFundInput();
    constructor(injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private _userServiceProxy: UserServiceProxy,
        private route:ActivatedRoute,
        private router:Router) {
        super(injector)
    }
    ngOnInit(): void {
        this.fundId = this.route.snapshot.params['fundId'];
        this.amountOfMoney = undefined;
        this.noteTransaction = undefined;
        this._fundRaiser.getListTransactionForFund(this.fundId).subscribe(result=>{
            this.listTransaction = result;
        })
    }

    limitedCharacter() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        var textLimited = document.querySelector<HTMLElement>('.note-input');
        console.log(input);
        textLimited.textContent = `${256 - input.length} character(s) left`
    }
    donateToFund() {
        this.inputData.amountOfMoney = this.amountOfMoney;
        this.inputData.fundId = this.fundId;
        this.inputData.noteTransaction = this.noteTransaction;
        if (this.amountOfMoney == null) {
            this.notify.warn("Nhập số tiền cần quyên góp");
            return;
        }
        this.isLoading = true;
        this._userServiceProxy.donateForFund(this.inputData).subscribe(
            () => {
                this.notify.success("Quyên góp thành công");
                this.isLoading = false;
                this.router.navigateByUrl('/admin/post');
            },
            (error => {
                this.notify.error(error);
                this.isLoading = false;
            }
            )
        )
    }
    openDonateInterface() {
        if (this.activeIndex === 0) {
            this.activeIndex = 1;
        }
        else {
            this.activeIndex = 0
        }
    }
}
