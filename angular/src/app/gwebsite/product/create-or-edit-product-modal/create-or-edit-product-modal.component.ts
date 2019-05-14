import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ProductsServiceProxy, SupplierServiceProxy, BiddingSaved } from '@shared/service-proxies/service-proxies';
import { SelectItem } from 'primeng/primeng';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditProductModal',
    templateUrl: './create-or-edit-product-modal.component.html',
    styleUrls: ['./create-or-edit-product-modal.component.css']
})
export class CreateOrEditProductModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('productCombobox') productCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    biddingTypes = [
        { label: 'Select bidding type', value: null },
        { label: 'Đấu thầu', value: 0 },
        { label: 'Chuyển nhượng', value: 1 },
        { label: 'Gì đó', value: 2 }

    ];
    status = [
        { label: 'Select status', value: null },
        { label: 'Trúng thầu', value: 1 },
        { label: 'Dự thầu', value: 2 },
        { label: 'Hết hạn', value: 3 }

    ];
    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    bidding: BiddingSaved = new BiddingSaved({ productId: 0, endDate: null, status: 0, supplierId: 0, startDate: null, price: 0, biddingType: 0 });
    selectItems: SelectItem[] = [];
    suppliers: SelectItem[] = [];
    rangeDates: Date[];
    constructor(
        injector: Injector,
        private _productsServiceProxy: ProductsServiceProxy,
        private _supplierServiceProxy: SupplierServiceProxy
    ) {
        super(injector);
        this.getDataProduct();
        this.suppliers.push({ value: '', label: 'Select supplier' });
    }

    show(productId?: number | null | undefined): void {
        this.active = true;
        this.modal.show();
    }

    save(): void {
        this.saving = true;
        this.bidding.startDate = this.rangeDates ? moment(this.rangeDates[0]) : moment(new Date());
        this.bidding.endDate = this.rangeDates && this.rangeDates.length > 1 ? moment(this.rangeDates[1]) : moment(new Date());
        // this.bidding.status = 0;
        this._supplierServiceProxy.changeOwnerBiddingProduct(this.bidding).subscribe(item => {
            this.close();
            this.modalSave.emit(null);
            console.log(item);
        });

    }
    dropdownChange() {
        this.getSupplierByProduct();
    }
    getDataProduct() {
        this._productsServiceProxy.getProducts('', '', 1000, 0).subscribe(products => {
            this.selectItems = [];
            this.selectItems.push({ value: '', label: 'Select product' });
            products.items.map(i => this.selectItems.push({ value: i.id, label: i.name }));
        });
    }

    getSupplierByProduct() {
        this._supplierServiceProxy.getSupplierByProduct('', '', 1000, 0, +this.bidding.productId).subscribe(suppliers => {
            this.suppliers = [];
            this.suppliers.push({ value: '', label: 'Select supplier' });
            suppliers.items.map(i => this.suppliers.push({ value: i.id, label: i.name }));
        });
    }
    dropdownSupplierChange() {
        console.log(this.bidding);
    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
