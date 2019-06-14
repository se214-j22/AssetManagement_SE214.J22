import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThuHoiServiceProxy, ThuHoiInput, TaiSanDto, TaiSanServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaiSanFindXuatComponent } from '../taisan/taisan-find-xuat.component';


@Component({
    selector: 'createOrEditThuHoiModal',
    templateUrl: './create-or-edit-thuhoi-modal.component.html'
})
export class CreateOrEditThuHoiModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thuHoiCombobox') thuHoiCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewTaiSanFindXuatModel') viewTaiSanFindXuatModel: TaiSanFindXuatComponent;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thuHoi: ThuHoiInput = new ThuHoiInput();
    taiSanXuat: TaiSanDto = new TaiSanDto();
    ngayThuHoi: number;

    constructor(
        injector: Injector,
        private _thuHoiService: ThuHoiServiceProxy,
        private _taiSanService: TaiSanServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayThuHoi = Date.now();
    }

    getTaiSans(taiSanXuat: TaiSanDto) {
        if (taiSanXuat.id != undefined) {

            this.taiSanXuat = taiSanXuat
            this.thuHoi.maTS = this.taiSanXuat.maTS;
            this.thuHoi.tenTaiSan = this.taiSanXuat.tenTs;
            this.thuHoi.tenDonVi = this.taiSanXuat.tenDV;
        }
    }

    show(thuHoiId?: number | null | undefined): void {
        this.saving = false;


        this._thuHoiService.getThuHoiForEdit(thuHoiId).subscribe(result => {
            this.thuHoi = result;

            this._taiSanService.getTaiSanForEdit(result.id).subscribe(kq => {
                this.taiSanXuat = kq;
            });

            this.modal.show();

        })
    }

    save(): void {
        let input = this.thuHoi;
        this.saving = true;
        this._thuHoiService.createOrEditThuHoi(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
