import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { UseAssetServiceProxy, UseAssetForViewDto, AssetForViewDto, AssetGroupForViewDto, AssetServiceProxy, AssetGroupServiceProxy, UserServiceProxy, OrganizationUnitServiceProxy, OrganizationUnitDto, UserEditDto } from "@shared/service-proxies/service-proxies";
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

    userUseAsset: UserEditDto = new UserEditDto();
    organizationUnit: OrganizationUnitDto = new OrganizationUnitDto();
    barcode: string = '';

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
        private _userService: UserServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
    ) {
        super(injector);
    }


    show(useassetId?: number | null | undefined): void {
        console.log(this);
        this._useassetService.getUseAssetForView(useassetId).subscribe(result => {
            this.useasset = result;
            this.modal.show();
            this.getUserUseAsset();
            this.getOrganizationUnit();
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
        this._userService.getUserForEdit(this.useasset.userId).subscribe(
            result => {
                this.userUseAsset = result.user;
            }
        )
    }

    getOrganizationUnit() {
        this._organizationUnitService.getOrganizationUnitByID(this.useasset.unitsUsedId).subscribe(
            result => {
                this.organizationUnit = result;
            }
        )
    }
}