import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ProviderServiceProxy, ProviderInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditProviderModal',
    templateUrl: './create-or-edit-provider-modal.component.html'
})
export class CreateOrEditProviderModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('providerCombobox') providerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    provider: ProviderInput = new ProviderInput();

    constructor(
        injector: Injector,
        private _providerService: ProviderServiceProxy
    ) {
        super(injector);
    }

    show(providerId?: number | null | undefined): void {
        this.saving = false;


        this._providerService.getProviderForEdit(providerId).subscribe(result => {
            this.provider = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.provider;
        this.saving = true;
        this._providerService.createOrEditProvider(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}