import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { PhiDuongBoServiceProxy, PhiDuongBoInput, ThongTinXeInput, ModelInput, ModelServiceProxy, ThongTinXeServiceProxy } from "@shared/service-proxies/service-proxies";
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPhiDuongBoModal',
    templateUrl: './create-or-edit-phiduongbo-modal.component.html'
})
export class CreateOrEditPhiDuongBoModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('phiDuongBoCombobox') phiDuongBoCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;
    phiDuongBo: PhiDuongBoInput = new PhiDuongBoInput();
    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    model: ModelInput = new ModelInput();
    ngayCapNhap: Date = new Date();
    ngayHetHan: Date;
    ngayDongPhi: Date;
    @Input() soXe: string;

    constructor(
        injector: Injector,
        private _phiDuongBoService: PhiDuongBoServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
    ) {
        super(injector);
        // this.ngayCapNhap = new Date();
        // this.ngayDongPhi = new Date();
        // this.ngayHetHan = new Date();
    }

    show(phiDuongBoId?: number | null | undefined): void {
        this.saving = false;
        this._thongtinxeService.getThongTinSeForEdit(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForEdit(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._phiDuongBoService.getPhiDuongBoForEdit(phiDuongBoId).subscribe(result => {
                this.phiDuongBo = result;
                if (phiDuongBoId != -1) {

                    // if (result.ngayCapNhat != undefined)
                    //     this.ngayCapNhap = result.ngayCapNhat.toDate()
                    if (result.ngayDongPhi != undefined)
                        this.ngayDongPhi = result.ngayDongPhi.toDate();
                    if (result.ngayHetHanDongPhi != undefined)
                        this.ngayHetHan = result.ngayHetHanDongPhi.toDate();

                }
            })
            this.modal.show();
        })
    }
    save(): void {

        this.phiDuongBo.ngayCapNhat = moment(this.ngayCapNhap);
        this.phiDuongBo.ngayDongPhi = moment(this.ngayDongPhi);
        this.phiDuongBo.ngayHetHanDongPhi = moment(this.ngayHetHan);
        this.phiDuongBo.soXe = this.soXe;
        let input = this.phiDuongBo;
        this.saving = true;
        this._phiDuongBoService.createOrEditPhiDuongBo(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })
    }

    close(): void {
        this.ngayCapNhap = null;
        this.ngayHetHan = null;
        this.ngayDongPhi = null;
        this.modal.hide();
        this.modalSave.emit(null);
    }



}