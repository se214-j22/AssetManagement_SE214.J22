import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThanhLyServiceProxy, ThanhLyInput, TaiSanDto, TaiSanServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTaiSanModalComponent } from '../taisan/create-or-edit-taisan-modal.component';
import { TaiSanFindComponent } from '../taisan/taisan-find.component';


@Component({
    selector: 'createOrEditThanhLyModal',
    templateUrl: './create-or-edit-thanhly-modal.component.html'
})
export class CreateOrEditThanhLyModalComponent extends AppComponentBase implements OnInit{

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thanhLyCombobox') thanhLyCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewThongTinTaiSan') viewThongTinTaiSan: TaiSanFindComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thanhLy: ThanhLyInput = new ThanhLyInput();
    taisan: TaiSanDto = new TaiSanDto();
    ngayXuat: number;
    listTenDonVi: string[]
    listTenNhanVien: string[]

    constructor(
        injector: Injector,
        private _thanhLyService: ThanhLyServiceProxy,
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
            this.thanhLy.maTS = this.taisan.maTS;
            this.thanhLy.tenTS = this.taisan.tenTs;
        }
    }

    show(thanhLyId?: number | null | undefined): void {
        this.saving = false;

        this._thanhLyService.getThanhLyForEdit(thanhLyId).subscribe(result => {
            this.thanhLy = result;

            this._taiSanService.getTaiSanForEdit(result.id).subscribe(kq => {
                this.taisan = kq;
            });

            this.modal.show();

        })
    }

    getTenDonVi(): void {
        this._thanhLyService.getTenDonVi().subscribe(
            result => {
                this.listTenDonVi = result['result'];
            })
    }

    save(): void {
        
        let input = this.thanhLy;
        this.saving = true;
        this._thanhLyService.createOrEditThanhLy(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
