import { DonViCungCapTaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { DonViCungCapTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewDonViCungCapTaiSanModal',
    templateUrl: './view-don-vi-cung-cap-tai-san-modal.component.html'
})

export class ViewDonViCungCapTaiSanModalComponent extends AppComponentBase {

    donViCungCapTaiSan : DonViCungCapTaiSanForViewDto = new DonViCungCapTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _donViCungCapTaiSanService: DonViCungCapTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(donViCungCapTaiSanId?: number | null | undefined): void {
        this._donViCungCapTaiSanService.getDonViCungCapTaiSanForView(donViCungCapTaiSanId).subscribe(result => {
            this.donViCungCapTaiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}