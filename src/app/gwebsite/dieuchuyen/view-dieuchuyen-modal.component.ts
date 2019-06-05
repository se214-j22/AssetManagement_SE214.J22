import { DieuChuyenForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { DieuChuyenServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewDieuChuyenModal',
    templateUrl: './view-dieuchuyen-modal.component.html'
})

export class ViewDieuChuyenModalComponent extends AppComponentBase {

    dieuchuyen : DieuChuyenForViewDto = new DieuChuyenForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _dieuchuyenService: DieuChuyenServiceProxy
    ) {
        super(injector);
    }

    show(dieuchuyenId?: number | null | undefined): void {
        this._dieuchuyenService.getDieuChuyenForView(dieuchuyenId).subscribe(result => {
            this.dieuchuyen = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}