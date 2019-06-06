import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetTypeServiceProxy, AssetTypeInput, ManufacturerInput, ManufacturerServiceProxy } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditManufacturerModal',
    templateUrl: './create-or-edit-manufacturer-modal.component.html'
})
export class CreateOrEditManufacturerModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('statusSelect') statusSelect;
    @ViewChild('dateInput') dateInput: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    manufacturer: ManufacturerInput = new ManufacturerInput();
    beingCreated: boolean;
    constructor(
        injector: Injector,
        private _manufacturerService: ManufacturerServiceProxy
    ) {
        super(injector);
    }

    show(manufacturerId?: number | null | undefined): void {
        this.saving = false;

        this._manufacturerService.getForEdit(manufacturerId).subscribe(result2 => {
            if (result2) {
                this.manufacturer = result2;
                this.beingCreated = (this.manufacturer.id == null);
                this.modal.show();
            }
        })
    }

    save(): void {
        let input = this.manufacturer;
        this.saving = true;
        this._manufacturerService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
