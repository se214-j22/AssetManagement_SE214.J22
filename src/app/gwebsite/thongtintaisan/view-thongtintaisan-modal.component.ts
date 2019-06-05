import { ThongTinTaiSanForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThongTinTaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThongTinTaiSanModal',
    templateUrl: './view-thongtintaisan-modal.component.html'
})

export class ViewThongTinTaiSanModalComponent extends AppComponentBase {

    thongtintaisan : ThongTinTaiSanForViewDto = new ThongTinTaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thongtintaisanService: ThongTinTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(thongtintaisanId?: number | null | undefined): void {
        this._thongtintaisanService.getThongTinTaiSanForView(thongtintaisanId).subscribe(result => {
            this.thongtintaisan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}