import { ThuHoiForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThuHoiServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThuHoiModal',
    templateUrl: './view-thuhoi-modal.component.html'
})

export class ViewThuHoiModalComponent extends AppComponentBase {

    thuhoi : ThuHoiForViewDto = new ThuHoiForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thuhoiService: ThuHoiServiceProxy
    ) {
        super(injector);
    }

    show(thuhoiId?: number | null | undefined): void {
        this._thuhoiService.getThuHoiForView(thuhoiId).subscribe(result => {
            this.thuhoi = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}