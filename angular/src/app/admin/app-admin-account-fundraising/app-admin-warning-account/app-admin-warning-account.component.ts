import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-admin-warning-account',
  templateUrl: './app-admin-warning-account.component.html',
  styleUrls: ['./app-admin-warning-account.component.less']
})
export class AppAdminWarningAccountComponent extends AppComponentBase implements OnInit {

 
  @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  listUser = [];
  columnDefs = [];
  defaultColDef
  rowDataRepreson = [];
  active: boolean = false;
  saving: boolean = false;
  //inputFundPackage: CreateOrEditFundPackageDto = new CreateOrEditFundPackageDto();
  response: any;
  constructor(
      injector: Injector,
      //private _adminServiceProxy: AdminFundRaisingServiceProxy,
  ) {
      super(injector);
  }

  ngOnInit() {

  }

  show(fundPackageId?: number): void {
      // if (!fundPackageId) {
      //     this.inputFundPackage = new CreateOrEditFundPackageDto();
      //     this.inputFundPackage.id = fundPackageId;
      //     this.active = true;
      //     this.modal.show();
      // } else {
      //     this._adminServiceProxy.getForEditFundPackage(fundPackageId)
      //         .subscribe((result) => {
      //             this.inputFundPackage = result;
      //             this.active = true;
      //             this.modal.show();
      //         });
      // }
  }
  save(){
      this.active = true;
      // this._adminServiceProxy.createOrEditFundPackage(this.inputFundPackage)
      //   .pipe(finalize(() => this.saving = false))
      //   .subscribe(() => {
      //     this.notify.info(this.l("SavedSuccessfully"));
      //     this.close();
      //     this.modalSave.emit(null);
      //     this.saving = false;
      // })
    }
  close(): void {
      this.active = false;
      this.modal.hide();
  }

}
