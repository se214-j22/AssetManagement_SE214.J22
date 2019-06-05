import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { XuatTaiSanServiceProxy, XuatTaiSanInput, TaiSanDto, TaiSanServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTaiSanModalComponent } from '../taisan/create-or-edit-taisan-modal.component';
import { TaiSanFindComponent } from '../taisan/taisan-find.component';


@Component({
    selector: 'createOrEditXuatTaiSanModal',
    templateUrl: './create-or-edit-xuattaisan-modal.component.html'
})
export class CreateOrEditXuatTaiSanModalComponent extends AppComponentBase implements OnInit{

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('xuatTaiSanCombobox') xuatTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewThongTinTaiSan') viewThongTinTaiSan: TaiSanFindComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    xuatTaiSan: XuatTaiSanInput = new XuatTaiSanInput();
    taisan: TaiSanDto = new TaiSanDto();
    ngayXuat: number;
    listTenDonVi: string[]
    listTenNhanVien: string[]

    constructor(
        injector: Injector,
        private _xuatTaiSanService: XuatTaiSanServiceProxy,
        private _taiSanService: TaiSanServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayXuat = Date.now();
        this.getTenDonVi();
    }

    getTaiSan(taisan: TaiSanDto) {
        if (taisan.id != undefined) {

            this.taisan = taisan
            this.xuatTaiSan.maTaiSan = this.taisan.id;
            this.xuatTaiSan.tenTaiSan = this.taisan.tenTs;
        }
    }

    show(xuatTaiSanId?: number | null | undefined): void {
        this.saving = false;

        this._xuatTaiSanService.getXuatTaiSanForEdit(xuatTaiSanId).subscribe(result => {
            this.xuatTaiSan = result;

            this._taiSanService.getTaiSanForEdit(result.id).subscribe(kq => {
                this.taisan = kq;
            });

            this.modal.show();

        })
    }

    getTenDonVi(): void {
        this._xuatTaiSanService.getTenDonVi().subscribe(
            result => {
                this.listTenDonVi = result['result'];
            })
    }

    getTenNhanVienTheoDV(): void {
        if (this.xuatTaiSan.tenDonVi == "...")
            return;
        
        this._xuatTaiSanService.getTenNhanVienTheoDV(this.xuatTaiSan.tenDonVi).subscribe(
            result => {
                this.listTenNhanVien = result['result'];
            })
    }

    save(): void {
        
        let input = this.xuatTaiSan;
        this.saving = true;
        this._xuatTaiSanService.createOrEditXuatTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
