import { PermissionCheckerService } from 'abp-ng2-module';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AppSessionService } from '@shared/common/session/app-session.service';

@Injectable()
export class AccountRouteGuard implements CanActivate {
    constructor(
        private _permissionChecker: PermissionCheckerService,
        private _router: Router,
        private _sessionService: AppSessionService
    ) {
        console.log(this.selectBestRoute())
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        console.log(this.selectBestRoute())
        if (route.queryParams['ss'] && route.queryParams['ss'] === 'true') {
            return true;
        }

        if (this._sessionService.user) {
            this._router.navigate([this.selectBestRoute()]);
            // console.log(this.selectBestRoute())
            return false;
        }

        return true;
    }

    selectBestRoute(): string {
        return '/app/notifications';
    }
}
