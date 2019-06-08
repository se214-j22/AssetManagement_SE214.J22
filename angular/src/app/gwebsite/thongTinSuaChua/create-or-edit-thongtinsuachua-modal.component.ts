import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinSuaChuaServiceProxy, ThongTinSuaChuaInput, ModelInput, ThongTinXeServiceProxy, ModelServiceProxy, ThongTinXeInput, CheckServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateArray } from 'ngx-bootstrap/chronos/types';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditThongTinSuaChuaModal',
    templateUrl: './create-or-edit-thongTinSuaChua-modal.component.html'
})
export class CreateOrEditThongTinSuaChuaModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thongTinSuaChuaCombobox') thongTinSuaChuaCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Input() soXe: string;
    saving = false;
    thongTinSuaChua: ThongTinSuaChuaInput = new ThongTinSuaChuaInput();
    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    model: ModelInput = new ModelInput();
    ngaySuaChua: Date;
    ngayDuKienSuaXong: Date;
    check: boolean = false;
    isDuyet: boolean;


    constructor(
        injector: Injector,
        private _thongTinSuaChuaService: ThongTinSuaChuaServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
    ) {
        super(injector);
    }

    show(thongTinSuaChuaId?: number | null | undefined): void {
        this.saving = false;
        this._thongtinxeService.getThongTinSeForEdit(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForEdit(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._thongTinSuaChuaService.getThongTinSuaChuaForEdit(thongTinSuaChuaId).subscribe(result => {
                this.thongTinSuaChua = result;
                if (result.trangThaiDuyet === "Đã duyệt")
                    this.check = true;
                if (thongTinSuaChuaId != -1) {
                    if (result.ngaySuaChua != undefined)
                        this.ngaySuaChua = result.ngaySuaChua.toDate();
                    if (result.ngayDuKienSuaXong != undefined)
                        this.ngayDuKienSuaXong = result.ngayDuKienSuaXong.toDate();
                }
            })
            this.modal.show();
        })


    }

    save(): void {
        if (this.check)
            this.thongTinSuaChua.trangThaiDuyet = "Đã duyệt";
        else
            this.thongTinSuaChua.trangThaiDuyet = "Chưa duyệt";
        this.thongTinSuaChua.soXe = this.soXe;
        this.thongTinSuaChua.ngaySuaChua = moment(this.ngaySuaChua);
        this.thongTinSuaChua.ngayDuKienSuaXong = moment(this.ngayDuKienSuaXong);
        let input = this.thongTinSuaChua;
        this.saving = true;
        this._thongTinSuaChuaService.createOrEditThongTinSuaChua(input).subscribe(result => {
            console.log("kkkkkkk", input);
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();

        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}