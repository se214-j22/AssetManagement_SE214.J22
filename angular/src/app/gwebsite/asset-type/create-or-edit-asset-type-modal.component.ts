import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetTypeServiceProxy, AssetTypeInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetTypeModal',
    templateUrl: './create-or-edit-asset-type-modal.component.html'
})
export class CreateOrEditAssetTypeModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('statusSelect') statusSelect;
    @ViewChild('dateInput') dateInput: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    assetType: AssetTypeInput = new AssetTypeInput();
    beingCreated: boolean;
    constructor(
        injector: Injector,
        private _assetTypeService: AssetTypeServiceProxy
    ) {
        super(injector);
    }

    show(assetTypeId?: number | null | undefined): void {
        this.saving = false;

        this._assetTypeService.getForEdit(assetTypeId).subscribe(result2 => {
            if (result2) {
                this.assetType = result2;
                this.beingCreated = (this.assetType.id == null);
                this.modal.show();
            }
        })
    }

    save(): void {
        let input = this.assetType;
        this.saving = true;
        this._assetTypeService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
