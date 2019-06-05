import { DieuChuyenForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { DieuChuyenServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewDieuChuyenModal',
    templateUrl: './view-dieuchuyen-modal.component.html'
})

export class ViewDieuChuyenModalComponent extends AppComponentBase {

    dieuChuyen : DieuChuyenForViewDto = new DieuChuyenForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _dieuChuyenService: DieuChuyenServiceProxy
    ) {
        super(injector);
    }

    show(dieuChuyenId?: number | null | undefined): void {
        this._dieuChuyenService.getDieuChuyenForView(dieuChuyenId).subscribe(result => {
            this.dieuChuyen = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
