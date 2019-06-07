import { BienBanBanGiaoTaiSanForViewDto, BienBanBanGiaoTaiSanDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { BienBanBanGiaoTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewBienBanBanGiaoTaiSanModal',
    templateUrl: './view-bien-ban-ban-giao-tai-san-modal.component.html'
})

export class ViewBienBanBanGiaoTaiSanModalComponent extends AppComponentBase {

    bienBanBanGiaoTaiSan : BienBanBanGiaoTaiSanDto = new BienBanBanGiaoTaiSanDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: BienBanBanGiaoTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this._hoaDonNhapService.getBienBanBanGiaoTaiSanForView(hoaDonNhapId).subscribe(result => {
            this.bienBanBanGiaoTaiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}