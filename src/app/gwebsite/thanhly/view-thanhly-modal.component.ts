import { ThanhLyForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThanhLyServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThanhLyModal',
    templateUrl: './view-thanhly-modal.component.html'
})

export class ViewThanhLyModalComponent extends AppComponentBase {

    thanhly : ThanhLyForViewDto = new ThanhLyForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thanhlyService: ThanhLyServiceProxy
    ) {
        super(injector);
    }

    show(thanhlyId?: number | null | undefined): void {
        this._thanhlyService.getThanhLyForView(thanhlyId).subscribe(result => {
            this.thanhly = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}