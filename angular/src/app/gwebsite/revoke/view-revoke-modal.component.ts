import { RevokeForViewDto, AssetServiceProxy, AssetForViewDto, AssetGroupForViewDto, AssetGroupServiceProxy, UseAssetServiceProxy, UseAssetDto } from './../../../shared/service-proxies/service-proxies';
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
    assetUse: UseAssetDto = new UseAssetDto();
    dateEndDepreciation: string = "";
    remainingOfLiquidation: number;

    constructor(
        injector: Injector,
        private _revokeService: RevokeServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
        private _useAsset: UseAssetServiceProxy,
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
}