import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { LiquidationDetailServiceProxy, LiquidationDetailInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditLiquidationDetailModal',
    templateUrl: './create-or-edit-liquidationdetail-modal.component.html'
})
export class CreateOrEditLiquidationDetailModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('liquidationdetailCombobox') liquidationdetailCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    liquidationdetail: LiquidationDetailInput = new LiquidationDetailInput();

    constructor(
        injector: Injector,
        private _liquidationdetailService: LiquidationDetailServiceProxy
    ) {
        super(injector);
    }

    show(liquidationdetailId?: number | null | undefined): void {
        this.saving = false;


        this._liquidationdetailService.getLiquidationDetailForEdit(liquidationdetailId).subscribe(result => {
            this.liquidationdetail = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.liquidationdetail;
        this.saving = true;
        this._liquidationdetailService.createOrEditLiquidationDetail(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
