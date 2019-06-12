import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { NhanVienServiceProxy, NhanVienInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditNhanVienModal',
    templateUrl: './create-or-edit-nhanvien-modal.component.html'
})
export class CreateOrEditNhanVienModalComponent extends AppComponentBase implements OnInit{


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('nhanVienCombobox') nhanVienCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    nhanVien: NhanVienInput = new NhanVienInput();
    listTenDonVi: string[];

    constructor(
        injector: Injector,
        private _nhanVienService: NhanVienServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {        
        this.getTenDonVi();
    }

    getTenDonVi(): void {
        this._nhanVienService.getTenDonVi().subscribe(
            result => {
                this.listTenDonVi = result['result'];
            })
    }

    show(nhanVienId?: number | null | undefined): void {
        this.saving = false;


        this._nhanVienService.getNhanVienForEdit(nhanVienId).subscribe(result => {
            this.nhanVien = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.nhanVien;
        this.saving = true;
        this._nhanVienService.createOrEditNhanVien(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
