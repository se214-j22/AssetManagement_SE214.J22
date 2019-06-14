import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { SuaChuaServiceProxy, SuaChuaInput, TaiSanDto, TaiSanServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaiSanFindXuatComponent } from '../taisan/taisan-find-xuat.component';


@Component({
    selector: 'createOrEditSuaChuaModal',
    templateUrl: './create-or-edit-suachua-modal.component.html'
})
export class CreateOrEditSuaChuaModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('suaChuaCombobox') suaChuaCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('viewTaiSanFindXuatModel') viewTaiSanFindXuatModel: TaiSanFindXuatComponent;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    suaChua: SuaChuaInput = new SuaChuaInput();
    taiSanXuat: TaiSanDto = new TaiSanDto();
    listTenDVSC: string[]
    listTenNVPT: string[]
    listTenNVDX: string[]
    tenDVDX: string
    ngayXuat: number

    constructor(
        injector: Injector,
        private _suaChuaService: SuaChuaServiceProxy,
        private _taiSanService: TaiSanServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayXuat = Date.now();
        this.getTenDVSC();
        this.getTenNVDX();
    }

    getTaiSans(taiSanXuat: TaiSanDto) {
        if (taiSanXuat.id != undefined) {

            this.taiSanXuat = taiSanXuat
            this.suaChua.maTS = this.taiSanXuat.maTS;
            this.suaChua.tenTaiSan = this.taiSanXuat.tenTs;            
        }
    }

    getTenDVSC(): void {
        this._suaChuaService.getTenDVSC().subscribe(
            result => {
                this.listTenDVSC = result['result'];
            })
    }

    getTenNVPT(tenDV: string): void {
        this._suaChuaService.getTenNVPT(tenDV).subscribe(
            result => {
                this.listTenNVPT = result['result'];
            })
    }

    getTenNVDX(): void {
        this._suaChuaService.getTenNVDX().subscribe(
            result => {
                this.listTenNVDX = result['result'];
            })
    }

    getTenDVDX(): void {
        this._suaChuaService.getTenDVDX(this.suaChua.tenNhanVienDX).subscribe(
            result => {
                this.tenDVDX = result['result'];
                this.suaChua.tenDVDeXuat = this.tenDVDX;
            })        
    }

    //getTenNVNhan(): void {
    //    if (this.suaChua.tenDonViNhan == "...")
    //        return;

    //    this._suaChuaService.getTenNVNhan(this.suaChua.tenDonViNhan).subscribe(
    //        result => {
    //            this.listTenNVNhan = result['result'];
    //        })
    //}

    show(suaChuaId?: number | null | undefined): void {
        this.saving = false;


        this._suaChuaService.getSuaChuaForEdit(suaChuaId).subscribe(result => {
            this.suaChua = result;

            this._taiSanService.getTaiSanForEdit(result.id).subscribe(kq => {
                this.taiSanXuat = kq;
            });

            this.modal.show();

        })
    }

    save(): void {
        let input = this.suaChua;
        this.saving = true;
        this._suaChuaService.createOrEditSuaChua(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
