import { QuanLyVanHanhForViewDto, QuanLyVanHanhServiceProxy, ModelForViewDto, ThongTinXeForViewDto, ModelServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Input } from "@angular/core";
import { ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';

@Component({
    selector: 'viewVanHanhXeModal',
    templateUrl: './view-vanhanhxe-modal.component.html',
    styleUrls: ['./view-vanhanhxe.component.css']
})

export class ViewVanHanhXeModalComponent extends AppComponentBase {

    vanhanhxe: QuanLyVanHanhForViewDto = new QuanLyVanHanhForViewDto();
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxe: ThongTinXeForViewDto = new ThongTinXeForViewDto();

    nhienlieudinhmuc: number;

    @ViewChild('viewModal') modal: ModalDirective;
    @Input() soXe: string;

    constructor(
        injector: Injector,
        private _vanhanhxeService: QuanLyVanHanhServiceProxy,
        private _modelService: ModelServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy

    ) {
        super(injector);
    }

    show(id?: number | null | undefined): void {
        console.log("doituongne", id);
        this._thongtinxeService.getThongTinXeForView(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForView(kq.model).subscribe(kq1 => {
                this.model = kq1;

            })
        });
        this._vanhanhxeService.getQuanLyVanHanhForView(id).subscribe(result => {
            this.vanhanhxe = result;
            console.log("doituong", result);
            this.nhienlieudinhmuc = (result.soKM * this.model.dinhMucNhienLieu) / 100;



        })


        this.modal.show();




    }
    close(): void {
        this.modal.hide();
    }
}