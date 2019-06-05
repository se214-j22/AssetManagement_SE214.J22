import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { NhomTaiSanServiceProxy, NhomTaiSanInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditNhomTaiSanModal',
    templateUrl: './create-or-edit-nhomtaisan-modal.component.html'
})
export class CreateOrEditNhomTaiSanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('nhomtaisanCombobox') nhomtaisanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    nhomtaisan: NhomTaiSanInput = new NhomTaiSanInput();

    constructor(
        injector: Injector,
        private _nhomtaisanService: NhomTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(nhomtaisanId?: number | null | undefined): void {
        this.saving = false;


        this._nhomtaisanService.getNhomTaiSanForEdit(nhomtaisanId).subscribe(result => {
            this.nhomtaisan = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.nhomtaisan;
        this.saving = true;
        this._nhomtaisanService.createOrEditNhomTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
