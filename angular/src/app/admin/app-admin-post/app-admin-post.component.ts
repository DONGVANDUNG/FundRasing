import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateOrEditPostComponent } from './create-or-edit-post/create-or-edit-post.component';

@Component({
  selector: 'app-app-admin-post',
  templateUrl: './app-admin-post.component.html',
  styleUrls: ['./app-admin-post.component.less']
})
export class AppAdminPostComponent implements OnInit {

  @ViewChild("createOrEdit") modalCreate : CreateOrEditPostComponent;
  constructor() { }

  ngOnInit() {
  }
  openModalCreate(){
    this.modalCreate.show();
  }
}
