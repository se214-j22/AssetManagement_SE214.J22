import { SuaChuaForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { SuaChuaServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewSuaChuaModal',
    templateUrl: './view-suachua-modal.component.html'
})

export class ViewSuaChuaModalComponent extends AppComponentBase {

    suachua : SuaChuaForViewDto = new SuaChuaForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _suachuaService: SuaChuaServiceProxy
    ) {
        super(injector);
    }

    show(suachuaId?: number | null | undefined): void {
        this._suachuaService.getSuaChuaForView(suachuaId).subscribe(result => {
            this.suachua = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}