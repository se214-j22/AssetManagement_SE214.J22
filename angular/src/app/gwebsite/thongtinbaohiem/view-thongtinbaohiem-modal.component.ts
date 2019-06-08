import { ModelForViewDto, ThongTinXeForViewDto, ModelServiceProxy, ThongTinBaoHiemServiceProxy, ThongTinBaoHiemForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';


@Component({
    selector: 'viewBaoHiemXeModal',
    templateUrl: './view-thongtinbaohiem-modal.component.html',

})

export class ViewBaoHiemXeModalComponent extends AppComponentBase {

    baohiemxe: ThongTinBaoHiemForViewDto = new ThongTinBaoHiemForViewDto();
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();


    @ViewChild('viewModal') modal: ModalDirective;
    @Input() soXe: string;

    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _baohiemxeService: ThongTinBaoHiemServiceProxy

    ) {
        super(injector);
    }

    show(id?: number | null | undefined): void {

        this._thongtinxeService.getThongTinXeForView(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForView(kq.model).subscribe(kq1 => {
                this.model = kq1;
                this._baohiemxeService.getThongTinBaoHiemForView(id).subscribe(result => {
                    this.baohiemxe = result;
                    console.log("day ne" + id, result);
                })
            })
        });

        this.modal.show();

    }
    close(): void {
        this.modal.hide();
    }
}