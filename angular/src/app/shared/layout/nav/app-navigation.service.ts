import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {
    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) { }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem('Thông báo', 'Pages.Administration', 'flaticon-alert', '/app/notifications'),
            new AppMenuItem('Người gây quỹ', 'Pages.Administration', 'flaticon-users-1', '/app/admin/fundRaiser'),
            new AppMenuItem('Quản lý giao dịch', 'Pages.Administration', 'fas fa-users-cog', '/app/admin/transaction'),
            new AppMenuItem('Gói quỹ', 'Pages.Administration', 'flaticon-line-graph', '/app/admin/fundPackage'),
            new AppMenuItem('Yêu cầu gây quỹ', 'Pages.Administration', 'flaticon-line-graph', '/app/admin/request-to-fundraiser'),
            new AppMenuItem('Đăng bài gây quỹ', 'Pages.FundRaising', 'flaticon2-analytics-2', '/app/admin/admin-post'),
            new AppMenuItem('Quản lý quỹ người dùng', 'Pages.FundRaising', 'flaticon-line-graph', '/app/admin/fundRaising'),
            new AppMenuItem('Quyên góp từ thiện', 'Pages.UserDonate', 'flaticon-line-graph', '/app/admin/user-post'),
            new AppMenuItem('Tham gia đấu giá', 'Pages.UserDonate', 'flaticon-line-graph', '/app/admin/auction-user'),
            new AppMenuItem('Đăng bài đấu giá', 'Pages.FundRaising', 'flaticon2-analytics-2', '/app/admin/auction-admin'),
            new AppMenuItem('Đăng ký gây quỹ', 'Pages.UserDonate', 'flaticon-refresh', '/app/admin/register-fundraiser'),
            new AppMenuItem('Đăng ký tài khoản ngân hàng', 'Pages.UserDonate', 'flaticon-refresh', '/app/admin/register-bank'),
            new AppMenuItem('Lịch sử Donate', 'Pages.UserDonate', 'flaticon-line-graph', '/app/admin/checkout'),
            new AppMenuItem('Roles', 'Pages.Administration', 'flaticon-suitcase', '/app/admin/roles'),
            new AppMenuItem('Phân quyền', 'Pages.Administration', 'flaticon-suitcase', '/app/admin/users'),
            new AppMenuItem(
                'DemoUiComponents',
                'Pages.DemoUiComponents',
                'flaticon-shapes',
                '/app/admin/demo-ui-components'
            ),
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {
        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null) {
                if (subMenuItem.route) {
                    return true;
                }
            } else if (this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                return true;
            }

            if (subMenuItem.items && subMenuItem.items.length) {
                let isAnyChildItemActive = this.checkChildMenuItemPermission(subMenuItem);
                if (isAnyChildItemActive) {
                    return true;
                }
            }
        }
        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (
            menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' &&
            this._appSessionService.tenant &&
            !this._appSessionService.tenant.edition
        ) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }

    /**
     * Returns all menu items recursively
     */
    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach((menuItem) => {
            allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });

        return allMenuItems;
    }

    private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
        if (!menuItem.items) {
            return [menuItem];
        }

        let menuItems = [menuItem];
        menuItem.items.forEach((subMenu) => {
            menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
        });

        return menuItems;
    }
}
