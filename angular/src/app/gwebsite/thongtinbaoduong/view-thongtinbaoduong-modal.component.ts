import { ThongTinBaoDuongForViewDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { ThongTinBaoDuongServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThongTinBaoDuongModal',
    templateUrl: './view-thongtinbaoduong-modal.component.html'
})

export class ViewThongTinBaoDuongModalComponent extends AppComponentBase {

    thongtinbaoduong: ThongTinBaoDuongForViewDto = new ThongTinBaoDuongForViewDto();
    @Input() soXe: string;
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thongtinbaoduongService: ThongTinBaoDuongServiceProxy
    ) {
        super(injector);
    }

    show(thongtinbaoduongId?: number | null | undefined): void {
        this._thongtinbaoduongService.getThongTinBaoDuongForView(thongtinbaoduongId).subscribe(result => {
            this.thongtinbaoduong = result;
            this.modal.show();
        })
    }

    close(): void {
        this.modal.hide();
    }
}