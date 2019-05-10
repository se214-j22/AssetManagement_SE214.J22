import { LiquidationForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { LiquidationServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewLiquidationModal',
    templateUrl: './view-liquidation-modal.component.html'
})

export class ViewLiquidationModalComponent extends AppComponentBase {

    liquidation : LiquidationForViewDto = new LiquidationForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _liquidationService: LiquidationServiceProxy
    ) {
        super(injector);
    }

    show(liquidationId?: number | null | undefined): void {
        this._liquidationService.getLiquidationForView(liquidationId).subscribe(result => {
            this.liquidation = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}