import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { UseAssetServiceProxy, UseAssetInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditUseAssetModal',
    templateUrl: './create-or-edit-useasset-modal.component.html'
})
export class CreateOrEditUseAssetModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('useassetCombobox') useassetCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    useasset: UseAssetInput = new UseAssetInput();

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy
    ) {
        super(injector);
    }

    show(useassetId?: number | null | undefined): void {
        this.saving = false;


        this._useassetService.getUseAssetForEdit(useassetId).subscribe(result => {
            this.useasset = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.useasset;
        this.saving = true;
        this._useassetService.createOrEditUseAsset(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}