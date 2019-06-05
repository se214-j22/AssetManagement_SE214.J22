import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { TransferServiceProxy, TransferInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditTransferModal',
    templateUrl: './create-or-edit-transfer-modal.component.html'
})
export class CreateOrEditTransferModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('transferCombobox') transferCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    transfer: TransferInput = new TransferInput();

    constructor(
        injector: Injector,
        private _transferService: TransferServiceProxy
    ) {
        super(injector);
    }

    show(transferId?: number | null | undefined): void {
        this.saving = false;


        this._transferService.getTransferForEdit(transferId).subscribe(result => {
            this.transfer = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.transfer;
        this.saving = true;
        this._transferService.createOrEditTransfer(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
