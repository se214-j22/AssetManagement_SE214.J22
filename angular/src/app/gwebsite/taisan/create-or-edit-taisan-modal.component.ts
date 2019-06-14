import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { TaiSanServiceProxy, TaiSanInput, NhomTaiSanInput } from '@shared/service-proxies/service-proxies';
import { DatePipe } from '@angular/common';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { NhomTaiSanComponent } from '../nhomtaisan/nhomtaisan.component';


@Component({
    selector: 'createOrEditTaiSanModal',
    templateUrl: './create-or-edit-taisan-modal.component.html'
})
export class CreateOrEditTaiSanModalComponent extends AppComponentBase implements OnInit{

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('taiSanCombobox') taiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    taiSan: TaiSanInput = new TaiSanInput();
    ngayNhap: number;
    listTenNhomTaiSan: string[];
    khauHao: NhomTaiSanInput = new NhomTaiSanInput();

    constructor(
        injector: Injector,
        private _taiSanService: TaiSanServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayNhap = Date.now();
    }

    show(taiSanId?: number | null | undefined): void {
        this.saving = false;

        this._taiSanService.getTaiSanForEdit(taiSanId).subscribe(result => {
            this.taiSan = result;
            this.modal.show();

        })
    }

    getTenNhomTaiSan(loaiTS: string): void {
        //if (this.taiSan.loaiTS == "...")
        //    this.taiSan.loaiTS = "";

        this._taiSanService.getTenNhomTaiSan(loaiTS).subscribe(
            result => {
                this.listTenNhomTaiSan = result['result'];
            })
    }

    getKhauHao(tenNhomTS: string): void {
        this._taiSanService.getKhauHao(tenNhomTS).subscribe(
            result => {
                this.khauHao = result['result'];
                this.taiSan.soThangKhauHao = this.khauHao.soThangKhauHao;
                this.taiSan.tyLeKhauHao = this.khauHao.tyLeKhauHao;
            })
    }

    save(): void {
        let input = this.taiSan;
        this.saving = true;
        this._taiSanService.createOrEditTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
