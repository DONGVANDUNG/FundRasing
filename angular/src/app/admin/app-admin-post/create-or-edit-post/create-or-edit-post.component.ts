import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrEditFundRaisingInputDto, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { error } from 'console';
import * as moment from 'moment';
import { ModalDirective } from 'ngx-bootstrap/modal';
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
    input: CreateOrEditFundRaisingInputDto = new CreateOrEditFundRaisingInputDto;
    selectedCity;
    startDate;
    listImage;
    endingDate;
    amountOfMoney;
    listOption = [{
        code: 1, name: 'Có'
    }, {
        code: 2, name: 'Không'
    }]
    date = moment(new Date());
    saving
    constructor(injector: Injector, private _fundRaiser: FundRaiserServiceProxy) {
        super(injector)
    }
    show() {
        this.modal.show();
        this.input = new CreateOrEditFundRaisingInputDto();
    }
    previewImages() {
        const preview = document.querySelector<HTMLInputElement>('#form-add-image');
        const input = document.querySelector<HTMLInputElement>('#images');
        var blockImageExisted = document.querySelector<HTMLElement>(".block-image")
        if (input.files) {
            const blockImage = document.createElement('div');
            blockImage.classList.add("block-image");
            blockImage.style.width = '100%';
            blockImage.style.display = 'flex';
            blockImage.style.flexWrap = 'wrap';
            blockImage.style.height = '120px';
            blockImage.style.overflow = 'auto';
            blockImage.style.justifyContent = 'space-between';
            blockImage.style.margin = '20px 0';
            blockImage.innerHTML = '';
            if (blockImageExisted != null) {
                blockImageExisted.innerHTML = '';
                blockImageExisted.style.width = '100%';
                blockImageExisted.style.display = 'flex';
                blockImageExisted.style.flexWrap = 'wrap';
                blockImageExisted.style.justifyContent = 'space-between';
                blockImageExisted.style.height = '120px';
                blockImageExisted.style.overflow = 'auto';
            }
            Array.from(input.files).forEach(file => {
                const reader = new FileReader();
                reader.onload = function (e: any) {
                    const img = document.createElement('img');
                    img.src = e.target.result;
                    img.style.width = '25%'; // Adjust the size as needed
                    //img.style.height = '60%'; // Adjust the size as needed
                    img.style.marginBottom = '10px'; // Adjust the size as needed
                    //chưa tồn tại block image
                    if (blockImageExisted == null) {
                        blockImage.appendChild(img)
                        preview.appendChild(blockImage);
                    }
                    else {
                        blockImageExisted.appendChild(img);
                    }
                };
                reader.readAsDataURL(file);
            })
        };
    }
    close() {
        this.modal.hide();
    }
    save() {
        this._fundRaiser.createFundRaising(this.input).subscribe(
            () => {
                this.notify.success("Thêm bài đăng quỹ thành công")
            }, (error) => {
                this.notify.error("Đã xảy ra lỗi")
            })
    }
}
