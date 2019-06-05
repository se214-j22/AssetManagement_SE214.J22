import { DonViForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { DonViServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewDonViModal',
    templateUrl: './view-donvi-modal.component.html'
})

export class ViewDonViModalComponent extends AppComponentBase {

    donVi : DonViForViewDto = new DonViForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _donViService: DonViServiceProxy
    ) {
        super(injector);
    }

    show(donViId?: number | null | undefined): void {
        this._donViService.getDonViForView(donViId).subscribe(result => {
            this.donVi = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
