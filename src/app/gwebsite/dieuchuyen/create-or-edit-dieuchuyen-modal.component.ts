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
    @ViewChild('dieuchuyenCombobox') dieuchuyenCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    dieuchuyen: DieuChuyenInput = new DieuChuyenInput();

    constructor(
        injector: Injector,
        private _dieuchuyenService: DieuChuyenServiceProxy
    ) {
        super(injector);
    }

    show(dieuchuyenId?: number | null | undefined): void {
        this.saving = false;


        this._dieuchuyenService.getDieuChuyenForEdit(dieuchuyenId).subscribe(result => {
            this.dieuchuyen = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.dieuchuyen;
        this.saving = true;
        this._dieuchuyenService.createOrEditDieuChuyen(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
