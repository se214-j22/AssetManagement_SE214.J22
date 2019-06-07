import { BienBanThanhLyForViewDto, BienBanThanhLyDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { BienBanThanhLyServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewBienBanThanhLyModal',
    templateUrl: './view-bien-ban-thanh-ly-modal.component.html'
})

export class ViewBienBanThanhLyModalComponent extends AppComponentBase {

    bienBanThanhLy : BienBanThanhLyDto = new BienBanThanhLyDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: BienBanThanhLyServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this._hoaDonNhapService.getBienBanThanhLyForView(hoaDonNhapId).subscribe(result => {
            this.bienBanThanhLy = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}