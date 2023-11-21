import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';
import { AdminFundRaisingServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import * as moment from 'moment';
import { DateTime } from 'luxon';
import { AppAdminViewDetailPostComponent } from './app-admin-view-detail-post/app-admin-view-detail-post.component';

@Component({
    selector: 'app-app-admin-post',
    templateUrl: './app-admin-post.component.html',
    styleUrls: ['./app-admin-post.component.less']
})
export class AppAdminPostComponent implements OnInit {

    @ViewChild("createOrEdit") modalCreate: CreateOrEditPostComponent;
    @ViewChild("viewDetailPost") modalViewDetail: AppAdminViewDetailPostComponent;
    constructor(private _fundRaisingRepo: AdminFundRaisingServiceProxy, private _dateTimeService: DateTimeService) { }
    baseUrl = AppConsts.remoteServiceBaseUrl + '/';
    filter;
    createdDateFilter;
    listFundRaising = [];
    isPayFee;
    createdDate;
    listOption = [{
        code: true, name: 'Có'
    }, {
        code: false, name: 'Không'
    }]
    ngOnInit() {
        this.getAllFundRaising();
    }
    getAllFundRaising() {
        this._fundRaisingRepo.getListFundRaising(
            this.filter,
            this.isPayFee,
            this.createdDateFilter
        ).subscribe((result) => {
            this.listFundRaising = result;
        })
    }
    openModalCreate() {
        this.modalCreate.show();
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
    showDetailPost(id){
        this.modalViewDetail.show(id);
    }
}
