import { XuatTaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { XuatTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewXuatTaiSanModal',
    templateUrl: './view-xuattaisan-modal.component.html'
})

export class ViewXuatTaiSanModalComponent extends AppComponentBase {

    xuatTaiSan : XuatTaiSanForViewDto = new XuatTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _xuatTaiSanService: XuatTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(xuatTaiSanId?: number | null | undefined): void {
        this._xuatTaiSanService.getXuatTaiSanForView(xuatTaiSanId).subscribe(result => {
            this.xuatTaiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
