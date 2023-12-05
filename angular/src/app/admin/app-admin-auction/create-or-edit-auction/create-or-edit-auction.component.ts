import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { PaginationParamsModel } from '@app/shared/common/models/base.model';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AdminFundRaisingServiceProxy, FileParameter, FundRaiserServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
    selector: 'app-create-or-edit-auction',
    templateUrl: './create-or-edit-auction.component.html',
    styleUrls: ['./create-or-edit-auction.component.less']
})
export class CreateOrEditAuctionComponent extends AppComponentBase implements OnInit {


    @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    listStatus = [];
    listDepartment = [];
    typeReport = 1;
    paginationParams: PaginationParamsModel;
    totalCounts: number;
    input: {
        file: FileParameter[],
        titleAuction: string,
        itemName: string,
        introduceItem: string,
        amountJumpMin: number,
        amountJumpMax: number,
        startingPrice: number,
        startDate: DateTime
        endDate: DateTime,
        amount:number
    } = {
            file: [],
            titleAuction: null,
            itemName: null,
            introduceItem: null,
            amountJumpMin: null,
            amountJumpMax: null,
            startingPrice: null,
            startDate: null,
            endDate: null,
            amount:null
        };
    listUser = [];
    columnDefs = [];
    defaultColDef
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
        // else {
        //     this._fundRaiser.getForEditFundPackage(auctionId)
        //         .subscribe((result) => {
        //             this.inputAuction = result;
        //             this.active = true;
        //             this.modal.show();
        //         });
        // }
    }
    save() {
        console.log(123)
        this.active = true;
        // this._fundRaiser.createOrEditAuction(
        //     0,
        //     this.input.file,
        //     this.input.titleAuction,
        //     this.input.itemName,
        //     this.input.introduceItem,
        //     this.input.amountJumpMin,
        //     this.input.amountJumpMax,
        //     this.input.startingPrice,
        //     this.input.amount,
        //     this.input.startDate,
        //     this.input.endDate,
        // )
        //     .pipe(finalize(() => this.saving = false))
        //     .subscribe(() => {
        //         this.notify.info(this.l("SavedSuccessfully"));
        //         this.close();
        //         this.modalSave.emit(null);
        //         this.saving = false;
        //     })
    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }
    previewImages(event) {
        this.input.file = [];
        for (let index = 0; index < event.target.files.length; index++) {
            this.input.file.push({ data: event.target.files[index], fileName: event.target.files[index].name })
        }
        const preview = document.querySelector<HTMLElement>('#form-add-image-auction');
        const input = document.querySelector<HTMLInputElement>('#images-auction');
        var blockImageExisted = document.querySelector<HTMLElement>(".block-image-auction")
        if (input.files) {
            const blockImage = document.createElement('div');
            blockImage.classList.add("block-image-auction");
            blockImage.style.width = '100%';
            blockImage.style.display = 'flex';
            blockImage.style.flexWrap = 'wrap';
            blockImage.style.height = '120px';
            blockImage.style.overflow = 'auto';
            blockImage.style.justifyContent = 'flex-start';
            blockImage.style.gap = '10px';
            blockImage.style.margin = '20px 0';
            blockImage.innerHTML = '';
            if (blockImageExisted != null) {
                blockImageExisted.innerHTML = '';
                blockImageExisted.style.width = '100%';
                blockImageExisted.style.display = 'flex';
                blockImageExisted.style.flexWrap = 'wrap';
                blockImageExisted.style.justifyContent = 'flex-start';
                blockImageExisted.style.gap = '10px';
                blockImageExisted.style.height = '120px';
                blockImageExisted.style.overflow = 'auto';
            }
            Array.from(event.target.files).forEach(function (file: Blob) {
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
    onChangeDate(event, type) {
        if (type === 'start') {
            this.input.startDate = event;
        }
        else this.input.endDate = event
    }
}
