import { AssetForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { AssetServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewAssetModal',
    templateUrl: './view-asset-modal.component.html'
})

export class ViewAssetModalComponent extends AppComponentBase {

    asset : AssetForViewDto = new AssetForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy
    ) {
        super(injector);
    }

    show(assetId?: number | null | undefined): void {
        this._assetService.getAssetForView(assetId).subscribe(result => {
            this.asset = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}