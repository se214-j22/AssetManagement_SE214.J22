import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinTaiSanServiceProxy, ThongTinTaiSanInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditThongTinTaiSanModal',
    templateUrl: './create-or-edit-thongtintaisan-modal.component.html'
})
export class CreateOrEditThongTinTaiSanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('nhomtaisanCombobox') nhomtaisanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thongtintaisan: ThongTinTaiSanInput = new ThongTinTaiSanInput();

    constructor(
        injector: Injector,
        private _thongtintaisanService: ThongTinTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(thongtintaisanId?: number | null | undefined): void {
        this.saving = false;


        this._thongtintaisanService.getThongTinTaiSanForEdit(thongtintaisanId).subscribe(result => {
            this.thongtintaisan = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.thongtintaisan;
        this.saving = true;
        this._thongtintaisanService.createOrEditThongTinTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
