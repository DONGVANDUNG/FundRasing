import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-donation-success',
    templateUrl: './donation-success.component.html',
    styleUrls: ['./donation-success.component.less']
})
export class DonationSuccessComponent implements OnInit {
    param: any;
    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        this.param = this.route.snapshot.params['fundId'];
        console.log(this.param);
    }

}
