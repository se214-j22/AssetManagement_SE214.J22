import { LoaiTaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { LoaiTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewLoaiTaiSanModal',
    templateUrl: './view-loai-tai-san-modal.component.html'
})

export class ViewLoaiTaiSanModalComponent extends AppComponentBase {

    loaiTaiSan : LoaiTaiSanForViewDto = new LoaiTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _loaiTaiSanService: LoaiTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(loaiTaiSanId?: number | null | undefined): void {
        this._loaiTaiSanService.getLoaiTaiSanForView(loaiTaiSanId).subscribe(result => {
            this.loaiTaiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}