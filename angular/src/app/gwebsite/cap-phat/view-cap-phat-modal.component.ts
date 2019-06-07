import { CapPhatForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { CapPhatServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewCapPhatModal',
    templateUrl: './view-cap-phat-modal.component.html'
})

export class ViewCapPhatModalComponent extends AppComponentBase {

    capPhat : CapPhatForViewDto = new CapPhatForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    tenPhong: string;
    tenSanPham: string;

    constructor(
        injector: Injector,
        private _capPhatService: CapPhatServiceProxy
    ) {
        super(injector);
    }

    show(tenPhong: string,tenSanPham: string, capPhatId?: number |null| undefined): void {
        this._capPhatService.getCapPhatForView(capPhatId).subscribe(result => {
            this.capPhat = result;
            this.modal.show();
        })
        this.tenPhong = tenPhong;
        this.tenSanPham=tenSanPham;
    }

    close() : void{
        this.modal.hide();
    }
}