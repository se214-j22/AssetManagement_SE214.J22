import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinXeInput, ThongTinXeServiceProxy, TaiSanInput, TaiSanServiceProxy, TaiSanDto, ModelDto, ModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaiSanComponent } from '../taisan/taisan.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ModelComponent } from '../model/model.component';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditThongTinXeModal',
    templateUrl: './create-or-edit-thongtinxe-modal.component.html',
    styleUrls: ['./create-or-edit-thongtinxe.component.css'],
    animations: [appModuleAnimation()]
})
export class CreateOrEditThongTinXeModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('viewTaiSan') viewTaiSan: TaiSanComponent;
    @ViewChild('viewModel') viewModel: ModelComponent;
    @ViewChild('customerCombobox') customerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    taisan: TaiSanDto = new TaiSanDto();
    taisanItem: TaiSanDto = new TaiSanDto();
    model: ModelDto = new ModelDto();
    modelItem: ModelDto = new ModelDto();
    ngaydangkibandau: Date;

    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
        private _taisanService: TaiSanServiceProxy
    ) {
        super(injector);
    }
    GetTaiSan(taisan: TaiSanDto) {
        if (taisan.maTaiSan != undefined) {

            this.taisan = taisan;
        }


    }
    GetModel(model: ModelDto) {
        if (model.model != undefined)
            this.model = model;
    }


    show(soXe?: string | null | undefined): void {
        this.saving = false;

        this._thongtinxeService.getThongTinSeForEdit(soXe).subscribe(result => {
            this.thongtinxe = result;
            this.ngaydangkibandau = result.ngayDangKiBanDau.toDate();
            this._taisanService.getTaiSanForEdit(result.maTaiSan).subscribe(kq => {
                this.taisan = kq;
                console.log("hix", kq);

            });
            this._modelService.getModelForEdit(result.model).subscribe(kq2 => {
                this.model = kq2;

            })
            this.modal.show();

        })
    }

    save(): void {

        this.saving = true;
        this.thongtinxe.ngayDangKiBanDau = moment(this.ngaydangkibandau);
        this.thongtinxe.maTaiSan = this.taisan.maTaiSan;
        this.thongtinxe.model = this.model.model;
        let input = this.thongtinxe;
        this._thongtinxeService.createOrEditThongTinXe(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            console.log("lasao" + this.thongtinxe.donViSuDung);
            this.close();
        })

    }
    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
        this.thongtinxe.model = this.model.model;
    }
}
