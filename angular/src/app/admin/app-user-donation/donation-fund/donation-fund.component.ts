import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'app-donation-fund',
    templateUrl: './donation-fund.component.html',
    styleUrls: ['./donation-fund.component.less']
})
export class DonationFundComponent extends AppComponentBase implements OnInit {
    param:any;
    isLoading: boolean = false
    constructor(injector: Injector,private route: ActivatedRoute) {
        super(injector);
    }

    ngOnInit() {
        this.param = this.route.snapshot.params['fundId'];
        //console.log(this.param);
    }
    // redirectHome() {
    //     this.route.navigateByUrl('guest/donation')
    // }
    donateNow() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        if (input.length === 0) {
            this.notify.warn("Vui lòng nhập nội dung");
        }
        else {
            this.isLoading = true;
            setTimeout(() => {
                this.isLoading = false;
            }
                , 2000
            );
        }
    }
    limitedCharacter() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        var textLimited = document.querySelector<HTMLElement>('.note-input');
        console.log(input);
        textLimited.textContent = `${256 - input.length} character(s) left`
    }
}
