import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { PhiDuongBoServiceProxy, PhiDuongBoForViewDTO, ModelForViewDto, ThongTinXeForViewDto, ModelServiceProxy, ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewPhiDuongBoModal',
    templateUrl: './view-phiduongbo-modal.component.html'
})

export class ViewPhiDuongBoModalComponent extends AppComponentBase {
    phiDuongBo: PhiDuongBoForViewDTO = new PhiDuongBoForViewDTO();
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;
    @Input() soXe: string;
    constructor(
        injector: Injector,
        private _phiDuongBoService: PhiDuongBoServiceProxy,
        private _modelService: ModelServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
    ) {
        super(injector);
    }

    show(phiDuongBoId?: number | null | undefined): void {

        this._thongtinxeService.getThongTinXeForView(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForView(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._phiDuongBoService.getPhiDuongBoForView(phiDuongBoId).subscribe(result => {
                this.phiDuongBo = result;
                this.modal.show();
            })
        })

    }

    close(): void {
        this.modal.hide();
    }
}





