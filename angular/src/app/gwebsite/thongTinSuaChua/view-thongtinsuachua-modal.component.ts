import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { ThongTinSuaChuaServiceProxy, ThongTinSuaChuaForViewDTO, ModelForViewDto, ThongTinXeForViewDto, ModelServiceProxy, ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewThongTinSuaChuaModal',
    templateUrl: './view-thongTinSuaChua-modal.component.html'
})

export class ViewThongTinSuaChuaModalComponent extends AppComponentBase {
    thongTinSuaChua: ThongTinSuaChuaForViewDTO = new ThongTinSuaChuaForViewDTO();
    @ViewChild('viewModal') modal: ModalDirective;
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();
    @Input() soXe: string;


    constructor(
        injector: Injector,
        private _thongTinSuaChuaService: ThongTinSuaChuaServiceProxy,
        private _modelService: ModelServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
    ) {
        super(injector);
    }

    show(thongTinSuaChuaId?: number | null | undefined): void {
        this._thongtinxeService.getThongTinXeForView(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForView(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._thongTinSuaChuaService.getThongTinSuaChuaForView(thongTinSuaChuaId).subscribe(result => {
                this.thongTinSuaChua = result;
                this.modal.show();
            })
        })

    }

    close(): void {
        this.modal.hide();
    }
}