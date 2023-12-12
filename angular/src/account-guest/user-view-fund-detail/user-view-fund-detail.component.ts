import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { UserFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-view-fund-detail',
    templateUrl: './user-view-fund-detail.component.html',
    styleUrls: ['./user-view-fund-detail.component.less']
})
export class UserViewFundDetailComponent extends AppComponentBase {
    isLoading: boolean = false;
    constructor(injector: Injector, private router: Router, private _userServiceProxy: UserFundRaisingServiceProxy,
        private dataFormatService: DataFormatService) {
        super(injector)
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("guest/home");
        }
        if (option === 2) {
            this.router.navigateByUrl("guest/project");
        }
        if (option === 3) {
            this.router.navigateByUrl("guest/fund-package");
        }
        if (option === 4) {
            this.router.navigateByUrl("guest/about-us");
        }
    }
}
