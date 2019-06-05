import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { SuaChuaServiceProxy, SuaChuaInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditSuaChuaModal',
    templateUrl: './create-or-edit-suachua-modal.component.html'
})
export class CreateOrEditSuaChuaModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('suachuaCombobox') suachuaCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    suachua: SuaChuaInput = new SuaChuaInput();

    constructor(
        injector: Injector,
        private _suachuaService: SuaChuaServiceProxy
    ) {
        super(injector);
    }

    show(suachuaId?: number | null | undefined): void {
        this.saving = false;


        this._suachuaService.getSuaChuaForEdit(suachuaId).subscribe(result => {
            this.suachua = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.suachua;
        this.saving = true;
        this._suachuaService.createOrEditSuaChua(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
