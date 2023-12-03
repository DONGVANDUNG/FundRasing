import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-fundraising-detail',
    templateUrl: './fundraising-detail.component.html',
    styleUrls: ['./fundraising-detail.component.less']
})
export class FundraisingDetailComponent implements OnInit {
    isLoading: boolean = false;
    listFuns = ['1'] // List quỹ đang gây dựng
    listFunsFinish = ['1','2','3','4'] // List quỹ đã kết thúc
    selectedTab: string = 'dangGayQuy';

    constructor(private router: Router) {
        this.isLoading = true;
        setTimeout(() => {
            this.isLoading = false;
        }
            , 1000)
    }

    ngOnInit() {
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("guest/home");
        }
        if (option === 2) {
            this.router.navigateByUrl("guest/fund-raising-live");
        }
        if (option === 3) {
            this.router.navigateByUrl("guest/fund-package");
        }
        if (option === 4) {
            this.router.navigateByUrl("guest/about-us");
        }
    }    
}
