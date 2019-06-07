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
            new AppMenuItem('Quản Lý Tài Sản', '', 'flaticon-interface-8', '', [
                new AppMenuItem('Đơn vị cung cấp tài sản', 'Pages.Administration.DonViCungCapTaiSan', 'flaticon-menu-1', '/app/gwebsite/don-vi-cung-cap-tai-san'),
                new AppMenuItem('Hóa đơn nhập', 'Pages.Administration.HoaDonNhap', 'flaticon-menu-1', '/app/gwebsite/hoa-don-nhap'),
                new AppMenuItem('Phòng ban', 'Pages.Administration.PhongBan', 'flaticon-menu-1', '/app/gwebsite/phong-ban'),
                new AppMenuItem('Biên bản bàn giao tài sản', 'Pages.Administration.BienBanBanGiaoTaiSan', 'flaticon-menu-1', '/app/gwebsite/bien-ban-ban-giao-tai-san'),
                new AppMenuItem('Loại tài sản', 'Pages.Administration.LoaiTaiSan', 'flaticon-menu-1', '/app/gwebsite/loai-tai-san'),
                new AppMenuItem('Bảng yêu cầu cung cấp tài sản', 'Pages.Administration.BangYeuCauCungCapTaiSan', 'flaticon-menu-1', '/app/gwebsite/bang-yeu-cau-cung-cap-tai-san'),
                new AppMenuItem('Phiếu bảo dưỡng', 'Pages.Administration.PhieuBaoDuong', 'flaticon-menu-1', '/app/gwebsite/phieu-bao-duong'),
                new AppMenuItem('Biên bản thanh lý', 'Pages.Administration.BienBanThanhLy', 'flaticon-menu-1', '/app/gwebsite/bien-ban-thanh-ly'),
                new AppMenuItem('Tài sản cố định', 'Pages.Administration.TaiSanCoDinh', 'flaticon-menu-1', '/app/gwebsite/tai-san-co-dinh'),
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
