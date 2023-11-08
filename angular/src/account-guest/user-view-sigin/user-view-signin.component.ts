import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-view-sigin',
  templateUrl: './user-view-signin.component.html',
  styleUrls: ['./user-view-signin.component.less']
})
export class UserViewSiginComponent implements OnInit {

  constructor(private route:Router) { }

  ngOnInit() {
  }
  redirectHome(){
    this.route.navigateByUrl('guest/home')
  }
}
