import { NhomTaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { NhomTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewNhomTaiSanModal',
    templateUrl: './view-nhomtaisan-modal.component.html'
})

export class ViewNhomTaiSanModalComponent extends AppComponentBase {

    nhomTaiSan : NhomTaiSanForViewDto = new NhomTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _nhomTaiSanService: NhomTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(nhomTaiSanId?: number | null | undefined): void {
        this._nhomTaiSanService.getNhomTaiSanForView(nhomTaiSanId).subscribe(result => {
            this.nhomTaiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
