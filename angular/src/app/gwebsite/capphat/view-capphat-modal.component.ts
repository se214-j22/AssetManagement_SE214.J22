import { CapPhatForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { CapPhatServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewCapPhatModal',
    templateUrl: './view-capphat-modal.component.html'
})

export class ViewCapPhatModalComponent extends AppComponentBase {

    capPhat : CapPhatForViewDto = new CapPhatForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _capPhatService: CapPhatServiceProxy
    ) {
        super(injector);
    }

    show(capPhatId?: number | null | undefined): void {
        this._capPhatService.getCapPhatForView(capPhatId).subscribe(result => {
            this.capPhat = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}
