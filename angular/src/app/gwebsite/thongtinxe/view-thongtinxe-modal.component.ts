import { ThongTinXeForViewDto, ModelDto, TaiSanForViewDto, ModelForViewDto, TaiSanServiceProxy, ModelServiceProxy, ThietBiKemTheoServiceProxy, ThietBiKemTheoDto } from './../../../shared/service-proxies/service-proxies';
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
    tbkts: ThietBiKemTheoDto[] = [];

    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _taisanService: TaiSanServiceProxy,
        private _modelService: ModelServiceProxy,
        private _tbktService: ThietBiKemTheoServiceProxy
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
                if (this.thongtinxe.soXe) {
                    this._tbktService.getThietBiKemTheosByFilter(this.thongtinxe.soXe, undefined, undefined, undefined).subscribe(kq3 => {
                        this.tbkts = kq3.items;
                    })
                }

            });

            this.modal.show();
        })
    }

    close(): void {
        this.modal.hide();
    }
}