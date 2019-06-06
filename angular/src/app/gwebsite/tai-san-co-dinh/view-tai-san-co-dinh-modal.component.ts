import { TaiSanCoDinhForViewDto, TaiSanCoDinhDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { TaiSanCoDinhServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewTaiSanCoDinhModal',
    templateUrl: './view-tai-san-co-dinh-modal.component.html'
})

export class ViewTaiSanCoDinhModalComponent extends AppComponentBase {

    taiSanCoDinh : TaiSanCoDinhDto = new TaiSanCoDinhDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: TaiSanCoDinhServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this._hoaDonNhapService.getTaiSanCoDinhForView(hoaDonNhapId).subscribe(result => {
            this.taiSanCoDinh = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}