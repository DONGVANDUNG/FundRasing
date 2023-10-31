import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.less']
})
export class UserLoginComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  selectTab() {
    var nav = document.querySelector<HTMLElement>('.list-nav')
    var listNav = document.querySelectorAll<HTMLElement>('.item-nav');
    console.log(nav);
    console.log(listNav);
    listNav.forEach(item => {
      item.onclick = () => {
        nav.classList.remove("active");
        item.classList.add("active");
      }
    })
  }
}
