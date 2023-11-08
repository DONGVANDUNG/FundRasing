import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-user-view-fund-detail',
  templateUrl: './user-view-fund-detail.component.html',
  styleUrls: ['./user-view-fund-detail.component.less']
})
export class UserViewFundDetailComponent extends AppComponentBase {
  isLoading: boolean = false;
  constructor(injector: Injector, private route: Router) {
    super(injector)
  }

  ngOnInit() {
  }
  redirectHome() {
    this.route.navigateByUrl('guest/donation')
  }
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
