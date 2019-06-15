import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { UseAssetServiceProxy, UseAssetForViewDto, AssetForViewDto, AssetGroupForViewDto, AssetServiceProxy, AssetGroupServiceProxy, UserServiceProxy, OrganizationUnitServiceProxy, OrganizationUnitDto, UserEditDto, CustomerServiceProxy, CustomerDto, CustomerForViewDto } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

@Component({
    selector: 'viewUseAssetModal',
    templateUrl: './view-useasset-modal.component.html'
})

export class ViewUseAssetModalComponent extends AppComponentBase {

    useasset: UseAssetForViewDto = new UseAssetForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    asset: AssetForViewDto = new AssetForViewDto();
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    dateEndDepreciation: string = "";

    customer: CustomerForViewDto = new CustomerForViewDto();
    organizationUnit: OrganizationUnitDto = new OrganizationUnitDto();
    organizationName: string = '';
    barcode: string = '';

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
        private _userService: CustomerServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
    ) {
        super(injector);
    }


    show(useassetId?: number | null | undefined): void {
        // console.log(this);
        this._useassetService.getUseAssetForView(useassetId).subscribe(result => {
            this.useasset = result;
            this.modal.show();
            this.getUserUseAsset();
            this.getOrganizationUnits();
            this.getAssetByAssetID();
            this.barcode = this.useasset.dateExport + "-" + this.useasset.assetId + "-" + this.useasset.unitsUsedId + "-" + this.useasset.userId;
        })
    }

    close(): void {
        this.modal.hide();
    }

    getAssetByAssetID() {
        this._assetService.getAssetByAssetID(this.useasset.assetId).subscribe(result => {
            this.asset = result;
            if (this.asset.assetGrouptId != "") {
                this.getAssetGroupByAssetID();
                let date = new Date(this.asset.dateAdded);
                date.setMonth(date.getMonth() + this.asset.monthOfDepreciation);
                this.dateEndDepreciation = moment(date).format('YYYY-MM-DD');
            }
        });
    }

    getAssetGroupByAssetID() {
        this._assetGroupService.getAssetGroupByAssetID(this.asset.assetGrouptId).subscribe(result => {
            this.assetGroup = result;
        });
    }

    getUserUseAsset() {
        // this._userService.getUserForEdit(this.useasset.userId).subscribe(
        //     result => {
        //         this.userUseAsset = result.user;
        //     }
        // )
        this._userService.getCustomerForView(this.useasset.userId).subscribe(result => {
            this.customer = result;
        })
    }

    getOrganizationUnits() {
        this.organizationName = '';
        this._organizationUnitService.getOrganizationUnits().subscribe(
            result => {
                if (result.items.length > 0) {
                    this.organizationName = result.items.filter(item => item.id === this.useasset.unitsUsedId)[0].displayName;
                }
                
                // this.organizationUnit = this.organizationUnits[0];
            }
        )
    }
}