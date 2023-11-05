import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-view-donation.component.html',
  styleUrls: ['./user-view-donation.component.less']
})
export class UserViewDonationComponent extends AppComponentBase {

  constructor(injector: Injector) {
    super(injector);
  }

  showFundDetail(){

  }

}
