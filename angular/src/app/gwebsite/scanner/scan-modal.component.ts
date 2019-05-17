import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { SanPhamInput, SanPhamServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment/moment.js';


@Component({
    selector: 'ScanModal',
    templateUrl: './scan-modal.component.html'
})
export class ScanModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('sanPhamCombobox') sanPhamCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;


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
    
    showInfo(sanPhamId?: string | null | undefined): void {
        this.saving = false;


        this._sanPhamService.getSanPhamForEditMaSP(sanPhamId).subscribe(result => {
            this.sanPham = result;
            this.modal.show();

        })
    }
    save(): void {
        let input = this.sanPham;
        this.saving = true;

        this._sanPhamService.createOrEditSanPham(input).subscribe(result => {
            this.notify.info(this.l('Scanned Successfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
