import { Component, OnInit, ViewChild } from '@angular/core';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppConsts } from '@shared/AppConsts';
import { AdminFundRaisingServiceProxy, UserFundRaisingServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPostComponent } from '../app-admin-post/create-or-edit-post/create-or-edit-post.component';
import { DataFormatService } from '@app/shared/common/services/data-format.service';
import { DataUtil } from '@metronic/app/kt/_utils';

@Component({
  selector: 'app-app-user-post',
  templateUrl: './app-user-post.component.html',
  styleUrls: ['./app-user-post.component.less']
})
export class AppUserPostComponent implements OnInit {

  constructor(private _userServiceProxy: UserFundRaisingServiceProxy,
    private _dateTimeService: DateTimeService, private dataFormatService: DataFormatService) { }
  listOption = [{
    code: true, name: 'Có'
  }, {
    code: false, name: 'Không'
  }];
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
      this.listFundRaising.forEach((item)=>{
        item.amountDonatePresent = this.dataFormatService.moneyFormat(item.amountDonatePresent);
        item.amountDonateTarget = this.dataFormatService.moneyFormat(item.amountDonateTarget);
      })
    })
  }
  formatDateTime(input, format) {
    return this._dateTimeService.formatDate(input as Date, format);
  }
  // onChangeDate(event) {
  //   this.input.creatimePost = this.formatDateTime(event, 'yyyy-MM-dd');
  //   this.getAllFundRaising();
  // }
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
