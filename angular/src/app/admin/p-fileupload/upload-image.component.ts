import { Component, EventEmitter, Output } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'p-upload',
    templateUrl: './upload-image.component.html',
    styleUrls: ['./upload-image.component.less']
})
export class UploadComponent extends AppComponentBase {
    @Output() onSelect: EventEmitter<any> = new EventEmitter<any>();
    baseUrl = AppConsts.remoteServiceBaseUrl + '/uploads/';
    files = [];
    onFileSelect(event: any) {

        let files = event.dataTransfer ? event.dataTransfer.files : event.target.files;
        for (let i = 0; i < files.length; i++) {
            this.files.push(files[i]);
        };
        this.onSelect.emit({ originalEvent: event, files: files, currentFiles: this.files });
    }
    clear() {
        this.files = []
    }
    remove(event: Event, index: number) {
        this.files.splice(index, 1);
    }
}
