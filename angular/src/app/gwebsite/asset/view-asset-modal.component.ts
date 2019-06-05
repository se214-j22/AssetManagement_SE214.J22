import { AssetForViewDto, AssetGroupServiceProxy, LiquidationServiceProxy, LiquidationForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { AssetServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewAssetModal',
    templateUrl: './view-asset-modal.component.html'
})

export class ViewAssetModalComponent extends AppComponentBase {

    assetGroupName: string = "";
    asset: AssetForViewDto = new AssetForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;
    liquidation: LiquidationForViewDto;

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
            console.log(this);
        })
    }

    close(): void {
        this.liquidation = null;
        this.modal.hide();
    }

    getNameAssetGroup(): void {
        this._assetgroupService.getAssetGroupNameByAssetID(this.asset.assetGrouptId).subscribe(result => {
            if (result != null)
                this.assetGroupName = result;
        });
    }
}