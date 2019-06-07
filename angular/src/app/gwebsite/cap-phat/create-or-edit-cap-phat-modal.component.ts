import { CapPhatServiceProxy, CapPhatInput, PhongBanDto,SanPhamDto } from '@shared/service-proxies/service-proxies'
import { Component, ViewChild, ElementRef, Output, Injector, EventEmitter, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditCapPhatModal',
    templateUrl: './create-or-edit-cap-phat-modal.component.html'
})
export class CreateOrEditCapPhatModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('capPhatCombobox') capPhatCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Input() phongBanList: PhongBanDto[];
    @Input() sanPhamList: SanPhamDto[];

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

        // lấy giờ hệ thống
        let now = new Date();
        input.ngayCap= moment(now);
        input.ngayCap.utcOffset(0, true);

        this._capPhatService.createOrEditCapPhat(input).subscribe(result => {
            this.notify.info(this.l('Saved Successfully'));
            this.close();
        })
    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}