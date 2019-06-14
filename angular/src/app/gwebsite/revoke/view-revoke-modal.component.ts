import { RevokeForViewDto, AssetServiceProxy, AssetForViewDto, AssetGroupForViewDto, AssetGroupServiceProxy, UseAssetServiceProxy, UseAssetDto, UserServiceProxy, UserEditDto, OrganizationUnitDto, OrganizationUnitServiceProxy, UseAssetForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { RevokeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

@Component({
    selector: 'viewRevokeModal',
    templateUrl: './view-revoke-modal.component.html'
})

export class ViewRevokeModalComponent extends AppComponentBase {

    revoke : RevokeForViewDto = new RevokeForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    asset: AssetForViewDto = new AssetForViewDto();
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    dateEndDepreciation: string = "";
    remainingOfLiquidation: number;
    useAsset: UseAssetForViewDto = new UseAssetForViewDto();
    userUseAsset: UserEditDto = new UserEditDto();
    organizationUnit: OrganizationUnitDto = new OrganizationUnitDto();
    
    constructor(
        injector: Injector,
        private _revokeService: RevokeServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
        private _useAsset: UseAssetServiceProxy,
        private _userService: UserServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
    ) {
        super(injector);
    }

    show(revokeId?: number | null | undefined): void {
        this._revokeService.getRevokeForView(revokeId).subscribe(result => {
            this.revoke = result;
            if (this.revoke.assetId != "") {
                this.getAssetByAssetID();
            }
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }

    getAssetByAssetID() {

        this._assetService.getAssetByAssetID(this.revoke.assetId).subscribe(result => {
            this.asset = result;
            if (this.asset.assetGrouptId != "") {
                this.getAssetGroupByAssetID();
                let date = new Date(this.asset.dateAdded);
                date.setMonth(date.getMonth() + this.asset.monthOfDepreciation);
                this.dateEndDepreciation = moment(date).format('YYYY-MM-DD');
            }
            this.calculateRemaining();
            this._useAsset.getUseAssetByAssetID(this.asset.assetId).subscribe(result => {
                this.useAsset = result;
                
            });
        });

        ///TODO: Viết code get ai đang sử dụng lên
    }

    getAssetGroupByAssetID() {
        this._assetGroupService.getAssetGroupByAssetID(this.asset.assetGrouptId).subscribe(result => {
            this.assetGroup = result;
        });
    }

    calculateRemaining() : number {
        let dateNow = moment();
        let dateAdd = moment(this.asset.dateAdded);
        console.log(dateNow);
        let diffDate : number = dateNow.diff(dateAdd, "months");
        this.remainingOfLiquidation = (this.asset.depreciationValue/this.asset.monthOfDepreciation)*(this.asset.monthOfDepreciation - diffDate);
        console.log(this.remainingOfLiquidation);
        console.log(diffDate);
        console.log(this.asset);
        return this.remainingOfLiquidation;
    }

    getUserUseAsset() {
        this._userService.getUserForEdit(this.useAsset.userId).subscribe(
            result => {
                this.userUseAsset = result.user;
            }
        )
    }

    getOrganizationUnit() {
        this._organizationUnitService.getOrganizationUnitByID(this.useAsset.unitsUsedId).subscribe(
            result => {
                this.organizationUnit = result;
            }
        )
    }

}
