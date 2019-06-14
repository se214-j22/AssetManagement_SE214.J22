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
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),

            new AppMenuItem('Group 6', '', 'flaticon-layers', '', [
                new AppMenuItem('MenuClient', 'Pages.Administration.MenuClient', 'flaticon-menu-1', '/app/gwebsite/menu-client'),
                new AppMenuItem('DemoModel', 'Pages.Administration.DemoModel', 'flaticon-menu-1', '/app/gwebsite/demo-model'),
                new AppMenuItem('Customer', 'Pages.Administration.Customer', 'flaticon-menu-1', '/app/gwebsite/customer'),
                new AppMenuItem('nhacungcap', '', 'flaticon-menu-1', '/app/gwebsite/nhacungcap'),
                new AppMenuItem('Quản lý xe', '', 'flaticon-menu-1', '', [
                    new AppMenuItem('Thông tin xe', '', 'flaticon-menu-1', '/app/gwebsite/thongtinxe'),
                    new AppMenuItem('Vận hành xe', '', 'flaticon-menu-1', '/app/gwebsite/vanhanhxe'),
                    new AppMenuItem('Bảo hiểm xe', '', 'flaticon-menu-1', '/app/gwebsite/thongtinbaohiem'),
                    new AppMenuItem('Phí đường bộ', '', 'flaticon-menu-1', '/app/gwebsite/phiduongbo'),
                    new AppMenuItem('Thông tin bảo dưỡng', '', 'flaticon-menu-1', '/app/gwebsite/thongtinbaoduong'),
                    new AppMenuItem('Thông tin đăng kiểm', '', 'flaticon-menu-1', '/app/gwebsite/thongtindangkiem'),

                    new AppMenuItem('Thông tin sửa chữa', '', 'flaticon-menu-1', '/app/gwebsite/thongTinSuaChua'),
                    new AppMenuItem('Thông tin chi tiết xe', '', 'flaticon-menu-1', '/app/gwebsite/chitietxe'),
                ]),
            ]),
            
            new AppMenuItem('Systems', '', 'flaticon-layers', '', [
                new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
                new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
                new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
                new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ]),
            new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')
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
