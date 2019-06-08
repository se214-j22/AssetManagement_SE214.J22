import { Component, ElementRef, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { HoaDonNhapServiceProxy, HoaDonNhapInput, DonViCungCapTaiSanDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditHoaDonNhapModal',
    templateUrl: './create-or-edit-hoa-don-nhap-modal.component.html'
})
export class CreateOrEditHoaDonNhapModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('hoaDonNhapCombobox') hoaDonNhapCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Input() donViCungCapTaiSans: DonViCungCapTaiSanDto[];

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    hoaDonNhap: HoaDonNhapInput = new HoaDonNhapInput();

    constructor(
        injector: Injector,
        private _hoaDonNhapService: HoaDonNhapServiceProxy
    ) {
        super(injector);
    }

    show(hoaDonNhapId?: number | null | undefined): void {
        this.saving = false;
        console.log(this.donViCungCapTaiSans);

        this._hoaDonNhapService.getHoaDonNhapForEdit(hoaDonNhapId).subscribe(result => {
            this.hoaDonNhap = result;
            this.modal.show();

        });

    }

    save(): void {
        let input = this.hoaDonNhap;
        this.saving = true;

        console.log(input.ngayNhan);

        if (moment.isMoment(input.ngayNhan) === false) {
            input.ngayNhan = moment(input.ngayNhan + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
            input.ngayNhan.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước
        }

        this._hoaDonNhapService.createOrEditHoaDonNhap(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
