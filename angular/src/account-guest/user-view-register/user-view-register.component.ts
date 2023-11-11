import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-view-register',
  templateUrl: './user-view-register.component.html',
  styleUrls: ['./user-view-register.component.less']
})
export class UserViewRegisterComponent implements OnInit {

  constructor(private route:Router) { }

  ngOnInit() {
  }
  redirectHome(){
    this.route.navigateByUrl('guest/home')
  }
}