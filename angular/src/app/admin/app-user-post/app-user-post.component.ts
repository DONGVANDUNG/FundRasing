import { Component, OnInit, ViewChild } from '@angular/core';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppConsts } from '@shared/AppConsts';
import { AdminFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPostComponent } from '../app-admin-post/create-or-edit-post/create-or-edit-post.component';

@Component({
  selector: 'app-app-user-post',
  templateUrl: './app-user-post.component.html',
  styleUrls: ['./app-user-post.component.less']
})
export class AppUserPostComponent implements OnInit {

  constructor(private _userServiceProxy: UserServiceProxy,
    private _dateTimeService: DateTimeService) { }
  listOption = [{
    code: true, name: 'Có'
  }, {
    code: false, name: 'Không'
  }]
  baseUrl = AppConsts.remoteServiceBaseUrl + '/';
  filter;
  createdDateFilter;
  listFundRaising = [];
  isPayFee;
  createdDate;
  ngOnInit() {
    this.getAllFundRaising();
  }
  getAllFundRaising() {
    this._userServiceProxy.getListPostOfFundRaising().subscribe((result) => {
      this.listFundRaising = result;
    })
  }
  formatDateTime(input, format) {
    return this._dateTimeService.formatDate(input as Date, format);
  }
  onChangeDate(event) {
    this.createdDateFilter = this.formatDateTime(event, 'yyyy-MM-dd');
    this.getAllFundRaising();
  }
  eventEnter(event) {
    if (event.keyCode === 13) {
      this.getAllFundRaising();
    }
  }
  setValueDropDown(event) {
    this.isPayFee = event;
    this.getAllFundRaising();
  }
}
