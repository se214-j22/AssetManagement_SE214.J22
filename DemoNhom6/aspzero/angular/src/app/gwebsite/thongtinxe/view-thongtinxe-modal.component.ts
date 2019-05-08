import { ThongTinXeForViewDto, ModelDto, TaiSanForViewDto, ModelForViewDto, TaiSanServiceProxy, ModelServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThongTinXeModal',
    templateUrl: './view-thongtinxe-modal.component.html',
    styleUrls: ['./view-thongtinxe-component.css']
})

export class ViewThongTinXeModalComponent extends AppComponentBase {

    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();
    model: ModelForViewDto = new ModelForViewDto();
    taisan: TaiSanForViewDto = new TaiSanForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _taisanService: TaiSanServiceProxy,
        private _modelService: ModelServiceProxy
    ) {
        super(injector);
    }

    show(soXe?: string | null | undefined): void {
        this._thongtinxeService.getThongTinXeForView(soXe).subscribe(result => {
            this.thongtinxe = result;
            this._taisanService.getTaiSanForView(result.maTaiSan).subscribe(kq => {
                this.taisan = kq;
            });
            this._modelService.getModelForView(result.model).subscribe(kq => {
                this.model = kq;

            });

            this.modal.show();
        })
    }

    close(): void {
        this.modal.hide();
    }
}