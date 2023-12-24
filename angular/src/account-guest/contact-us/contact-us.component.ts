import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-contact-us',
    templateUrl: './contact-us.component.html',
    styleUrls: ['./contact-us.component.less']
})
export class ContactUsComponent implements OnInit {

    constructor(private router: Router) { }
    isLoading: boolean = false;
    isLogin = localStorage.getItem("isLogin");
    ngOnInit() {
        this.isLoading = true;
        setTimeout(() => {
            this.isLoading = false;
        }
            , 1000)
    }
    redirectLink(option) {
        if (option === 1) {
            this.router.navigateByUrl("/home");
        }
        if (option === 2) {
            this.router.navigateByUrl("/project");
        }
        if (option === 3) {
            this.router.navigateByUrl("/fund-package");
        }
        // if (option === 4) {
        //     this.router.navigateByUrl("/about-us");
        // }
    }
    routerLink(){
        this.router.navigateByUrl('/app/notifications');
    }
}
