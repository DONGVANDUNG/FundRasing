import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AccountServiceProxy,
    PasswordComplexitySetting,
    ProfileServiceProxy,
    RegisterOutput,
} from '@shared/service-proxies/service-proxies';
import { ReCaptchaV3Service } from 'ng-recaptcha';
import { finalize } from 'rxjs/operators';
import { LoginService } from '../login/login.service';
import { RegisterModel } from './register.model';

@Component({
    templateUrl: './register.component.html',
    animations: [accountModuleAnimation()],
    styleUrls: ['./register.component.less'],
})
export class RegisterComponent extends AppComponentBase implements OnInit {
    model: RegisterModel = new RegisterModel();
    passwordComplexitySetting: PasswordComplexitySetting = new PasswordComplexitySetting();
    saving = false;
    listOptionAccount = [
        {label:'Quyên góp, Đấu giá',value: 1},
        {label:'Gây quỹ từ thiện',value: 2}
    ]
    constructor(
        injector: Injector,
        private _accountService: AccountServiceProxy,
        private _router: Router,
        private readonly _loginService: LoginService,
        private _profileService: ProfileServiceProxy,
        private _reCaptchaV3Service: ReCaptchaV3Service
    ) {
        super(injector);
    }

    get useCaptcha(): boolean {
        return this.setting.getBoolean('App.UserManagement.UseCaptchaOnRegistration');
    }

    ngOnInit() {
        //Prevent to register new users in the host context
        if (this.appSession.tenant == null) {
            this._router.navigate(['account/login']);
            return;
        }

        this._profileService.getPasswordComplexitySetting().subscribe((result) => {
            this.passwordComplexitySetting = result.setting;
        });
    }

    save(): void {
        var userId;
        let recaptchaCallback = (token: string) => {
            this.saving = true;
            this.model.captchaResponse = token;
            this._accountService
                .register(this.model)
                .pipe(
                    finalize(() => {

                        this.saving = false;
                        this._router.navigate(['account/login']);
                    },
                    )
                )
                .subscribe((result: RegisterOutput) => {
                    userId = result.userId
                    if (!result.canLogin) {
                        // this._accountService.addBasePermisson(result.userId).subscribe(()=>{
                        this.notify.success(this.l('Đăng ký tài khoản thành công'));
                        // })
                    }

                    //Autheticate
                    this.saving = true;
                    this._loginService.authenticateModel.userNameOrEmailAddress = this.model.userName;
                    this._loginService.authenticateModel.password = this.model.password;
                    this._loginService.authenticate(() => {
                        this.saving = false;
                    });
                }, error => {
                    this.notify.error(this.l('Đã xảy ra lỗi khi tạo tài khoản'));
                });
        };

        if (this.useCaptcha) {
            this._reCaptchaV3Service.execute('register').subscribe((token) => recaptchaCallback(token));
        } else {
            recaptchaCallback(null);
        }
    }
}
