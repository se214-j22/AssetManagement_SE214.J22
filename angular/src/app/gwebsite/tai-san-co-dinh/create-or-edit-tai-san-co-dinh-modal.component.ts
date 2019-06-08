import { Component, ElementRef, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { HoaDonNhapServiceProxy, HoaDonNhapInput, DonViCungCapTaiSanDto, LoaiTaiSanDto, HoaDonNhapOutput, TaiSanCoDinhInput, TaiSanCoDinhServiceProxy, LoaiTaiSanServiceProxy } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditTaiSanCoDinhModal',
    templateUrl: './create-or-edit-tai-san-co-dinh-modal.component.html'
})
export class CreateOrEditTaiSanCoDinhModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    // @ViewChild('hoaDonNhapCombobox') hoaDonNhapCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    // @Input() donViCungCapTaiSans: DonViCungCapTaiSanDto[];
    @Input() loaiTaiSans: LoaiTaiSanDto[];
    @Input() hoaDonNhaps: HoaDonNhapOutput[];

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    taiSanCoDinhInput: TaiSanCoDinhInput = new TaiSanCoDinhInput();

    constructor(
        injector: Injector,
        private _taiSanCoDinhService: TaiSanCoDinhServiceProxy,
        private _loaiTaiSanService: LoaiTaiSanServiceProxy,
    ) {
        super(injector);
    }

    show(taiSanCoDinhId?: number | null | undefined): void {
        this.saving = false;

        // this._loaiTaiSanService.getLoaiTaiSanForView(this.taiSanCoDinhInput.loaiTaiSanId).subscribe(loaiTaiSan => {

        // })

        this._taiSanCoDinhService.getTaiSanCoDinhForEdit(taiSanCoDinhId).subscribe(result => {
            this.taiSanCoDinhInput = result;
            this.modal.show();

        });

    }

    save(): void {
        let input = this.taiSanCoDinhInput;
        this.saving = true;
        
        this.loaiTaiSans.forEach(loaiTaiSan => {
            if (loaiTaiSan.id === input.loaiTaiSanId) {
                input.haoMonTaiSan = input.giaTriTaiSan * ((100 - loaiTaiSan.tiLeHaoMon)/100);
            }
        })

        this._taiSanCoDinhService.createOrEditTaiSanCoDinh(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
