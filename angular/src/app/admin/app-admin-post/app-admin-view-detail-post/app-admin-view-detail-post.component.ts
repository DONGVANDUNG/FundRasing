import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FileParameter, FundRaiserServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import * as moment from 'moment';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-app-admin-view-detail-post',
    templateUrl: './app-admin-view-detail-post.component.html',
    styleUrls: ['./app-admin-view-detail-post.component.less']
})
export class AppAdminViewDetailPostComponent extends AppComponentBase {

    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    //@Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    inforFundDetail;
    isLoading;
    responsiveOptions
    saving

    constructor(injector: Injector,
        private _fundRaiser: FundRaiserServiceProxy,
        private _userServiceProxy: UserServiceProxy) {
        super(injector)
        this.responsiveOptions = [
            {
                breakpoint: '1199px',
                numVisible: 1,
                numScroll: 1
            },
            {
                breakpoint: '991px',
                numVisible: 2,
                numScroll: 1
            },
            {
                breakpoint: '767px',
                numVisible: 1,
                numScroll: 1
            }
        ];
    }
    show(fundId) {
        this._userServiceProxy.getInforFundRaisingById(fundId).subscribe(result => {
            this.inforFundDetail = result;
        })
        this.modal.show();
    }
    close() {
        this.modal.hide();
    }
}
