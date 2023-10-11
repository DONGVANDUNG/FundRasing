import { Component, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-app-user-home',
  templateUrl: './app-user-home.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./app-user-home.component.less']
})
export class AppUserHomeComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
