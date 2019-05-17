import { PhongBanServiceProxy, PhongBanInput } from '@shared/service-proxies/service-proxies'
import { Component, ViewChild, ElementRef, Output, Injector, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'createOrEditPhongBanModal',
    templateUrl: './create-or-edit-phong-ban-modal.component.html'
})
export class CreateOrEditPhongBanModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('phongBanCombobox') phongBanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    phongBan: PhongBanInput = new PhongBanInput();

    constructor(
        injector: Injector,
        private _phongBanService: PhongBanServiceProxy
    ) {
        super(injector);
    }

    show(phongBanId?: number | null | undefined): void {
        this.saving = false;

        this._phongBanService.getPhongBanForEdit(phongBanId).subscribe(result => {
            this.phongBan = result;
            this.modal.show();
        })
    }

    save(): void {
        let input = this.phongBan;
        this.saving = true;
        this._phongBanService.createOrEditPhongBan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}