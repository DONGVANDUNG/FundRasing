import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-app-user-detail-auction',
  templateUrl: './app-user-detail-auction.component.html',
  styleUrls: ['./app-user-detail-auction.component.less']
})
export class AppUserDetailAuctionComponent implements OnInit {
  auctionId
  constructor(private route:ActivatedRoute) { }

  ngOnInit() {
    this.auctionId = this.route.snapshot.params['fundId'];
  }

}
