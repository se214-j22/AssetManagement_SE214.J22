import { XuatSuDungForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { XuatSuDungServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewXuatSuDungModal',
    templateUrl: './view-xuatsudung-modal.component.html'
})

export class ViewXuatSuDungModalComponent extends AppComponentBase {

    xuatsudung : XuatSuDungForViewDto = new XuatSuDungForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _xuatsudungService: XuatSuDungServiceProxy
    ) {
        super(injector);
    }

    show(xuatsudungId?: number | null | undefined): void {
        this._xuatsudungService.getXuatSuDungForView(xuatsudungId).subscribe(result => {
            this.xuatsudung = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}