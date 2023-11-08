import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-user-home',
  templateUrl: './app-user-donation.component.html',
  styleUrls: ['./app-user-donation.component.less']
})
export class AppUserDonationComponent extends AppComponentBase {

  constructor(injector: Injector,private route:Router) {
    super(injector);
  }

  showFundDetail(){
    this.route.navigateByUrl('guest/home/fund-detail')
  }
  redirectHome(){
    this.route.navigateByUrl('guest/home')
  }

}
