import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'app-user-view-home',
    templateUrl: './user-view-home.component.html',
    styleUrls: ['./user-view-home.component.less']
})
export class UserViewHomeComponent extends AppComponentBase {
    selectedTabHome = true;
    selectedTabDonate: boolean = false;
    selectedTabRegister: boolean = false;
    selectedTabSignIn: boolean = false;
    selectedTabCheckOut: boolean = false;
    selectedFunDetail: boolean = false;
    isLoading = false;
    blockDonateSuccess: boolean = false;
    constructor(injector: Injector, private router: Router) {
        super(injector);
    }
    selectTab(option) {
        const menuNav = document.querySelector<HTMLElement>(".list-nav");
        const listNav = document.querySelectorAll<HTMLElement>('.item-nav');

        menuNav.addEventListener("click", (event) => {
            const clickedItem = event.target as HTMLElement;
            if (clickedItem && clickedItem.classList.contains('item-nav')) {
                listNav.forEach(nav => nav.classList.remove("active"));
                clickedItem.classList.add("active");
            }
        });
        // this.selectedTabHome = false;
        // this.selectedTabDonate = false;
        // this.selectedTabRegister = false;
        // this.selectedTabSignIn = false;
        // this.selectedTabCheckOut = false;
        if (option === "home") {
            this.router.navigateByUrl('/guest/home')
        }
        if (option === "donate") {
            // this.router.navigateByUrl('/guest/donation')
        }
        if (option === "register") {
            this.router.navigateByUrl('/guest/register')
        }
        if (option === "sigin") {
            this.router.navigateByUrl('/guest/sigin')
        }
        if (option === "checkout") {
            this.router.navigateByUrl('/guest/checkout')
        }
    }
    showFundDetail() {
        this.selectedFunDetail = true;
    }
    limitedCharacter() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        var textLimited = document.querySelector<HTMLElement>('.note-input');
        console.log(input);
        textLimited.textContent = `${256 - input.length} character(s) left`
    }
    donateNow() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        if (input.length === 0) {
            this.notify.warn("Vui lòng nhập nội dung");
        }
        else {
            this.isLoading = true;
            setTimeout(() => {
                this.isLoading = false;
                this.selectedFunDetail = true;
                this.blockDonateSuccess = true;
            }
                , 2000
            );
        }
    }
}
