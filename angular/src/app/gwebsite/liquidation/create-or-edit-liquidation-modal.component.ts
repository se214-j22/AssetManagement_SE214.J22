import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { LiquidationServiceProxy, LiquidationInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditLiquidationModal',
    templateUrl: './create-or-edit-liquidation-modal.component.html'
})
export class CreateOrEditLiquidationModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('liquidationCombobox') liquidationCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    liquidation: LiquidationInput = new LiquidationInput();

    constructor(
        injector: Injector,
        private _liquidationService: LiquidationServiceProxy
    ) {
        super(injector);
    }

    show(liquidationId?: number | null | undefined): void {
        this.saving = false;


        this._liquidationService.getLiquidationForEdit(liquidationId).subscribe(result => {
            this.liquidation = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.liquidation;
        this.saving = true;
        this._liquidationService.createOrEditLiquidation(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}