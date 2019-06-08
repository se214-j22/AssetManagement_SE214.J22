import { QuanLyVanHanhForViewDto, QuanLyVanHanhServiceProxy, ModelForViewDto, ThongTinXeForViewDto, ModelServiceProxy, ThongTinDangKiemServiceProxy, ThongTinDangKiemForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';

@Component({
    selector: 'viewDangKiemXeModal',
    templateUrl: './view-thongtindangkiem-modal.component.html',

})

export class ViewDangKiemXeModalComponent extends AppComponentBase {

    dangkiemxe: ThongTinDangKiemForViewDto = new ThongTinDangKiemForViewDto();
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();


    @ViewChild('viewModal') modal: ModalDirective;
    @Input() soXe: string;

    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _dangkiemxeService: ThongTinDangKiemServiceProxy

    ) {
        super(injector);
    }

    show(id?: number | null | undefined): void {

        this._thongtinxeService.getThongTinXeForView(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForView(kq.model).subscribe(kq1 => {
                this.model = kq1;
                this._dangkiemxeService.getThongTinDangKiemForView(id).subscribe(result => {
                    this.dangkiemxe = result;
                    console.log("test show view-thongtindangkiem-modal.components" + id, result);
                })
            })
        });

        this.modal.show();

    }
    close(): void {
        this.modal.hide();
    }
}