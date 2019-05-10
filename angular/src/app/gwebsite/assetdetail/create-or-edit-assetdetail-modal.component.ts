import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetDetailServiceProxy, AssetDetailInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetDetailModal',
    templateUrl: './create-or-edit-assetdetail-modal.component.html'
})
export class CreateOrEditAssetDetailModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('assetdetailCombobox') assetdetailCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    assetdetail: AssetDetailInput = new AssetDetailInput();

    constructor(
        injector: Injector,
        private _assetdetailService: AssetDetailServiceProxy
    ) {
        super(injector);
    }

    show(assetdetailId?: number | null | undefined): void {
        this.saving = false;


        this._assetdetailService.getAssetDetailForEdit(assetdetailId).subscribe(result => {
            this.assetdetail = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.assetdetail;
        this.saving = true;
        this._assetdetailService.createOrEditAssetDetail(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
