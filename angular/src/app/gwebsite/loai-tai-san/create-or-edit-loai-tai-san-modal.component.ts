import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { LoaiTaiSanServiceProxy, LoaiTaiSanInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditLoaiTaiSanModal',
    templateUrl: './create-or-edit-loai-tai-san-modal.component.html'
})
export class CreateOrEditLoaiTaiSanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('loaiTaiSanCombobox') loaiTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    loaiTaiSan: LoaiTaiSanInput = new LoaiTaiSanInput();

    constructor(
        injector: Injector,
        private _loaiTaiSanService: LoaiTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(loaiTaiSanId?: number | null | undefined): void {
        this.saving = false;


        this._loaiTaiSanService.getLoaiTaiSanForEdit(loaiTaiSanId).subscribe(result => {
            this.loaiTaiSan = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.loaiTaiSan;
        this.saving = true;
        this._loaiTaiSanService.createOrEditLoaiTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
