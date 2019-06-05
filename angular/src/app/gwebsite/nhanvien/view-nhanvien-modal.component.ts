import { NhanVienForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { NhanVienServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewNhanVienModal',
    templateUrl: './view-nhanvien-modal.component.html'
})

export class ViewNhanVienModalComponent extends AppComponentBase {

    nhanVien : NhanVienForViewDto = new NhanVienForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _nhanVienService: NhanVienServiceProxy
    ) {
        super(injector);
    }

    show(nhanVienId?: number | null | undefined): void {
        this._nhanVienService.getNhanVienForView(nhanVienId).subscribe(result => {
            this.nhanVien = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
