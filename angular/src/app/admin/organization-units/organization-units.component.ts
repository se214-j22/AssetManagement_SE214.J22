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
    assetIds: string = '11,12';
    organizationUnitId: number = 9;
    constructor(injector: Injector,
        private organizationUnitService: OrganizationUnitServiceProxy,
    ) {
        super(injector);
    }

    placeAssetsToOrganizationUnit() {
        let assetIds_listString = this.assetIds.split(',');
        let assetIds_listNumber = assetIds_listString.map(assetId => {
            return Number(assetId);
        });
        let input = new AssetsToOrganizationUnitInput(
            {
                assetIds: assetIds_listNumber,
                organizationUnitId: this.organizationUnitId
            });
        this.organizationUnitService.placeAssetsToOrganizationUnit(input).subscribe(result => { 
            this.notify.info(this.l('PlaceSuccessfully'));
        });
    }
}
