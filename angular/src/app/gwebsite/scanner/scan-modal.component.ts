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
    isNull: Boolean;
    sanPham: SanPhamInput = new SanPhamInput();

    constructor(
        injector: Injector,
        private _sanPhamService: SanPhamServiceProxy
    ) {
        super(injector);
    }
    
    public showInfo(sanPhamId?: string | null | undefined): void {
        this.saving = false;


        this._sanPhamService.getSanPhamForEditMaSP(sanPhamId).subscribe(result => {
            this.sanPham = result;
            
            if (this.sanPham != null)
                {
                    this.isNull = false;
                    this.modal.show();
                }

        })
    }
    save(): void {
        let input = this.sanPham;
        this.saving = true;

        
        let now = new Date();
        
        input.ngayCapNhat= moment(now); // moment phải có thời gian thì mới gọi .toISOString() được
        input.ngayCapNhat.utcOffset(0, true);
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
