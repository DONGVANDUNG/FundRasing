import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-fundraising-live-sites',
    templateUrl: './fundraising-live-sites.component.html',
    styleUrls: ['./fundraising-live-sites.component.less']
})
export class FundraisingLiveSitesComponent implements OnInit {
    isLoading: boolean = false;
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
