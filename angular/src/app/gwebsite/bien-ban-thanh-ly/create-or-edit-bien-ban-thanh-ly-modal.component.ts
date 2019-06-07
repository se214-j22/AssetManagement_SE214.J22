import { Component, ElementRef, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { HoaDonNhapServiceProxy, HoaDonNhapInput, DonViCungCapTaiSanDto, LoaiTaiSanDto, HoaDonNhapOutput, BienBanThanhLyInput, BienBanThanhLyServiceProxy, TaiSanCoDinhForViewDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditBienBanThanhLyModal',
    templateUrl: './create-or-edit-bien-ban-thanh-ly-modal.component.html'
})
export class CreateOrEditBienBanThanhLyModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Input() taiSanCoDinhs: TaiSanCoDinhForViewDto[];

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    bienBanThanhLyInput: BienBanThanhLyInput = new BienBanThanhLyInput();

    constructor(
        injector: Injector,
        private _bienBanThanhLyService: BienBanThanhLyServiceProxy
    ) {
        super(injector);
    }

    show(bienBanThanhLyId?: number | null | undefined): void {
        this.saving = false;

        this._bienBanThanhLyService.getBienBanThanhLyForEdit(bienBanThanhLyId).subscribe(result => {
            this.bienBanThanhLyInput = result;
            this.modal.show();

        });

    }

    save(): void {
        let input = this.bienBanThanhLyInput;
        this.saving = true;

        if (moment.isMoment(input.ngayThanhLy) === false) {
            input.ngayThanhLy = moment(input.ngayThanhLy + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
            input.ngayThanhLy.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước
        }

        this._bienBanThanhLyService.createOrEditBienBanThanhLy(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
