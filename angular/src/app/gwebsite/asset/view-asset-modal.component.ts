import { AssetForViewDto, AssetGroupServiceProxy, LiquidationServiceProxy, LiquidationForViewDto, AssetGroupDto, AssetGroupForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { AssetServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

@Component({
    selector: 'viewAssetModal',
    templateUrl: './view-asset-modal.component.html'
})

export class ViewAssetModalComponent extends AppComponentBase {

    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    asset: AssetForViewDto = new AssetForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;
    liquidation: LiquidationForViewDto;
    remainingOfLiquidation: number;
    remainingOfMonth: number;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetgroupService: AssetGroupServiceProxy,
        private _liquidationService: LiquidationServiceProxy,
    ) {
        super(injector);
    }

    show(assetId?: number | null | undefined): void {
        this._assetService.getAssetForView(assetId).subscribe(result => {
            this.asset = result;
            if (this.asset.status == 3) {
                this._liquidationService.getLiquidationByAssetID(this.asset.assetId).subscribe(
                    result => {
                        this.liquidation = result;
                    }
                );
            }
            this.modal.show();
            this.getNameAssetGroup();
            this.calculateRemaining();
            console.log(this);
        })
    }

    close(): void {
        this.liquidation = null;
        this.modal.hide();
    }

    getNameAssetGroup(): void {
        this._assetgroupService.getAssetGroupByAssetID(this.asset.assetGrouptId).subscribe(result => {
            if (result != null)
                this.assetGroup = result;
        });
    }

    calculateRemaining() : number {
        let dateNow = moment();
        let dateAdd = moment(this.asset.dateAdded);
        console.log(dateNow);
        this.remainingOfMonth = dateNow.diff(dateAdd, "months");
        this.remainingOfLiquidation = (this.asset.depreciationValue/this.asset.monthOfDepreciation)*(this.asset.monthOfDepreciation - this.remainingOfMonth);
        console.log(this.remainingOfLiquidation);
        console.log(this.remainingOfMonth);
        console.log(this.asset);
        return this.remainingOfLiquidation;
    }
}