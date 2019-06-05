import { NhomTaiSanForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { NhomTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewNhomTaiSanModal',
    templateUrl: './view-nhomtaisan-modal.component.html'
})

export class ViewNhomTaiSanModalComponent extends AppComponentBase {

    nhomtaisan : NhomTaiSanForViewDto = new NhomTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _nhomtaisanService: NhomTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(nhomtaisanId?: number | null | undefined): void {
        this._nhomtaisanService.getNhomTaiSanForView(nhomtaisanId).subscribe(result => {
            this.nhomtaisan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}