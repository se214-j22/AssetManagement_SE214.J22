import { BangYeuCauCungCapTaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { BangYeuCauCungCapTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewBangYeuCauCungCapTaiSanModal',
    templateUrl: './view-bang-yeu-cau-cung-cap-tai-san-modal.component.html'
})

export class ViewBangYeuCauCungCapTaiSanModalComponent extends AppComponentBase {

    bangYeuCauCungCapTaiSan : BangYeuCauCungCapTaiSanForViewDto = new BangYeuCauCungCapTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    tenPhong: string;

    constructor(
        injector: Injector,
        private _bangYeuCauCungCapTaiSanService: BangYeuCauCungCapTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(tenPhong: string, bangYeuCauCungCapTaiSanId?: number | null | undefined): void {
        this._bangYeuCauCungCapTaiSanService.getBangYeuCauCungCapTaiSanForView(bangYeuCauCungCapTaiSanId).subscribe(result => {
            this.bangYeuCauCungCapTaiSan = result;
            this.modal.show();
        })
        this.tenPhong = tenPhong;
    }

    close() : void{
        this.modal.hide();
    }
}