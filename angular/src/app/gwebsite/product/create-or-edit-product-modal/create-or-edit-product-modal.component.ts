import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto, ProductsServiceProxy, ProductSavedCreate } from '@shared/service-proxies/service-proxies';
import { ProductDto, ApprovalStatusEnum, NewPJDto, ProductTypeInfo } from '../dto/product.dto';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditProductModal',
    templateUrl: './create-or-edit-product-modal.component.html',
    styleUrls: ['./create-or-edit-product-modal.component.css']
})
export class CreateOrEditProductModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    // @ViewChild('productCombobox') productCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    product: ProductDto = new ProductDto();
    products: ComboboxItemDto[] = [];

    public pjCode = '';
    public pjName = '';
    public pjCreateDate = '';
    public pjActiveDate = '';
    public pjUnitPrice = '';
    public pjCalUnit = '';
    public pjDescription = '';

    public productTypeId: number;

    public productTypes = [
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

    public supplierId: number;
    public suppliers = [
        {
            id: 1,
            code: 'S001',
            name: 'DMX'
        },
        {
            id: 2,
            code: 'S002',
            name: 'FPT'
        },
        {
            id: 3,
            code: 'S001',
            name: 'Fridge'
        }
    ];

    public productTypeInfoList = [];
    public supplierInfoList = [];

    public isCheckActive = false;
    public statusEnum = ApprovalStatusEnum;
    public newProduct: NewPJDto;

    constructor(
        injector: Injector,
        private _apiService: ProductsServiceProxy
    ) {
        super(injector);
    }

    show(productId?: number | null | undefined): void {
        this.active = true;
        this.saving = false;

        this.pjCode = '';
        this.pjName = '';
        this.isCheckActive = false;
        this.pjUnitPrice = '';
        this.pjCalUnit = '';
        this.pjDescription = '';

        let now = new Date();
        this.pjCreateDate = moment(now).format('DD/MM/YYYY');

        this.productTypeId = this.productTypes[0].id;
        this.productTypeInfoList = [];

        this.productTypes.forEach((item, i) => {
            this.productTypeInfoList.push(
                new ProductTypeInfo(item.id, `${item.code} - ${item.name}`));
        });

        this.supplierId = this.suppliers[0].id;
        this.supplierInfoList = [];

        this.suppliers.forEach((item, i) => {
            this.supplierInfoList.push(
                new ProductTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
        this.modal.show();

    }

    save(): void {
        if (this.pjCode && this.pjCode !== '' && this.pjName && this.pjName !== '') {
            this.saving = true;

            let status = this.isCheckActive ? this.statusEnum.Active : this.statusEnum.Inactive;

            //createDate: BE lấy giờ hệ thống
            this.newProduct = new NewPJDto(this.pjCode, this.pjName, this.productTypeId, this.supplierId, this.pjUnitPrice,
                this.pjCalUnit, this.pjDescription, status);


            console.log(this.pjCode + '--' + this.pjName + '--' + this.productTypeId + '--' + this.supplierId + '--' + this.pjUnitPrice
                + '--' + this.pjCalUnit + '--' + this.pjDescription + '--' + status);

            // this.insertProduct();
            this._apiService.createProductAsync(new ProductSavedCreate({ name: this.pjName, address: 'aaa', calUnit: this.pjCalUnit, code: this.pjCode, createDate: moment(new Date(this.pjCreateDate)), description: this.pjDescription, productTypeId: this.productTypeId, status: status, supplierId: this.supplierId, unitPrice: this.pjUnitPrice })).subscribe(item => {
                console.log(item);
                this.modalSave.emit(null);
            })
            // call api create product category theo code,nam,status
            // add xuống, id tự tạo

            //trước khi add nhớ check duplicat code.


            this.close();
        }
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
