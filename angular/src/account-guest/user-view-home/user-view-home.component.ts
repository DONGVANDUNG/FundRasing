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

    raisingArray = ['1','2','3','4','5'] // ARRAY NÀY BAO GỒM ẢNH, TITLE , CONTENT CỦA 1 DỰ ÁN ĐANG GÂY QUỸ, CÓ THỂ LÀ 1 RECORD


    constructor(injector: Injector, private router: Router) {
        super(injector);
        this.isLoading = true;
        setTimeout(() => {
            this.isLoading = false;
        }
            , 1000)
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
    selectTab() {
        const menuNav = document.querySelector<HTMLElement>(".list-nav");
        const listNav = document.querySelectorAll<HTMLElement>('.item-nav');

        menuNav.addEventListener("click", (event) => {
            const clickedItem = event.target as HTMLElement;
            if (clickedItem && clickedItem.classList.contains('item-nav')) {
                listNav.forEach(nav => nav.classList.remove("active"));
                clickedItem.classList.add("active");
            }
        });
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
    changeBanner(index){
        var banner = document.querySelectorAll('.circle-img')
        var backgroundBanner = document.querySelector('.home-banner')
        var bannerTitle = document.querySelector<HTMLElement>('.banner-title')
        var bannerContent = document.querySelector<HTMLElement>('.banner-content')

        var bannerClasses = ['banner1', 'banner2', 'banner3'];
         backgroundBanner.classList.remove(...bannerClasses);

         if (index >= 1 && index <= bannerClasses.length) {
            backgroundBanner.classList.add(bannerClasses[index - 1]);
        }

        var circleImgs = document.querySelectorAll<HTMLElement>('.circle-img');
        circleImgs.forEach((circleImg, i) => {
            circleImg.style.backgroundColor = i + 1 === index ? '#F9153E' : 'grey';
        });

        bannerTitle.classList.add('zoomOutAnimation');
        bannerContent.classList.add('zoomOutAnimation');
       
        setTimeout(() => {
            bannerTitle.classList.remove('zoomOutAnimation');
            bannerContent.classList.remove('zoomOutAnimation');
        }, 1000);
    }

    goToFundraisingDetail(id): void{
        this.router.navigate(['/guest/fund-raising-detail', id]);
    }
}
