import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { RepairServiceProxy, RepairInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditRepairModal',
    templateUrl: './create-or-edit-repair-modal.component.html'
})
export class CreateOrEditRepairModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('repairCombobox') repairCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    repair: RepairInput = new RepairInput();

    constructor(
        injector: Injector,
        private _repairService: RepairServiceProxy
    ) {
        super(injector);
    }

    show(repairId?: number | null | undefined): void {
        this.saving = false;


        this._repairService.getRepairForEdit(repairId).subscribe(result => {
            this.repair = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.repair;
        this.saving = true;
        this._repairService.createOrEditRepair(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
