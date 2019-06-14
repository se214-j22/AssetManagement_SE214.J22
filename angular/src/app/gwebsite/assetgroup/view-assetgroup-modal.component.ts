import { AssetGroupForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { AssetGroupServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewAssetGroupModal',
    templateUrl: './view-assetgroup-modal.component.html'
})

export class ViewAssetGroupModalComponent extends AppComponentBase {

    assetGroupName: string = '';
    assetgroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _assetgroupService: AssetGroupServiceProxy
    ) {
        super(injector);
    }

    show(assetgroupId?: number | null | undefined): void {
        this._assetgroupService.getAssetGroupForView(assetgroupId).subscribe(result => {
            this.assetgroup = result;
            this.modal.show();
            this.getNameAssetGroupParent();
        })
    }

    close(): void {
        this.modal.hide();
    }

    getNameAssetGroupParent(): void {
        this._assetgroupService.getAssetGroupNameByAssetID(this.assetgroup.assetGroupParentId).subscribe(result => {
            if(result != null)
            this.assetGroupName = result;
        });
    }
}