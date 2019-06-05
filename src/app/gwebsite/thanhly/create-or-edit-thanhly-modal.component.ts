import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThanhLyServiceProxy, ThanhLyInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditThanhLyModal',
    templateUrl: './create-or-edit-thanhly-modal.component.html'
})
export class CreateOrEditThanhLyModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thanhlyCombobox') thanhlyCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thanhly: ThanhLyInput = new ThanhLyInput();

    constructor(
        injector: Injector,
        private _thanhlyService: ThanhLyServiceProxy
    ) {
        super(injector);
    }

    show(thanhlyId?: number | null | undefined): void {
        this.saving = false;


        this._thanhlyService.getThanhLyForEdit(thanhlyId).subscribe(result => {
            this.thanhly = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.thanhly;
        this.saving = true;
        this._thanhlyService.createOrEditThanhLy(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
