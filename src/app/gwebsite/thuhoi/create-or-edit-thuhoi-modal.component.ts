import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThuHoiServiceProxy, ThuHoiInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditThuHoiModal',
    templateUrl: './create-or-edit-thuhoi-modal.component.html'
})
export class CreateOrEditThuHoiModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thuhoiCombobox') thuhoiCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thuhoi: ThuHoiInput = new ThuHoiInput();

    constructor(
        injector: Injector,
        private _thuhoiService: ThuHoiServiceProxy
    ) {
        super(injector);
    }

    show(thuhoiId?: number | null | undefined): void {
        this.saving = false;


        this._thuhoiService.getThuHoiForEdit(thuhoiId).subscribe(result => {
            this.thuhoi = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.thuhoi;
        this.saving = true;
        this._thuhoiService.createOrEditThuHoi(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
