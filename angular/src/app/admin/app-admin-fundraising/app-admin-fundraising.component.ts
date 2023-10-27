import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-app-admin-fundraising',
  templateUrl: './app-admin-fundraising.component.html',
  styleUrls: ['./app-admin-fundraising.component.less']
})
export class AppAdminFundraisingComponent extends AppComponentBase implements OnInit {

  constructor( injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }

}
