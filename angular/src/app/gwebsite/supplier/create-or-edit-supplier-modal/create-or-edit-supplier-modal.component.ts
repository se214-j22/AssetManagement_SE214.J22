import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { SupplierDto, ApprovalStatusEnum, NewPJDto, SupplierTypeInfo } from '../dto/supplier.dto';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditSupplierModal',
    templateUrl: './create-or-edit-supplier-modal.component.html',
    styleUrls: ['./create-or-edit-supplier-modal.component.css']
})
export class CreateOrEditSupplierModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('supplierCombobox') supplierCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    supplier: SupplierDto = new SupplierDto();
    suppliers: ComboboxItemDto[] = [];

    public pjCode = '';
    public pjName = '';
    public supplierTypeId: number;
    public pjCreateDate = '';
    public pjActiveDate = '';
    public pjAddress = '';
    public pjEmail = '';
    public pjFax = '';
    public pjPhone = '';
    public pjContact = '';
    public pjDescription = '';

    public supplierTypes = [
        {
            id: 1,
            code: 'F001',
            name: 'Computer Screen'
        },
        {
            id: 2,
            code: 'F002',
            name: 'Computer CPU'
        },
        {
            id: 3,
            code: 'G001',
            name: 'Fridge'
        }
    ];

    public supplierTypeInfoList = [];

    public isCheckActive = false;
    public statusEnum = ApprovalStatusEnum;
    public newSupplier: NewPJDto;

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    show(supplierId?: number | null | undefined): void {
        this.active = true;
        this.saving = false;

        this.pjCode = '';
        this.pjName = '';
        this.isCheckActive = false;
        this.pjAddress = '';
        this.pjFax = '';
        this.pjPhone = '';
        this.pjContact = '';
        this.pjDescription = '';

        let now = new Date();
        this.pjCreateDate = moment(now).format('DD/MM/YYYY');

        this.supplierTypeId = this.supplierTypes[0].id;
        this.supplierTypeInfoList = [];

        this.supplierTypes.forEach((item, i) => {
            this.supplierTypeInfoList.push(
                new SupplierTypeInfo(item.id, `${item.code} - ${item.name}`));
        });

        this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', supplierId).subscribe(result => {
            this.supplier = result.menuClient;
            this.suppliers = result.menuClients;
            this.modal.show();
            setTimeout(() => {
                $(this.supplierCombobox.nativeElement).selectpicker('refresh');
            }, 0);
        });
    }

    save(): void {
        if (this.pjCode && this.pjCode !== '' && this.pjName && this.pjName !== '') {
            this.saving = true;

            let status = this.isCheckActive ? this.statusEnum.Active : this.statusEnum.Inactive;

            //createDate: BE lấy giờ hệ thống
            this.newSupplier = new NewPJDto(this.pjCode, this.pjName, this.supplierTypeId, this.pjAddress,
                this.pjEmail, this.pjFax, this.pjPhone, this.pjContact, this.pjDescription, status);


            console.log(this.pjCode + '--' + this.pjName + '--' + this.supplierTypeId + '--' + this.pjAddress
                + '--' + this.pjEmail + '--' + this.pjFax + '--' + this.pjPhone + '--' + this.pjContact + '--' +
                this.pjDescription + '--' + status);

            // this.insertSupplier();

            // call api create product category theo code,nam,status
            // add xuống, id tự tạo

            //trước khi add nhớ check duplicat code.


            this.close();
        }
    }

    insertSupplier() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.post('api/MenuClient/CreateMenuClient', this.supplier)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    updateSupplier() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.put('api/MenuClient/UpdateMenuClient', this.supplier)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    activeNewPrj(event: Event): void {
        if (this.isCheckActive) {
            this.pjActiveDate = this.pjCreateDate;
        }
    }
}
