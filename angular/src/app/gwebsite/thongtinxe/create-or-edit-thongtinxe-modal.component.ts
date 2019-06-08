import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ThongTinXeInput, ThongTinXeServiceProxy, TaiSanInput, TaiSanServiceProxy, TaiSanDto, ModelDto, ModelServiceProxy, ThietBiKemTheoInput, ThietBiKemTheoDto, ThietBiKemTheoServiceProxy, OrganizationUnitDto, OrganizationUnitServiceProxy, CheckServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaiSanComponent } from '../taisan/taisan.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ModelComponent } from '../model/model.component';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditThongTinXeModal',
    templateUrl: './create-or-edit-thongtinxe-modal.component.html',
    styleUrls: ['./create-or-edit-thongtinxe.component.css'],
    animations: [appModuleAnimation()]
})
export class CreateOrEditThongTinXeModalComponent extends AppComponentBase implements AfterViewInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('viewTaiSan') viewTaiSan: TaiSanComponent;
    @ViewChild('viewModel') viewModel: ModelComponent;
    @ViewChild('customerCombobox') customerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    thietbikemmtheo: ThietBiKemTheoInput = new ThietBiKemTheoInput();
    tbkts: ThietBiKemTheoDto[] = [];
    taisan: TaiSanDto = new TaiSanDto();
    taisanItem: TaiSanDto = new TaiSanDto();
    model: ModelDto = new ModelDto();
    modelItem: ModelDto = new ModelDto();
    ngaydangkibandau: Date;
    check: boolean = false;
    organizationUnits: OrganizationUnitDto[];
    isDuyet: boolean;

    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
        private _taisanService: TaiSanServiceProxy,
        private _tbktService: ThietBiKemTheoServiceProxy,
        private _organizationUnitServiceProxy: OrganizationUnitServiceProxy,
        private _isDuyet: CheckServiceProxy
    ) {
        super(injector);
        _isDuyet.isDuyet().subscribe(result => {
            this.isDuyet = result;
        })
    }

    ngAfterViewInit(): void {
        this._organizationUnitServiceProxy.getOrganizationUnitDtosForTree().subscribe(response => {
            var selectedOrganization = [];
            this.organizationUnits = response;
        })
    }
    GetTaiSan(taisan: TaiSanDto) {
        if (taisan.maTaiSan != undefined) {

            this.taisan = taisan;
        }
    }

    GetModel(model: ModelDto) {
        if (model.model != undefined)
            this.model = model;
    }

    show(soXe?: string | null | undefined): void {
        this.saving = false;

        this._thongtinxeService.getThongTinSeForEdit(soXe).subscribe(result => {
            this.thongtinxe = result;
            if (this.thongtinxe.trangThaiDuyet === "Đã duyệt")
                this.check = true;
            else
                this.check = false;
            if (result.id != undefined)
                this.ngaydangkibandau = result.ngayDangKiBanDau.toDate();
            this._taisanService.getTaiSanForEdit(result.maTaiSan).subscribe(kq => {
                this.taisan = kq;
                console.log("hix", kq);
            });
            this._modelService.getModelForEdit(result.model).subscribe(kq2 => {
                this.model = kq2;
            });
            if (result.soXe) {
                this._tbktService.getThietBiKemTheosByFilter(result.soXe, undefined, undefined, undefined).subscribe(kq3 => {
                    this.tbkts = kq3.items;
                })
            }

            this.modal.show();
        })
    }

    onKeydown(event, index: number): void {
        if (event.key === "Enter") {
            let input = this.tbkts[index];
            this._tbktService.createOrEditThietBiKemTheo(input).subscribe(
            );
        }
    }



    ThemThietBi(): void {
        let input = new ThietBiKemTheoDto();
        input.soLuong = 0;
        // input.thietBiKemTheo = null;
        // input.dienGiai = null;
        input.soXe = this.thongtinxe.soXe;
        this.tbkts.push(input);
    }


    save(): void {
        if (this.check) {
            this.thongtinxe.trangThaiDuyet = "Đã duyệt"
        }
        else
            this.thongtinxe.trangThaiDuyet = "Chưa duyệt"

        this.saving = true;
        this.thongtinxe.ngayDangKiBanDau = moment(this.ngaydangkibandau);
        this.thongtinxe.maTaiSan = this.taisan.maTaiSan;
        this.thongtinxe.model = this.model.model;
        let input = this.thongtinxe;
        this._thongtinxeService.createOrEditThongTinXe(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            console.log("lasao" + this.thongtinxe.donViSuDung);
            this.close();
        })
    }

    Xoa(index: number): void {
        let temp = this.tbkts[index];
        this._tbktService.deleteThietBiKemTheo(temp.id).subscribe();
        if (this.thongtinxe.soXe) {
            this._tbktService.getThietBiKemTheosByFilter(this.thongtinxe.soXe, undefined, undefined, undefined).subscribe(kq3 => {
                this.tbkts = kq3.items;
            })
        }



    }
    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
        this.thongtinxe.model = this.model.model;
    }
}
