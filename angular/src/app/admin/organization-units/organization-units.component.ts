import { Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { OrganizationTreeComponent } from './organization-tree.component';
import { OrganizationUnitMembersComponent } from './organization-unit-members.component';
import { OrganizationUnitServiceProxy, ProductsToOrganizationUnitInput } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';

@Component({
    templateUrl: './organization-units.component.html',
    animations: [appModuleAnimation()]
})
export class OrganizationUnitsComponent extends AppComponentBase {

    @ViewChild('ouMembers') ouMembers: OrganizationUnitMembersComponent;
    @ViewChild('ouTree') ouTree: OrganizationTreeComponent;
    productIds: string = '11,12';
    organizationUnitId: number = 9;
    constructor(injector: Injector,
        private organizationUnitService: OrganizationUnitServiceProxy,
    ) {
        super(injector);
    }

    placeProductsToOrganizationUnit() {
        let productIds_listString = this.productIds.split(',');
        let productIds_listNumber = productIds_listString.map(productId => {
            return Number(productId);
        });
        let input = new ProductsToOrganizationUnitInput(
            {
                productIds: productIds_listNumber,
                organizationUnitId: this.organizationUnitId
            });
        this.organizationUnitService.placeProductToOrganizationUnit(input).subscribe(result => { 
            this.notify.info(this.l('PlaceSuccessfully'));
        });
    }
}
