import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ProductsServiceProxy, SupplierSavedDto, Bidding, BiddingSaved, SupplierServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { SelectItem } from 'primeng/primeng';

@Component({
    selector: 'app-create-or-edit-supplier',
    templateUrl: './create-or-edit-supplier.component.html',
    styleUrls: ['./create-or-edit-supplier.component.css']
})
export class CreateOrEditSupplierComponent extends AppComponentBase {
    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('purchaseCombobox') purchaseCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    isEdit = false;
    active = false;
    saving = false;
    supplier: SupplierSavedDto = new SupplierSavedDto({ address: '', biddings: [], contact: '', email: '', fax: '', id: 0, name: '', phone: '00000000990' });
    biddings: any[] = [];
    productId = '';
    selectItems: SelectItem[];
    biddingTypes = [
        { label: 'Bidding type', value: null },
        { label: 'Đấu thầu', value: 0 },
        { label: 'Chuyển nhượng', value: 1 },
        { label: 'Gì đó', value: 2 }

    ];
    status = [
        { label: 'Status', value: null },
        { label: 'Trúng thầu', value: 1 },
        { label: 'Dự thầu', value: 2 },
        { label: 'Hết hạn', value: 3 }

    ];
    rangeDates: Date[];
    constructor(
        injector: Injector,
        private _productsServiceProxy: ProductsServiceProxy,
        private _supplierServiceProxy: SupplierServiceProxy
    ) {
        super(injector);
        this.getDataProduct();
    }

    show(supplier?: any | null | undefined): void {
        this.active = true;
        this.supplier = supplier ? supplier : this.supplier;
        this.isEdit = supplier ? true : false;
        this.modal.show();
    }

    save(): void {

        let input = this.supplier;
        this.saving = true;
        if (input.id) {
            this.updatePurchase();
        } else {
            this.insertPurchase();
        }
    }
    getDataProduct() {
        this._productsServiceProxy.getProducts('', '', 1000, 0).subscribe(products => {
            this.selectItems = [];
            products.items.map(i => this.selectItems.push({ value: { id: i.id }, label: i.name }));
        });
    }
    insertPurchase() {
        let input = Object.create(this.supplier);
        input.biddings = [];
        this.biddings.map(item => {
            const startDate = item.ranges ? item.ranges[0] : new Date();
            const endDate = item.ranges ? item.ranges[1] ? item.ranges[1] : new Date() : new Date();
            input.biddings.push(new BiddingSaved({
                startDate, endDate,
                status: item.status, productId: item.id, supplierId: input.id, biddingType: item.biddingType, price: item.price
            }));
        });

        this._supplierServiceProxy.createSupplier(input).subscribe(item => {
            this.close();
            this.modalSave.emit(null);
        }, err => console.log(err));
    }



    updatePurchase() {
        let input = Object.create(this.supplier);
        // input.biddings = [];
        // this.biddings.map(item => {
        //     const startDate = item.ranges ? item.ranges[0] : new Date();
        //     const endDate = item.ranges ? item.ranges[1] ? item.ranges[1] : new Date() : new Date();
        //     input.biddings.push(new BiddingSaved({
        //         startDate, endDate,
        //         status: 0, productId: item.id, supplierId: input.id
        //     }));
        // })
        this._supplierServiceProxy.updateSupplier(input).subscribe(item => {
            this.close();
            this.modalSave.emit(null);
        }, err => console.log(err));
    }
    onChangeOptionProduct() {
        console.log('sss', this.biddingTypes);
    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
