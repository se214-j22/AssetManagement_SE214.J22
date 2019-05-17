import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective, DatePickerComponent } from 'ngx-bootstrap';
import { SanPhamInput, SanPhamServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment/moment.js';


@Component({
    selector: 'createOrEditSanPhamModal',
    templateUrl: './create-or-edit-san-pham-modal.component.html'
})
export class CreateOrEditSanPhamModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('sanPhamCombobox') sanPhamCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    sanPham: SanPhamInput = new SanPhamInput();

    constructor(
        injector: Injector,
        private _sanPhamService: SanPhamServiceProxy
    ) {
        super(injector);
    }

    show(sanPhamId?: number | null | undefined): void {
        this.saving = false;


        this._sanPhamService.getSanPhamForEdit(sanPhamId).subscribe(result => {
            this.sanPham = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.sanPham;
        this.saving = true;

        input.ngayTao = moment(input.ngayTao + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
        input.ngayTao.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước

        input.ngayCapNhat= moment(input.ngayCapNhat + ' ' + '0:01'); // moment phải có thời gian thì mới gọi .toISOString() được
        input.ngayCapNhat.utcOffset(0, true); // Khi gọi .toISOString() thì nó offset timezone nên phải set timezone là 0 trước

        this._sanPhamService.createOrEditSanPham(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
