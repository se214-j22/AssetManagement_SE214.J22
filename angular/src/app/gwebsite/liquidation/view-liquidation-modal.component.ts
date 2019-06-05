import { LiquidationForViewDto, AssetForViewDto, AssetGroupDto, AssetGroupForViewDto, AssetServiceProxy, AssetGroupServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { LiquidationServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

@Component({
    selector: 'viewLiquidationModal',
    templateUrl: './view-liquidation-modal.component.html'
})

export class ViewLiquidationModalComponent extends AppComponentBase {

    liquidation: LiquidationForViewDto = new LiquidationForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    asset: AssetForViewDto = new AssetForViewDto();
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    dateEndDepreciation: string = "";

    constructor(
        injector: Injector,
        private _liquidationService: LiquidationServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
    ) {
        super(injector);
    }

    show(liquidationId?: number | null | undefined): void {
        this._liquidationService.getLiquidationForView(liquidationId).subscribe(result => {
            this.liquidation = result;
            if (this.liquidation.assetID != "") {
                this.getAssetByAssetID();
            }
            this.modal.show();
        })
    }

    close(): void {
        this.modal.hide();
    }

    getAssetByAssetID() {
        this._assetService.getAssetByAssetID(this.liquidation.assetID).subscribe(result => {
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
}