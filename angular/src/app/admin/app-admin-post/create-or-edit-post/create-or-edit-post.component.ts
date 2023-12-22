import { CreateOrEditFundRaisingDto, CreateOrEditFundRaisingInputDto, GetInforFileDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { UploadComponent } from '@app/admin/p-fileupload/upload-image.component';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { error } from 'console';
import { DateTime } from 'luxon';
import * as moment from 'moment';
import { ModalDirective } from 'ngx-bootstrap/modal';
interface UploadEvent {
    originalEvent: Event;
    files: File[];
}
@Component({
    selector: 'app-create-or-edit-post',
    templateUrl: './create-or-edit-post.component.html',
    styleUrls: ['./create-or-edit-post.component.less']
})
export class CreateOrEditPostComponent extends AppComponentBase {
    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @ViewChild("uploadImage", { static: true }) modalUpload: UploadComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    isLoading;
    text;
    uploadedFiles = [];
    fileList: File[];
    file: File;
    input: CreateOrEditFundRaisingInputDto = new CreateOrEditFundRaisingInputDto;
    selectedCity;
    fundId;
    startDate;
    postId;
    listFund = [];
    listImage = [];

    endingDate;
    amountOfMoney;
    listTopic = [
        { label: "Trẻ em", value: "Trẻ em" },
        { label: "Giáo dục", value: "Giáo dục" },
        { label: "Hoàn cảnh khó khăn", value: "Hoàn cảnh khó khăn" },
        { label: "Thiên tai", value: "Thiên tai" },
        { label: "Y Tế", value: "Y Tế" },
        { label: "Cộng đồng", value: "Cộng đồng" },
    ];
    listOption = [{
        code: true, name: 'Có'
    }, {
        code: false, name: 'Không'
    }];
    date = moment(new Date());
    saving
    constructor(injector: Injector, private _fundRaiser: FundRaiserServiceProxy) {
        super(injector)
    }
    show(postId?) {
        this.listFund = [];
        this.postId = postId;
        this.uploadedFiles = [];
        this._fundRaiser.getListFundName().subscribe(result => {
            result.forEach(item => {
                this.listFund.push({ label: item.name, value: item.id })
            })
        })
        if (postId) {
            this._fundRaiser.getForEditPost(postId).subscribe(result => {
                this.input = result;
                this.input.file.forEach(image => {
                    image.imageUrl = image.imageUrl.slice(8, image.imageUrl.length)
                })
            })
        }
        else {
            this.input = new CreateOrEditFundRaisingInputDto();
            this.input.file = [];
        }
        this.modal.show();
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
    clearData(){
        this.input.file = [];
        this.input.fundId = undefined;
        this.input.postTopic = undefined;
        this.input.postTitle = undefined;
        this.input.targetIntroduce = undefined;
    }
    close() {
        this.postId = null;
        this.modalUpload.clear();
        this.modal.hide();
    }
    save() {
        if (!this.input.fundId) {
            this.notify.warn("Vui lòng chọn quỹ");
            return;
        }
        if (!this.input.postTopic) {
            this.notify.warn("Vui lòng chủ đề bài đăng");
            return;
        }
        if (!this.input.postTitle) {
            this.notify.warn("Vui lòng tiêu đề bài đăng");
            return;
        }
        if (!this.input.targetIntroduce) {
            this.notify.warn("Vui nhập nội dung bài đăng");
            return;
        }
        // if (!this.input.purpose) {
        //     this.notify.warn("Vui nhập mục đích gây quỹ");
        //     return;
        // }
        if (!this.input.file) {
            this.notify.warn("Vui lòng chọn ảnh cho bài đăng");
            return;
        }
        console.log(this.input);
        if (!this.input.id) {
            this._fundRaiser.createPostOfFundRaising(
                this.input
            ).subscribe(
                () => {
                    this.notify.success("Thêm bài đăng thành công");
                    this.modalSave.emit(null);
                    this.clearData();
                    this.modal.hide();
                }, (error) => {
                    this.notify.error("Đã xảy ra lỗi")
                })
        }
        else {
            this._fundRaiser.updatePostOfFundRaising(
                this.input
            ).subscribe(
                () => {
                    this.notify.success("Thêm bài đăng thành công");
                    this.modalSave.emit(null);

                    this.modal.hide();
                }, (error) => {
                    this.notify.error("Đã xảy ra lỗi")
                })
        }
    }
    // onChangeDate(event, type) {
    //     if (type === 'start') {
    //         this.input.fundStartDate = event;
    //     }
    //     else this.input.fundEndDate = event
    // }
}
