import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinXeServiceProxy, ModelServiceProxy, ThongTinXeInput, ModelInput, ThongTinDangKiemServiceProxy, ThongTinDangKiemInput, CheckServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditDangKiemXeModal',
    templateUrl: './create-or-edit-thongtindangkiem-modal.component.html',
})
export class CreateOrEditDangKiemXeModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('customerCombobox') customerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    saving = false;

    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    dangkiemxe: ThongTinDangKiemInput = new ThongTinDangKiemInput();
    model: ModelInput = new ModelInput();
    // arrCoQuanDangKiem: string[] = [];
    ngayDangKiem: Date;
    ngayHetHanDangKiem: Date;
    check: boolean = false;
    @Input() soXe: string;
    isDuyet: boolean;


    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
        private _dangkiemxeService: ThongTinDangKiemServiceProxy,
        // private _coquandangkiemService: CoQuanDangKiemServiceProxy
        private _isDuyet: CheckServiceProxy
    ) {
        super(injector);
        this._isDuyet.isDuyet().subscribe(result => {
            this.isDuyet = result;
        })

    }

    show(Id?: number | null | undefined): void {
        this.saving = false;
        this._thongtinxeService.getThongTinSeForEdit(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            if (this.thongtinxe.trangThaiDuyet === "Đã duyệt")
                this.check = true;
            this._modelService.getModelForEdit(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._dangkiemxeService.getThongTinDangKiemForEdit(Id).subscribe(result => {
                this.dangkiemxe = result;
                if (Id != -1) {
                    if (this.ngayDangKiem != undefined)
                        this.ngayDangKiem = result.ngayDangKiem.toDate();
                    if (this.ngayHetHanDangKiem != undefined)
                        this.ngayHetHanDangKiem = result.ngayHetHanDangKiem.toDate();
                }

            })

            // this._coquandangkiemService.getCoQuanDangKiemsByFilter(undefined, undefined, undefined, undefined).subscribe(kq => {

            //     kq.items.map(item => {
            //         this.arrCoQuanDangKiem.push(item.tenCoQuanDangKiem);
            //     })
            //     console.log("mang", this.arrCoQuanDangKiem);
            // })

        })
        this.modal.show();
    }

    save(): void {

        if (this.check)
            this.dangkiemxe.trangThaiDuyet = "Đã duyệt";
        else
            this.dangkiemxe.trangThaiDuyet = "Chưa duyệt";
        this.dangkiemxe.soXe = this.soXe;
        if (this.ngayDangKiem != null)
            this.dangkiemxe.ngayDangKiem = moment(this.ngayDangKiem);
        if (this.ngayHetHanDangKiem != null)
            this.dangkiemxe.ngayHetHanDangKiem = moment(this.ngayHetHanDangKiem);
        let input = this.dangkiemxe;
        // console.log("ahihihi", this.dangkiemxe.coQuanDangKiem);
        this.saving = true;
        this._dangkiemxeService.createOrEditThongTinDangKiem(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
            // this.arrCoQuanDangKiem = [];
        })


    }

    close(): void {
        this.ngayDangKiem = null;
        this.ngayHetHanDangKiem = null;
        this.modal.hide();
        // this.arrCoQuanDangKiem = [];
        this.modalSave.emit(null);

    }
}
