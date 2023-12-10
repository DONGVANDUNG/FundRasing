import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetInforFileDto, ThemeSettingsDto } from '@shared/service-proxies/service-proxies';
import { TranslationKeys } from 'primeng/api';

@Component({
    selector: 'p-upload-auction',
    templateUrl: './upload-image.component.html',
    styleUrls: ['./upload-image.component.less']
})
export class UploadImageAuctionComponent extends AppComponentBase implements OnInit {
    @Output() onSelect: EventEmitter<any> = new EventEmitter<any>();
    baseUrl = AppConsts.remoteServiceBaseUrl + '/uploads/';
    listImage = [];
    files = [];
    inputValue;
    postNumber = []
    @Input() set inforImage(value) {
        if (value?.length !== 0 && value !== undefined && this.inputValue == null) {
            this.files = value;
            this.inputValue = 1;
        }
        this.inputValue = null;
    };

    get inforImage() {
        return this.postNumber;
    }
    // @Input() inforImage;
    listFileName = [];
    ngOnInit(): void {
        console.log(this.files);
    }
    onFileSelect(event: any) {
        let filesImage = event.dataTransfer ? event.dataTransfer.files : event.target.files;
        for (let i = 0; i < filesImage.length; i++) {
            var newImage = new GetInforFileDto();
            newImage.imageUrl = filesImage[i].name;
            newImage.size = filesImage[i].size;
            this.files?.push({ imageUrl: filesImage[i].name, size: filesImage[i].size });
        };
        this.onSelect.emit(this.files);
    }
    clear() {
        this.files = [];
    }
    remove(event: Event, index: number,id) {
        this.files.splice(index, 1);
    }
    formatSize(bytes: number, decimalPlaces: number = 2) {
        if (bytes === 0) return '0 Bytes';

        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

        const i = Math.floor(Math.log(bytes) / Math.log(k));

        return parseFloat((bytes / Math.pow(k, i)).toFixed(decimalPlaces)) + ' ' + sizes[i];
    }
}
