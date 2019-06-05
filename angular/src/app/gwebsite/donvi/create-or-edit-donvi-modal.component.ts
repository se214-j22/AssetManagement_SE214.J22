import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { DonViServiceProxy, DonViInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditDonViModal',
    templateUrl: './create-or-edit-donvi-modal.component.html'
})
export class CreateOrEditDonViModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('donViCombobox') donViCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    donVi: DonViInput = new DonViInput();

    constructor(
        injector: Injector,
        private _donViService: DonViServiceProxy
    ) {
        super(injector);
    }

    show(donViId?: number | null | undefined): void {
        this.saving = false;


        this._donViService.getDonViForEdit(donViId).subscribe(result => {
            this.donVi = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.donVi;
        this.saving = true;
        this._donViService.createOrEditDonVi(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
