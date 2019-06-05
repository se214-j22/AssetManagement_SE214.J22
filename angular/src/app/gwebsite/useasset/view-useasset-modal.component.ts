import { UseAssetForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { UseAssetServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewUseAssetModal',
    templateUrl: './view-useasset-modal.component.html'
})

export class ViewUseAssetModalComponent extends AppComponentBase {

    useasset : UseAssetForViewDto = new UseAssetForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy
    ) {
        super(injector);
    }

    show(useassetId?: number | null | undefined): void {
        this._useassetService.getUseAssetForView(useassetId).subscribe(result => {
            this.useasset = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}