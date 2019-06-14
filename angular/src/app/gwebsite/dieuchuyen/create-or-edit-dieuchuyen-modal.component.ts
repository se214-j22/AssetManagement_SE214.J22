import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { DieuChuyenServiceProxy, DieuChuyenInput, TaiSanDto, TaiSanServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaiSanFindXuatComponent } from '../taisan/taisan-find-xuat.component';


@Component({
    selector: 'createOrEditDieuChuyenModal',
    templateUrl: './create-or-edit-dieuchuyen-modal.component.html'
})
export class CreateOrEditDieuChuyenModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('dieuChuyenCombobox') dieuChuyenCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewTaiSanFindXuatModel') viewTaiSanFindXuatModel: TaiSanFindXuatComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    dieuChuyen: DieuChuyenInput = new DieuChuyenInput();
    taiSanXuat: TaiSanDto = new TaiSanDto();
    listTenDV: string[]
    listTenNVNhan: string[]
    ngayDieuChuyen: number

    constructor(
        injector: Injector,
        private _dieuChuyenService: DieuChuyenServiceProxy,
        private _taiSanService: TaiSanServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayDieuChuyen = Date.now();
        this.getTenDV();
    }

    getTaiSans(taiSanXuat: TaiSanDto) {
        if (taiSanXuat.id != undefined) {

            this.taiSanXuat = taiSanXuat
            this.dieuChuyen.maTaiSan = this.taiSanXuat.maTS;
            this.dieuChuyen.tenTaiSan = this.taiSanXuat.tenTs;
            this.dieuChuyen.tenDonViDC = this.taiSanXuat.tenDV;
        }
    }

    getTenDV(): void {
        this._dieuChuyenService.getTenDV().subscribe(
            result => {
                this.listTenDV = result['result'];
            })
    }

    getTenNVNhan(): void {
        if (this.dieuChuyen.tenDonViNhan == "...")
            return;

        this._dieuChuyenService.getTenNVNhan(this.dieuChuyen.tenDonViNhan).subscribe(
            result => {
                this.listTenNVNhan = result['result'];
            })
    }

    show(dieuChuyenId?: number | null | undefined): void {
        this.saving = false;


        this._dieuChuyenService.getDieuChuyenForEdit(dieuChuyenId).subscribe(result => {
            this.dieuChuyen = result;

            this._taiSanService.getTaiSanForEdit(result.id).subscribe(kq => {
                this.taiSanXuat = kq;
            });

            this.modal.show();

        })
    }

    save(): void {
        let input = this.dieuChuyen;
        this.saving = true;
        this._dieuChuyenService.createOrEditDieuChuyen(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
