import { PhongBanForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { PhongBanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewPhongBanModal',
    templateUrl: './view-phong-ban-modal.component.html'
})

export class ViewPhongBanModalComponent extends AppComponentBase {

    phongBan : PhongBanForViewDto = new PhongBanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _phongBanService: PhongBanServiceProxy
    ) {
        super(injector);
    }

    show(phongBanId?: number | null | undefined): void {
        this._phongBanService.getPhongBanForView(phongBanId).subscribe(result => {
            this.phongBan = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}