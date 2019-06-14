import { SuaChuaForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { SuaChuaServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewSuaChuaModal',
    templateUrl: './view-suachua-modal.component.html'
})

export class ViewSuaChuaModalComponent extends AppComponentBase {

    suaChua : SuaChuaForViewDto = new SuaChuaForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _suaChuaService: SuaChuaServiceProxy
    ) {
        super(injector);
    }

    show(suaChuaId?: number | null | undefined): void {
        this._suaChuaService.getSuaChuaForView(suaChuaId).subscribe(result => {
            this.suaChua = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
