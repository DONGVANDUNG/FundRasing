import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { UploadImageAuctionComponent } from '@app/admin/p-fileupload/p-fileupload.component';
import { PaginationParamsModel } from '@app/shared/common/models/base.model';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, CreateOrEditAuctionInputDto, FundRaiserServiceProxy, GetInforFileDto } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
    selector: 'app-create-or-edit-auction',
    templateUrl: './create-or-edit-auction.component.html',
    styleUrls: ['./create-or-edit-auction.component.less']
})
export class CreateOrEditAuctionComponent extends AppComponentBase implements OnInit {


    @ViewChild("uploadImage", { static: true }) modalUpload: UploadImageAuctionComponent;
    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    listStatus = [];
    listDepartment = [];
    typeReport = 1;
    paginationParams: PaginationParamsModel;
    totalCounts: number;
    input: CreateOrEditAuctionInputDto = new CreateOrEditAuctionInputDto();
    listUser = [];
    columnDefs = [];
    defaultColDef;
    startDate;
    endDate;
    rowDataRepreson = [];
    active: boolean = false;
    saving: boolean = false;
    response: any;
    constructor(
        injector: Injector,
        private _adminServiceProxy: AdminFundRaisingServiceProxy,
        private _fundRaiser: FundRaiserServiceProxy
    ) {
        super(injector);
    }

    ngOnInit() {

    }

    show(auctionId?: number): void {
        if (!auctionId) {
            this.active = true;
            this.modal.show();
        }
        else {
            this._fundRaiser.getForEditAuction(auctionId)
                .subscribe((result) => {
                    this.input = result;
                    this.startDate = result.startDate;
                    this.endDate = result.endDate;
                    this.startDate = new Date(this.startDate);
                    this.endDate = new Date(this.endDate);
                    this.active = true;
                    this.input.file.forEach(image => {
                        image.imageUrl = image.imageUrl.slice(8, image.imageUrl.length)
                    })
                    this.modal.show();
                });
        }
    }
    save() {
        this.active = true;
        if (!this.input.titleAuction) {
            this.notify.warn("Vui nhập tiêu đề bài đăng");
            return;
        }
        if (!this.input.amountJumpMin) {
            this.notify.warn("Vui lòng nhập bước nhảy tối thiểu");
            return;
        }
        if (!this.input.amountJumpMax) {
            this.notify.warn("Vui lòng nhập bước nhảy tối đa");
            return;
        }
        if (!this.input.itemName) {
            this.notify.warn("Vui nhập tên vật phẩm đấu giá");
            return;
        }
        if (!this.startDate) {
            this.notify.warn("Vui nhập ngày bắt đầu đấu giá");
            return;
        }
        if (!this.endDate) {
            this.notify.warn("Vui nhập ngày kết thúc đấu giá");
            return;
        }
        if (!this.input.introduceItem) {
            this.notify.warn("Vui nhập nội dung giới thiệu vật phẩm đấu giá");
            return;
        }
        if (!this.input.file) {
            this.notify.warn("Vui chọn ảnh cho vật phẩm");
            return;
        }
        this.input.startDate = DateTime.fromJSDate(this.startDate);
        this.input.endDate = DateTime.fromJSDate(this.endDate);
        this._fundRaiser.createOrEditItemAuction(
            this.input
        )
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l("SavedSuccessfully"));
                this.close();
                this.modalSave.emit(null);
                this.saving = false;
            })
    }
    close(): void {
        this.active = false;
        this.modal.hide();
        this.modalUpload.clear();
    }
    updateListImage(listImage) {
        console.log(listImage);
        this.input.file = [];
        listImage.forEach(element => {
            var node = new GetInforFileDto();
            node.id = element.id;
            node.imageUrl = element.imageUrl;
            node.size = element.size;
            this.input.file.push(node);
        });

    }
}
