import { LiquidationDetailForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { LiquidationDetailServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewLiquidationDetailModal',
    templateUrl: './view-liquidationdetail-modal.component.html'
})

export class ViewLiquidationDetailModalComponent extends AppComponentBase {

    liquidationdetail : LiquidationDetailForViewDto = new LiquidationDetailForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _liquidationdetailService: LiquidationDetailServiceProxy
    ) {
        super(injector);
    }

    show(liquidationdetailId?: number | null | undefined): void {
        this._liquidationdetailService.getLiquidationDetailForView(liquidationdetailId).subscribe(result => {
            this.liquidationdetail = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}