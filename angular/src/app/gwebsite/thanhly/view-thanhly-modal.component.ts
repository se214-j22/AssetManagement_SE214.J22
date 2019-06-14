import { ThanhLyForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThanhLyServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThanhLyModal',
    templateUrl: './view-thanhly-modal.component.html'
})

export class ViewThanhLyModalComponent extends AppComponentBase {

    thanhLy : ThanhLyForViewDto = new ThanhLyForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thanhLyService: ThanhLyServiceProxy
    ) {
        super(injector);
    }

    show(thanhLyId?: number | null | undefined): void {
        this._thanhLyService.getThanhLyForView(thanhLyId).subscribe(result => {
            this.thanhLy = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
