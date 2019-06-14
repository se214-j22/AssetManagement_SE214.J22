import { TransferForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { TransferServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewTransferModal',
    templateUrl: './view-transfer-modal.component.html'
})

export class ViewTransferModalComponent extends AppComponentBase {

    transfer : TransferForViewDto = new TransferForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _transferService: TransferServiceProxy
    ) {
        super(injector);
    }

    show(transferId?: number | null | undefined): void {
        this._transferService.getTransferForView(transferId).subscribe(result => {
            this.transfer = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}