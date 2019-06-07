import { Component, ElementRef, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { HoaDonNhapServiceProxy, HoaDonNhapInput, DonViCungCapTaiSanDto, LoaiTaiSanDto, HoaDonNhapOutput, PhieuBaoDuongInput, PhieuBaoDuongServiceProxy, TaiSanCoDinhForViewDto } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditPhieuBaoDuongModal',
    templateUrl: './create-or-edit-phieu-bao-duong-modal.component.html'
})
export class CreateOrEditPhieuBaoDuongModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    // @Input() donViCungCapTaiSans: DonViCungCapTaiSanDto[];
    // @Input() loaiTaiSans: LoaiTaiSanDto[];
    // @Input() hoaDonNhaps: HoaDonNhapOutput[];
    @Input() taiSanCoDinhs: TaiSanCoDinhForViewDto[];

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    phieuBaoDuongInput: PhieuBaoDuongInput = new PhieuBaoDuongInput();

    constructor(
        injector: Injector,
        private _phieuBaoDuongService: PhieuBaoDuongServiceProxy
    ) {
        super(injector);
    }

    show(phieuBaoDuongId?: number | null | undefined): void {
        this.saving = false;

        this._phieuBaoDuongService.getPhieuBaoDuongForEdit(phieuBaoDuongId).subscribe(result => {
            this.phieuBaoDuongInput = result;
            this.modal.show();

        });

    }

    save(): void {
        let input = this.phieuBaoDuongInput;
        this.saving = true;

        this._phieuBaoDuongService.createOrEditPhieuBaoDuong(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
