import { HoaDonNhapForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { HoaDonNhapServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewHoaDonNhapModal',
    templateUrl: './view-hoa-don-nhap-modal.component.html'
})

export class ViewHoaDonNhapModalComponent extends AppComponentBase {

    hoaDonNhap : HoaDonNhapForViewDto = new HoaDonNhapForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: HoaDonNhapServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this._hoaDonNhapService.getHoaDonNhapForView(hoaDonNhapId).subscribe(result => {
            this.hoaDonNhap = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}