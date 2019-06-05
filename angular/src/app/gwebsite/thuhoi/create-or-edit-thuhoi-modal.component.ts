import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThuHoiServiceProxy, ThuHoiInput, CTDonViDto, CTDonViServiceProxy } from '@shared/service-proxies/service-proxies';
import { CTDonViComponent } from '../donvi/donvi-chitiet.component';


@Component({
    selector: 'createOrEditThuHoiModal',
    templateUrl: './create-or-edit-thuhoi-modal.component.html'
})
export class CreateOrEditThuHoiModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('thuHoiCombobox') thuHoiCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewCTDonVi') viewCTDonVi: CTDonViComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thuHoi: ThuHoiInput = new ThuHoiInput();
    ctDonVi: CTDonViDto = new CTDonViDto();
    ngayThuHoi: number;

    constructor(
        injector: Injector,
        private _thuHoiService: ThuHoiServiceProxy,
        private _ctDonViService: CTDonViServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayThuHoi = Date.now();
    }

    getCTDonVi(ctDonVi: CTDonViDto) {
        if (ctDonVi.id != undefined) {

            this.ctDonVi = ctDonVi
            this.thuHoi.maTS = this.ctDonVi.maTS;
            this.thuHoi.tenTaiSan = this.ctDonVi.tenTaiSan;
            this.thuHoi.maDV = this.ctDonVi.maDV;
            this.thuHoi.tenDonVi = this.ctDonVi.tenDonVi;
        }
    }

    show(thuHoiId?: number | null | undefined): void {
        this.saving = false;


        this._thuHoiService.getThuHoiForEdit(thuHoiId).subscribe(result => {
            this.thuHoi = result;

            this._ctDonViService.getCTDonViForEdit(result.id).subscribe(kq => {
                this.ctDonVi = kq;
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
