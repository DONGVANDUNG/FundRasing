import { Component, OnInit, ViewChild } from '@angular/core';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites/fundraising-live-sites.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FundPackageComponent } from './fund-package/fund-package.component';
import { UserHomeComponent } from './user-home/user-home.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.less']
})
export class UserComponent implements OnInit {
  @ViewChild("fundRaisingLive") fundRaisingLive: FundraisingLiveSitesComponent;
  @ViewChild("fundPackage") fundPackage: FundPackageComponent;
  @ViewChild("userHome") userHome: UserHomeComponent;
  @ViewChild("contactUs") contactUs: ContactUsComponent;
  constructor() { }

  ngOnInit() {
  }

}
