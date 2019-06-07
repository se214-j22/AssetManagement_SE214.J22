import { PhieuBaoDuongForViewDto, PhieuBaoDuongDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { PhieuBaoDuongServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewPhieuBaoDuongModal',
    templateUrl: './view-phieu-bao-duong-modal.component.html'
})

export class ViewPhieuBaoDuongModalComponent extends AppComponentBase {

    phieuBaoDuong : PhieuBaoDuongDto = new PhieuBaoDuongDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: PhieuBaoDuongServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this._hoaDonNhapService.getPhieuBaoDuongForView(hoaDonNhapId).subscribe(result => {
            this.phieuBaoDuong = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}