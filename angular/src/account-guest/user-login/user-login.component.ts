import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-user-login',
    templateUrl: './user-login.component.html',
    styleUrls: ['./user-login.component.less']
})
export class UserLoginComponent implements OnInit {
    selectedTabHome = true;
    selectedTabDonate: boolean = false;
    selectedTabRegister: boolean = false;
    selectedTabSignIn: boolean = false;
    selectedTabCheckOut: boolean = false;
    selectedFunDetail: boolean = false;
    constructor(injectore: Injector, private router: Router) { }
    ngOnInit() {
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
        this.selectedTabHome = false;
        this.selectedTabDonate = false;
        this.selectedTabRegister = false;
        this.selectedTabSignIn = false;
        this.selectedTabCheckOut = false;
        if (option === "home") {
            this.selectedTabHome = true;
        }
        if (option === "donate") {
            this.selectedTabDonate = true;
        }
        if (option === "register") {
            this.selectedTabRegister = true;
        }
        if (option === "sigin") {
            this.selectedTabSignIn = true;
        }
        if (option === "checkout") {
            this.selectedTabCheckOut = true;
        }
    }
    showFundDetail() {
        this.selectedFunDetail = true;
    }
    limitedCharacter() {
        var input = document.querySelector<HTMLInputElement>('.input-content-donation').value;
        var textLimited = document.querySelector<HTMLElement>('.note-input');
        console.log(input);
        textLimited.textContent =  `${256 - input.length} character(s) left`
    }
}
