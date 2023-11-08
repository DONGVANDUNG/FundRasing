import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactUsComponent } from 'account-guest/contact-us/contact-us.component';
import { FundraisingLiveSitesComponent } from './fundraising-live-sites/fundraising-live-sites.component';
import { FundPackageComponent } from './fund-package/fund-package.component';
import { Route, Router } from '@angular/router';
import { UserViewHomeComponent } from './user-view-home/user-view-home.component';

@Component({
    selector: 'app-account-guest',
    templateUrl: './account-guest.component.html',
    styleUrls: ['./account-guest.component.less']
})
export class AccountGuestComponent implements OnInit {

    @ViewChild("fundRaisingLive") fundRaisingLive: FundraisingLiveSitesComponent;
    @ViewChild("fundPackage") fundPackage: FundPackageComponent;
    @ViewChild("userHome") userHome: UserViewHomeComponent;
    @ViewChild("contactUs") contactUs: ContactUsComponent;
    constructor(private router: Router) { }

    ngOnInit() {
    }
    gotoLogin() {
        this.router.navigateByUrl('/guest/login');
    }
    redirectLink(option){
        this.router.navigateByUrl(`guest/${option}`);
    }
}
