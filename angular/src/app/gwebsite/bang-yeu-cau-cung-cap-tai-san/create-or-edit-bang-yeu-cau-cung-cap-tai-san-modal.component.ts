import { BangYeuCauCungCapTaiSanServiceProxy, BangYeuCauCungCapTaiSanInput, PhongBanDto } from '@shared/service-proxies/service-proxies'
import { Component, ViewChild, ElementRef, Output, Injector, EventEmitter, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditBangYeuCauCungCapTaiSanModal',
    templateUrl: './create-or-edit-bang-yeu-cau-cung-cap-tai-san-modal.component.html'
})
export class CreateOrEditBangYeuCauCungCapTaiSanModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('bangYeuCauCungCapTaiSanCombobox') bangYeuCauCungCapTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;

    @Input() phongBanList: PhongBanDto[];

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    bangYeuCauCungCapTaiSan: BangYeuCauCungCapTaiSanInput = new BangYeuCauCungCapTaiSanInput();

    constructor(
        injector: Injector,
        private _bangYeuCauCungCapTaiSanService: BangYeuCauCungCapTaiSanServiceProxy
    ) {
        super(injector);
    }

    show(bangYeuCauCungCapTaiSanId?: number | null | undefined): void {
        this.saving = false;

        this._bangYeuCauCungCapTaiSanService.getBangYeuCauCungCapTaiSanForEdit(bangYeuCauCungCapTaiSanId).subscribe(result => {
            this.bangYeuCauCungCapTaiSan = result;
            this.modal.show();
        })
    }

    save(): void {
        let input = this.bangYeuCauCungCapTaiSan;
        this.saving = true;

        input.ngayYeuCau = moment(input.ngayYeuCau + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
        input.ngayYeuCau.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước

        this._bangYeuCauCungCapTaiSanService.createOrEditBangYeuCauCungCapTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}