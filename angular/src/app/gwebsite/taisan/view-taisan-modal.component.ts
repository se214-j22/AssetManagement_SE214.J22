import { TaiSanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { TaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewTaiSanModal',
    templateUrl: './view-taisan-modal.component.html'
})

export class ViewTaiSanModalComponent extends AppComponentBase {

    taiSan : TaiSanForViewDto = new TaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _taiSanService: TaiSanServiceProxy
    ) {
        super(injector);
    }

    show(taiSanId?: number | null | undefined): void {
        this._taiSanService.getTaiSanForView(taiSanId).subscribe(result => {
            this.taiSan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
