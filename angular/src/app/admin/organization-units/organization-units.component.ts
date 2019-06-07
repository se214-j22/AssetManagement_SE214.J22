import { Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { OrganizationTreeComponent } from './organization-tree.component';
import { OrganizationUnitMembersComponent } from './organization-unit-members.component';
import { OrganizationUnitServiceProxy, AssetsToOrganizationUnitInput } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { OrganizationUnitAssetsComponent } from './organization-unit-asset/organization-unit-assets.component';

@Component({
    templateUrl: './organization-units.component.html',
    animations: [appModuleAnimation()]
})
export class OrganizationUnitsComponent extends AppComponentBase {

    @ViewChild('ouMembers') ouMembers: OrganizationUnitMembersComponent;
    @ViewChild('ouAssets') ouAssets: OrganizationUnitAssetsComponent;
    @ViewChild('ouTree') ouTree: OrganizationTreeComponent;
    constructor(injector: Injector,
    ) {
        super(injector);
    }

}
