import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-app-user-aution',
    templateUrl: './app-user-aution.component.html',
    styleUrls: ['./app-user-aution.component.less']
})
export class AppUserAutionComponent extends AppComponentBase implements OnInit {

    constructor(injector: Injector, private _userServiceProxy: UserServiceProxy) {
        super(injector);
    }

    ngOnInit() {
    }

}
