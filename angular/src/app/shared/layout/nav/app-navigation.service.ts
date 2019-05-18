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
            new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
                new AppMenuItem('MenuClient', 'Pages.Administration.MenuClient', 'flaticon-menu-1', '/app/gwebsite/menu-client'),
            ]),
            new AppMenuItem('Catalog', '', 'flaticon-interface-5', '', [
                new AppMenuItem('Supplier', 'Pages.Administration.MenuClient', 'flaticon-profile-1', '/app/gwebsite/supplier'),
                new AppMenuItem('Supplier Catalog', 'Pages.Administration.MenuClient', 'flaticon-truck', '/app/gwebsite/supplier-category'),
                new AppMenuItem('Product', 'Pages.Administration.MenuClient', 'flaticon-app', '/app/gwebsite/product'),
                new AppMenuItem('Product Catalog', 'Pages.Administration.MenuClient', 'flaticon-shapes', '/app/gwebsite/product-category')
            ]),
            new AppMenuItem('Plan', '', 'flaticon-calendar', '', [
                new AppMenuItem('Purchase Plan', 'Pages.Administration.MenuClient', 'flaticon-calendar-2', '/app/gwebsite/plan')
            ]),
            new AppMenuItem('Purchase', '', 'flaticon-cart', '', [
                new AppMenuItem('Project', 'Pages.Administration.MenuClient', 'flaticon-imac', '/app/gwebsite/project'),
                new AppMenuItem('Bid Profile', 'Pages.Administration.MenuClient', 'flaticon-book', '/app/gwebsite/bidProfile'),
                new AppMenuItem('Purchase Contract', 'Pages.Administration.MenuClient', 'flaticon-squares-1', '/app/gwebsite/purchaseContract'),
                new AppMenuItem('Purchase Order', 'Pages.Administration.MenuClient', 'flaticon-open-box', '/app/gwebsite/purchaseOrder'),
                new AppMenuItem('Submission', 'Pages.Administration.MenuClient', 'flaticon-exclamation-1', '/app/gwebsite/submission'),
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
