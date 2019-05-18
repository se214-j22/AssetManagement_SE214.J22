import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { DonViCungCapTaiSanServiceProxy, DonViCungCapTaiSanInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditDonViCungCapTaiSanModal',
    templateUrl: './create-or-edit-don-vi-cung-cap-tai-san-modal.component.html'
})
export class CreateOrEditDonViCungCapTaiSanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('donViCungCapTaiSanCombobox') donViCungCapTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    donViCungCapTaiSan: DonViCungCapTaiSanInput = new DonViCungCapTaiSanInput();

    constructor(
        injector: Injector,
        private _donViCungCapTaiSanService: DonViCungCapTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(donViCungCapTaiSanId?: number | null | undefined): void {
        this.saving = false;


        this._donViCungCapTaiSanService.getDonViCungCapTaiSanForEdit(donViCungCapTaiSanId).subscribe(result => {
            this.donViCungCapTaiSan = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.donViCungCapTaiSan;
        this.saving = true;
        this._donViCungCapTaiSanService.createOrEditDonViCungCapTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
