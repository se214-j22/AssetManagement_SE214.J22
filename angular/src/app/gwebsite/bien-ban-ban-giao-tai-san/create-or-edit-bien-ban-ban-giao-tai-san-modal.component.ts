import { Component, ElementRef, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { HoaDonNhapServiceProxy, HoaDonNhapInput, DonViCungCapTaiSanDto, LoaiTaiSanDto, HoaDonNhapOutput, BienBanBanGiaoTaiSanInput, BienBanBanGiaoTaiSanServiceProxy, TaiSanCoDinhForViewDto, PhongBanDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditBienBanBanGiaoTaiSanModal',
    templateUrl: './create-or-edit-bien-ban-ban-giao-tai-san-modal.component.html'
})
export class CreateOrEditBienBanBanGiaoTaiSanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Input() taiSanCoDinhs: TaiSanCoDinhForViewDto[];
    @Input() phongBans: PhongBanDto[];

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    bienBanBanGiaoTaiSanInput: BienBanBanGiaoTaiSanInput = new BienBanBanGiaoTaiSanInput();

    constructor(
        injector: Injector,
        private _bienBanBanGiaoTaiSanService: BienBanBanGiaoTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(bienBanBanGiaoTaiSanId?: number | null | undefined): void {
        this.saving = false;

        this._bienBanBanGiaoTaiSanService.getBienBanBanGiaoTaiSanForEdit(bienBanBanGiaoTaiSanId).subscribe(result => {
            this.bienBanBanGiaoTaiSanInput = result;
            console.log(this.bienBanBanGiaoTaiSanInput);
            this.modal.show();
        });

    }

    save(): void {
        let input = this.bienBanBanGiaoTaiSanInput;
        this.saving = true;

        if (moment.isMoment(input.ngayNhan) === false) {
            input.ngayNhan = moment(input.ngayNhan + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
            input.ngayNhan.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước
        }

        console.log(input.ngayNhan);

        this._bienBanBanGiaoTaiSanService.createOrEditBienBanBanGiaoTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
