import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {

    constructor(private _permissionService: PermissionCheckerService) {

    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            // new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            // new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            // new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            // new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Đơn vị sử dụng', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
            new AppMenuItem('Người sử dụng', 'Pages.Administration.Customer', 'flaticon-users', '/app/gwebsite/customer'),
            new AppMenuItem('Quản lý tài sản', '', 'flaticon-analytics', '', [
                new AppMenuItem('Thông tin nhóm tài sản', 'Pages.Administration.AssetGroup', 'flaticon-more-v5', '/app/gwebsite/assetgroup'),
                new AppMenuItem('Thông tin tài sản', 'Pages.Administration.Asset', 'flaticon-more-v5', '/app/gwebsite/asset'),
                new AppMenuItem('Xuất sử dụng', 'Pages.Administration.UseAsset', 'flaticon-more-v5', '/app/gwebsite/useasset'),
                new AppMenuItem('Thu hồi', 'Pages.Administration.Revoke', 'flaticon-more-v5', '/app/gwebsite/revoke'),
                new AppMenuItem('Thanh lý', 'Pages.Administration.Liquidation', 'flaticon-more-v5', '/app/gwebsite/liquidation'),

                // new AppMenuItem('Quản lý tài sản', '', 'flaticon-squares', '', [
                //     new AppMenuItem('Thông tin nhóm tài sản', 'Pages.Administration.AssetGroup', 'flaticon-menu-1', '/app/gwebsite/assetgroup'),
                //     new AppMenuItem('Thông tin tài sản', 'Pages.Administration.Asset', 'flaticon-menu-1', '/app/gwebsite/asset'),
                //     new AppMenuItem('Xuất sử dụng', 'Pages.Administration.UseAsset', 'flaticon-menu-1', '/app/gwebsite/useasset'),
                //     new AppMenuItem('Thu hồi', 'Pages.Administration.Revoke', 'flaticon-menu-1', '/app/gwebsite/revoke'),
                //     new AppMenuItem('Thanh lý', 'Pages.Administration.Liquidation', 'flaticon-menu-1', '/app/gwebsite/liquidation'),

                // ])
            ]),
            // new AppMenuItem('Systems', '', 'flaticon-layers', '', [
            //     // new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
            //     // new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
            //     // new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
            //     new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
            //     // new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
            //     // new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
            //     // new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
            //     // new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
            //     new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
            //     new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            // ]),
            // new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {

        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName && this._permissionService.isGranted(subMenuItem.permissionName)) {
                return true;
            }

            if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            } else if (!subMenuItem.permissionName) {
                return true;
            }
        }

        return false;
    }
}
