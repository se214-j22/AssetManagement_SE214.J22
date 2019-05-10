import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { DieuChuyenServiceProxy, DieuChuyenInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditDieuChuyenModal',
    templateUrl: './create-or-edit-dieuchuyen-modal.component.html'
})
export class CreateOrEditDieuChuyenModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('dieuChuyenCombobox') dieuChuyenCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    dieuChuyen: DieuChuyenInput = new DieuChuyenInput();

    constructor(
        injector: Injector,
        private _dieuChuyenService: DieuChuyenServiceProxy
    ) {
        super(injector);
    }

    show(dieuChuyenId?: number | null | undefined): void {
        this.saving = false;


        this._dieuChuyenService.getDieuChuyenForEdit(dieuChuyenId).subscribe(result => {
            this.dieuChuyen = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.dieuChuyen;
        this.saving = true;
        this._dieuChuyenService.createOrEditDieuChuyen(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
