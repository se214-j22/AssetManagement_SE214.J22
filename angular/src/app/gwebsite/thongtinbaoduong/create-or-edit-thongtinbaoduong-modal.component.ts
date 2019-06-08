import { Component, ElementRef, EventEmitter, Injector, Output, AfterViewInit, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinBaoDuongServiceProxy, ThongTinBaoDuongInput, ThongTinXeInput, ModelInput, ModelServiceProxy, ThongTinXeServiceProxy, CheckServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditThongTinBaoDuongModal',
    templateUrl: './create-or-edit-thongtinbaoduong-modal.component.html'
})
export class CreateOrEditThongTinBaoDuongModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thongtinbaoduongCombobox') thongtinbaoduongCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Input() soXe: string;

    saving = false;
    ngayBaoDuong: Date;
    ngayBaoDuongTiepTheo: Date;
    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    model: ModelInput = new ModelInput();
    check: boolean = false;
    isDuyet: boolean;


    thongtinbaoduong: ThongTinBaoDuongInput = new ThongTinBaoDuongInput();

    constructor(
        injector: Injector,
        private _thongtinbaoduongService: ThongTinBaoDuongServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
        private _isDuyet: CheckServiceProxy
    ) {
        super(injector);
        _isDuyet.isDuyet().subscribe(result => {
            this.isDuyet = result;
        })
    }

    // ngAfterViewInit(): void {
    //     let t = this;
    //     $(this.dateInput.nativeElement).datetimepicker({
    //         locale: abp.localization.currentLanguage.name,
    //         format: 'L'
    //     }).on('dp.change', function (e) {
    //         t.thongtinbaoduong.ngayBaoDuong = $(t.ngayBaoDuongInput.nativeElement).val().toString();
    //         t.thongtinbaoduong.ngayBaoDuongTiepTheo = $(t.ngayBaoDuongTiepTheoInput.nativeElement).val().toString();
    //     });
    // }
    show(thongtinbaoduongId?: number | null | undefined): void {
        this.saving = false;
        this._thongtinxeService.getThongTinSeForEdit(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            if (this.thongtinxe.trangThaiDuyet === "Đã duyệt")
                this.check = true;
            this._modelService.getModelForEdit(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            this._thongtinbaoduongService.getThongTinBaoDuongForEdit(thongtinbaoduongId).subscribe(result => {
                this.thongtinbaoduong = result;
                if (thongtinbaoduongId != -1) {
                    this.ngayBaoDuong = result.ngayBaoDuong.toDate();
                    this.ngayBaoDuongTiepTheo = result.ngayBaoDuongTiepTheo.toDate();
                    console.log("testNgayBaoDuong", this.ngayBaoDuong);
                }

            })
        })
        this.modal.show();




    }

    save(): void {
        if (this.check)
            this.thongtinbaoduong.trangThaiDuyet = "Đã duyệt";
        else
            this.thongtinbaoduong.trangThaiDuyet = "Chưa duyệt";
        this.thongtinbaoduong.soXe = this.soXe;
        this.thongtinbaoduong.ngayBaoDuong = moment(this.ngayBaoDuong);
        this.thongtinbaoduong.ngayBaoDuongTiepTheo = moment(this.ngayBaoDuongTiepTheo);
        let input = this.thongtinbaoduong;
        console.log(input);
        this.saving = true;
        this._thongtinbaoduongService.createOrEditThongTinBaoDuong(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
        this.ngayBaoDuong = null;
        this.ngayBaoDuongTiepTheo = null;
    }
}
