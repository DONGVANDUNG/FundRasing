import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-all-fund',
  templateUrl: './view-all-fund.component.html',
  styleUrls: ['./view-all-fund.component.less']
})
export class ViewAllFundComponent implements OnInit {

  constructor(private route:Router) { }

  ngOnInit() {
  }
  showFundDetail(){
    this.route.navigateByUrl('/donate')
  }

}
