import { ThuHoiForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThuHoiServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThuHoiModal',
    templateUrl: './view-thuhoi-modal.component.html'
})

export class ViewThuHoiModalComponent extends AppComponentBase {

    thuHoi : ThuHoiForViewDto = new ThuHoiForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thuHoiService: ThuHoiServiceProxy
    ) {
        super(injector);
    }

    show(thuHoiId?: number | null | undefined): void {
        this._thuHoiService.getThuHoiForView(thuHoiId).subscribe(result => {
            this.thuHoi = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
