import { CreateOrEditFundRaisingDto, CreateOrEditFundRaisingInputDto, GetInforFileDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
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
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    isLoading;
    text;
    uploadedFiles: any[] = [];
    fileList: File[];
    file: File;
    // input: {
    //     file: FileParameter[],
    //     postTitle: string,
    //     targetIntroduce: string,
    //     postTopic: string,
    //     purpose: string,
    //     note: string,
    // } = {
    //         file: [],
    //         postTitle: null,
    //         targetIntroduce: null,
    //         postTopic: null,
    //         purpose: null,
    //         note: null,
    //     };
    input: CreateOrEditFundRaisingInputDto = new CreateOrEditFundRaisingInputDto();
    selectedCity;
    fundId;
    startDate;
    postId;
    listImage;
    endingDate;
    amountOfMoney;
    listOption = [{
        code: true, name: 'Có'
    }, {
        code: false, name: 'Không'
    }]
    date = moment(new Date());
    saving
    constructor(injector: Injector, private _fundRaiser: FundRaiserServiceProxy) {
        super(injector)
    }
    show(postId?) {
        this.postId = postId;
        this.uploadedFiles = [];
        // if (postId) {
        //     this._fundRaiser.getForEditPost(postId).subscribe(result => {
        //         this.input = result
        //         this.input.file.forEach(image => {
        //             const uint8Array = new Uint8Array(image.size);
        //             this.file = new File([new Blob([uint8Array], { type: 'image/png' })], image.imageUrl.slice(8, image.imageUrl.length));
        //             this.uploadedFiles.push(this.file);
        //         })
        //         console.log(this.uploadedFiles)
        //     })
        // }
        this.modal.show();
        // this.input = {
        //     file: [],
        //     fundName: null,
        //     fundTitle: null,
        //     amountOfMoney: null,
        //     fundStartDate: null,
        //     fundEndDate: null,
        //     fundContent: null,
        //     reasonCreateFund: null,
        //     isPayFee: false,
        // };
    }
    // previewImages(event) {
    //     this.input.file = [];
    //     for (let index = 0; index < event.target.files.length; index++) {
    //         this.input.file.push({ data: event.target.files[index], fileName: event.target.files[index].name })
    //     }
    //     const preview = document.querySelector<HTMLInputElement>('#form-add-image');
    //     const input = document.querySelector<HTMLInputElement>('#images');
    //     var blockImageExisted = document.querySelector<HTMLElement>(".block-image")
    //     if (input.files) {
    //         const blockImage = document.createElement('div');
    //         blockImage.classList.add("block-image");
    //         blockImage.style.width = '100%';
    //         blockImage.style.display = 'flex';
    //         blockImage.style.flexWrap = 'wrap';
    //         blockImage.style.height = '120px';
    //         blockImage.style.overflow = 'auto';
    //         blockImage.style.justifyContent = 'flex-start';
    //         blockImage.style.gap = '10px';
    //         blockImage.style.margin = '20px 0';
    //         blockImage.innerHTML = '';
    //         if (blockImageExisted != null) {
    //             blockImageExisted.innerHTML = '';
    //             blockImageExisted.style.width = '100%';
    //             blockImageExisted.style.display = 'flex';
    //             blockImageExisted.style.flexWrap = 'wrap';
    //             blockImageExisted.style.justifyContent = 'flex-start';
    //             blockImageExisted.style.gap = '10px';
    //             blockImageExisted.style.height = '120px';
    //             blockImageExisted.style.overflow = 'auto';
    //         }
    //         Array.from(event.target.files).forEach(function (file: Blob) {
    //             const reader = new FileReader();
    //             reader.onload = function (e: any) {
    //                 const img = document.createElement('img');
    //                 img.src = e.target.result;
    //                 img.style.width = '25%'; // Adjust the size as needed
    //                 //img.style.height = '60%'; // Adjust the size as needed
    //                 img.style.marginBottom = '10px'; // Adjust the size as needed
    //                 //chưa tồn tại block image
    //                 if (blockImageExisted == null) {
    //                     blockImage.appendChild(img)
    //                     preview.appendChild(blockImage);
    //                 }
    //                 else {
    //                     blockImageExisted.appendChild(img);
    //                 }
    //             };
    //             reader.readAsDataURL(file);
    //         })
    //     };
    // }
    close() {
        this.modal.hide();
    }
    onUpload(event: UploadEvent) {
        // for (let file of event.files) {
        //     var image = new GetInforFileDto();
        //     image.imageUrl = file.name;
        //     image.size = file.size;
        //     this.input.file.push(image);
        //     this.uploadedFiles.push(file);
        // }
    }
    save() {
        this._fundRaiser.createPostOfFundRaising(
            this.input
        ).subscribe(
            () => {
                this.notify.success("Thêm bài đăng quỹ thành công");
                this.modalSave.emit(null);

                this.modal.hide();
            }, (error) => {
                this.notify.error("Đã xảy ra lỗi")
            })
    }
    // onChangeDate(event, type) {
    //     if (type === 'start') {
    //         this.input.fundStartDate = event;
    //     }
    //     else this.input.fundEndDate = event
    // }
}
