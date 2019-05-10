import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CapPhatServiceProxy, CapPhatInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditCapPhatModal',
    templateUrl: './create-or-edit-capphat-modal.component.html'
})
export class CreateOrEditCapPhatModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('capPhatCombobox') capPhatCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    capPhat: CapPhatInput = new CapPhatInput();

    constructor(
        injector: Injector,
        private _capPhatService: CapPhatServiceProxy
    ) {
        super(injector);
    }

    show(capPhatId?: number | null | undefined): void {
        this.saving = false;


        this._capPhatService.getCapPhatForEdit(capPhatId).subscribe(result => {
            this.capPhat = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.capPhat;
        this.saving = true;
        this._capPhatService.createOrEditCapPhat(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
