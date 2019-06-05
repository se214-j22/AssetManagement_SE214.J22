import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { XuatTaiSanServiceProxy, XuatTaiSanInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditXuatTaiSanModal',
    templateUrl: './create-or-edit-xuattaisan-modal.component.html'
})
export class CreateOrEditXuatTaiSanModalComponent extends AppComponentBase implements OnInit{

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('xuatTaiSanCombobox') xuatTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    xuatTaiSan: XuatTaiSanInput = new XuatTaiSanInput();
    ngayXuat: number;

    constructor(
        injector: Injector,
        private _xuatTaiSanService: XuatTaiSanServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.ngayXuat = Date.now();        
    }

    show(xuatTaiSanId?: number | null | undefined): void {
        this.saving = false;


        this._xuatTaiSanService.getXuatTaiSanForEdit(xuatTaiSanId).subscribe(result => {
            this.xuatTaiSan = result;
            this.modal.show();

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
