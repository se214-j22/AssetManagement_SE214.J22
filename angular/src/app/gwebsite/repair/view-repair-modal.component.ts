import { RepairForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { RepairServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewRepairModal',
    templateUrl: './view-repair-modal.component.html'
})

export class ViewRepairModalComponent extends AppComponentBase {

    repair : RepairForViewDto = new RepairForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _repairService: RepairServiceProxy
    ) {
        super(injector);
    }

    show(repairId?: number | null | undefined): void {
        this._repairService.getRepairForView(repairId).subscribe(result => {
            this.repair = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}