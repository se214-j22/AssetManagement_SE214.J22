import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { XuatSuDungServiceProxy, XuatSuDungInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditXuatSuDungModal',
    templateUrl: './create-or-edit-xuatsudung-modal.component.html'
})
export class CreateOrEditXuatSuDungModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('xuatsudungCombobox') xuatsudungCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    xuatsudung: XuatSuDungInput = new XuatSuDungInput();

    constructor(
        injector: Injector,
        private _xuatsudungService: XuatSuDungServiceProxy
    ) {
        super(injector);
    }

    show(xuatsudungId?: number | null | undefined): void {
        this.saving = false;


        this._xuatsudungService.getXuatSuDungForEdit(xuatsudungId).subscribe(result => {
            this.xuatsudung = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.xuatsudung;
        this.saving = true;
        this._xuatsudungService.createOrEditXuatSuDung(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
