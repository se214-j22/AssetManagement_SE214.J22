import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { DieuChuyenServiceProxy, DieuChuyenInput, CTDonViDto, CTDonViServiceProxy } from '@shared/service-proxies/service-proxies';
import { CTDonViComponent } from '../donvi/donvi-chitiet.component';


@Component({
    selector: 'createOrEditDieuChuyenModal',
    templateUrl: './create-or-edit-dieuchuyen-modal.component.html'
})
export class CreateOrEditDieuChuyenModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('dieuChuyenCombobox') dieuChuyenCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewCTDonVi') viewCTDonVi: CTDonViComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    dieuChuyen: DieuChuyenInput = new DieuChuyenInput();
    ctDonVi: CTDonViDto = new CTDonViDto();
    listTenDV: string[]
    listTenNVNhan: string[]
    ngayDieuChuyen: number

    constructor(
        injector: Injector,
        private _dieuChuyenService: DieuChuyenServiceProxy,
        private _ctDonViService: CTDonViServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayDieuChuyen = Date.now();
        this.getTenDV();
    }

    getCTDonVi(ctDonVi: CTDonViDto) {
        if (ctDonVi.id != undefined) {

            this.ctDonVi = ctDonVi
            this.dieuChuyen.maTaiSan = this.ctDonVi.maTS;
            this.dieuChuyen.tenTaiSan = this.ctDonVi.tenTaiSan;
            this.dieuChuyen.tenNhanVienDC = this.ctDonVi.tenDonVi;
        }
    }

    getTenDV(): void {
        this._dieuChuyenService.getTenDV().subscribe(
            result => {
                this.listTenDV = result['result'];
            })
    }

    getTenNVNhan(): void {
        if (this.dieuChuyen.tenDonVi == "...")
            return;

        this._dieuChuyenService.getTenNVNhan(this.dieuChuyen.tenDonVi).subscribe(
            result => {
                this.listTenNVNhan = result['result'];
            })
    }

    show(dieuChuyenId?: number | null | undefined): void {
        this.saving = false;


        this._dieuChuyenService.getDieuChuyenForEdit(dieuChuyenId).subscribe(result => {
            this.dieuChuyen = result;

            this._ctDonViService.getCTDonViForEdit(result.id).subscribe(kq => {
                this.ctDonVi = kq;
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
