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

            new AppMenuItem('Group 7', '', 'flaticon-squares', '', [
                new AppMenuItem('QuanLyTaiSan', '', 'flaticon-interface-8', '', [  
                    new AppMenuItem('NhomTaiSan', 'Pages.Administration.NhomTaiSan', 'flaticon-menu-1', '/app/gwebsite/nhomtaisan'),              
                    new AppMenuItem('TaiSan', 'Pages.Administration.TaiSan', 'flaticon-menu-1', '/app/gwebsite/taisan'),                
                    new AppMenuItem('XuatTaiSan', 'Pages.Administration.XuatTaiSan', 'flaticon-menu-1', '/app/gwebsite/xuattaisan'),
                    new AppMenuItem('DieuChuyen', 'Pages.Administration.DieuChuyen', 'flaticon-menu-1', '/app/gwebsite/dieuchuyen'),
                    new AppMenuItem('ThuHoi', 'Pages.Administration.ThuHoi', 'flaticon-menu-1', '/app/gwebsite/thuhoi')
                ]),
                new AppMenuItem('QuanLyDonVi', '', 'flaticon-interface-8', '', [                
                    new AppMenuItem('DonVi', 'Pages.Administration.DonVi', 'flaticon-menu-1', '/app/gwebsite/donvi'),
                    new AppMenuItem('NhanVien', 'Pages.Administration.NhanVien', 'flaticon-menu-1', '/app/gwebsite/nhanvien')
                ]),
            ]),

            new AppMenuItem('Group 9', '', 'flaticon-interface-8', '', [
                new AppMenuItem('MenuClient', 'Pages.Administration.MenuClient', 'flaticon-menu-1', '/app/gwebsite/menu-client'),
                new AppMenuItem('DemoModel', 'Pages.Administration.DemoModel', 'flaticon-menu-1', '/app/gwebsite/demo-model'),
                new AppMenuItem('Customer', 'Pages.Administration.Customer', 'flaticon-menu-1', '/app/gwebsite/customer'),

                new AppMenuItem('Quản lý tài sản', '', 'flaticon-squares', '', [
                    new AppMenuItem('Nhóm tài sản', 'Pages.Administration.AssetGroup', 'flaticon-menu-1', '/app/gwebsite/assetgroup'),
                    new AppMenuItem('Tài sản', 'Pages.Administration.Asset', 'flaticon-menu-1', '/app/gwebsite/asset'),
                    // new AppMenuItem('Sử dụng tài sản', 'Pages.Administration.UseAsset', 'flaticon-menu-1', '/app/gwebsite/useasset'),
                    // new AppMenuItem('Điều chuyển tài sản', 'Pages.Administration.Transfer', 'flaticon-menu-1', '/app/gwebsite/transfer'),
                    new AppMenuItem('Thu hồi tài sản', 'Pages.Administration.Revoke', 'flaticon-menu-1', '/app/gwebsite/revoke'),
                    // new AppMenuItem('Sửa chữa tài sản', 'Pages.Administration.Repair', 'flaticon-menu-1', '/app/gwebsite/repair'),
                    new AppMenuItem('Thanh lý tài sản', 'Pages.Administration.Liquidation', 'flaticon-menu-1', '/app/gwebsite/liquidation'),

                ])
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
