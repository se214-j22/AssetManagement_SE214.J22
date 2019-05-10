import { ProviderForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ProviderServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewProviderModal',
    templateUrl: './view-provider-modal.component.html'
})

export class ViewProviderModalComponent extends AppComponentBase {

    provider : ProviderForViewDto = new ProviderForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _providerService: ProviderServiceProxy
    ) {
        super(injector);
    }

    show(providerId?: number | null | undefined): void {
        this._providerService.getProviderForView(providerId).subscribe(result => {
            this.provider = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}