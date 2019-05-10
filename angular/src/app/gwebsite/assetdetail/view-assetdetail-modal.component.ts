import { AssetDetailForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { AssetDetailServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewAssetDetailModal',
    templateUrl: './view-assetdetail-modal.component.html'
})

export class ViewAssetDetailModalComponent extends AppComponentBase {

    assetdetail : AssetDetailForViewDto = new AssetDetailForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _assetdetailService: AssetDetailServiceProxy
    ) {
        super(injector);
    }

    show(assetdetailId?: number | null | undefined): void {
        this._assetdetailService.getAssetDetailForView(assetdetailId).subscribe(result => {
            this.assetdetail = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}