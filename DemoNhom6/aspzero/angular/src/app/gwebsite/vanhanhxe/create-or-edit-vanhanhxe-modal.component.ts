import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { QuanLyVanHanhInput, QuanLyVanHanhServiceProxy, ThongTinXeServiceProxy, ModelServiceProxy, ThongTinXeInput, ModelInput } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';




@Component({
    selector: 'createOrEditVanHanhXeModal',
    templateUrl: './create-or-edit-vanhanhxe-modal.component.html',
    styleUrls: ['./create-or-edit-vanhanhxe.component.css']
})
export class CreateOrEditVanHanhXeModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('customerCombobox') customerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;
    soKmMoi: number;
    ngayCapNhap: Date;

    vanhanhxe: QuanLyVanHanhInput = new QuanLyVanHanhInput();
    vanhanhxesau: QuanLyVanHanhInput = new QuanLyVanHanhInput();

    thongtinxe: ThongTinXeInput = new ThongTinXeInput();
    model: ModelInput = new ModelInput();
    km: number;
    xangdinhmuc: number;
    @Input() soXe: string;


    constructor(
        injector: Injector,
        private _vanhanhxeService: QuanLyVanHanhServiceProxy,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy
    ) {
        super(injector);
        this.vanhanhxesau = null;
    }

    show(Id?: number | null | undefined, idSau?: number | null | undefined): void {
        this.saving = false;


        this._thongtinxeService.getThongTinSeForEdit(this.soXe).subscribe(kq => {
            this.thongtinxe = kq;
            this._modelService.getModelForEdit(kq.model).subscribe(kq1 => {
                this.model = kq1;
            })
            if (Id !== -1) {

                this._vanhanhxeService.getQuanLyVanHanhForEdit(Id).subscribe(result => {
                    this.vanhanhxe = result;
                    //  this.vanhanhxe.soKM = this.vanhanhxe.kmMoi - this.vanhanhxe.kmCu;
                    this.soKmMoi = result.kmMoi;
                    this.xangdinhmuc = (this.model.dinhMucNhienLieu * this.vanhanhxe.soKM) / 100;
                    if (idSau !== -1) {
                        this._vanhanhxeService.getQuanLyVanHanhForEdit(idSau).subscribe(kq => {
                            this.vanhanhxesau = kq;
                        })
                    }

                    let day = moment(this.vanhanhxe.ngayCapNhat).format();
                    this.ngayCapNhap = new Date(day);

                })

            }
            else {
                this.vanhanhxe = new QuanLyVanHanhInput();
                this._vanhanhxeService.getQuanLyVanHanhsByFilter(this.soXe, null).subscribe(kq => {

                    if (kq === null)
                        this.vanhanhxe.kmCu = 0;
                    else
                        this.vanhanhxe.kmCu = kq.items[kq.items.length - 1].kmMoi;

                })

            }

            this.modal.show();
        })
    }
    onKeydown(event): void {
        if (event.key === "Enter") {

            this.vanhanhxe.soKM = this.vanhanhxe.kmMoi - this.vanhanhxe.kmCu;

            this.xangdinhmuc = (this.model.dinhMucNhienLieu * this.vanhanhxe.soKM) / 100;
            console.log("enter ne" + this.xangdinhmuc);

        }
    }
    TinhDinhMucNhienLieu(): void {

        this.vanhanhxe.soKM = this.vanhanhxe.kmMoi - this.vanhanhxe.kmCu;
        this.xangdinhmuc = (this.model.dinhMucNhienLieu * this.vanhanhxe.soKM) / 100;

    }
    save(): void {

        this.vanhanhxe.soXe = this.soXe;
        if (this.vanhanhxesau != null) {
            if (this.vanhanhxe.kmMoi !== this.soKmMoi) {
                this.vanhanhxesau.kmCu = this.vanhanhxe.kmMoi;
            }

            let inputsau = this.vanhanhxesau;
            this._vanhanhxeService.createOrEditQuanLyVanHanh(inputsau).subscribe(result => {

            })
        }

        this.vanhanhxe.ngayCapNhat = moment(this.ngayCapNhap);

        //alert(this.vanhanhxe.ngayCapNhat);
        let input = this.vanhanhxe;
        this.saving = true;
        this._vanhanhxeService.createOrEditQuanLyVanHanh(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
            //alert("again " + this.vanhanhxe.ngayCapNhat);
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
