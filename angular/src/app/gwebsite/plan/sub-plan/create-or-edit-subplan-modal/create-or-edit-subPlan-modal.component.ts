import { Component, OnInit, AfterViewChecked, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { SubPlanDto, ProductSubPlanDto, PurchaseProducts, StatusEnum } from '../../dto/plan.dto';

@Component({
    selector: 'createOrEditSubPlanModal',
    templateUrl: './create-or-edit-subPlan-modal.component.html',
    styleUrls: ['./create-or-edit-subPlan-modal.component.css']
})
export class CreateOrEditSubPlanModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('subPlanCombobox') subPlanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    isEdit = false;

    purchaseProducts: PurchaseProducts = new PurchaseProducts();
    products: ComboboxItemDto[] = [];


    //because 'create' will create new product for this plan
    //=> get all productCodes not assigned for this plan
    //results include: productCode, productName, CalUnit
    public productsNotAssignThisPlan = [
        {
            productCode: 'F001',
            productName: 'Computer Screen',
            calUnit: 'Int',
            unitPrice: 20000
        },
        {
            productCode: 'F002',
            productName: 'Computer CPU',
            calUnit: 'Char',
            unitPrice: 50000
        },
        {
            productCode: 'G001',
            productName: 'Fridge',
            calUnit: 'Float',
            unitPrice: 30000
        },
        {
            productCode: 'G002',
            productName: 'Water Purifier',
            calUnit: 'Bool',
            unitPrice: 40000
        }
    ];

    public productInfoList: ProductSubPlanDto[] = [];
    public productCode = '';
    public quantity = 0;
    public planId = 0;
    public newProduct: SubPlanDto;

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }


    // onChangeProductCode(): void {
    //     console.log(this.productCode + '--' + this.quantity);
    // }

    show(planId: number): void {
        // this._apiService.get('api/Products/GetProducts').subscribe(result => {
        //     this.products = result.items;
        //     this.modal.show();
        //     setTimeout(() => {
        //         $(this.subPlanCombobox.nativeElement).selectpicker('refresh');
        //     }, 0);
        // });

        // get all productCodes not assigned for this plan
        // by planId
        if (planId) {
            this.planId = planId;
            // this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', planId).subscribe(result => {
            //     this.productsNotAssignThisPlan = result.products;
            //     this.modal.show();
            //     setTimeout(() => {
            //         $(this.subPlanCombobox.nativeElement).selectpicker('refresh');
            //     }, 0);
            // });

            this.saving = false;
            this.productCode = this.productsNotAssignThisPlan[0].productCode;
            this.productInfoList = [];
            this.quantity = 0;

            this.productsNotAssignThisPlan.forEach((item, i) => {
                this.productInfoList.push(new ProductSubPlanDto(item.productCode,
                    `${item.productCode} - ${item.productName} - VND${item.unitPrice}/${item.calUnit}`));
            });

            this.modal.show();
            this.active = true;
        }
    }

    save(): void {
        this.saving = true;

        this.newProduct = new SubPlanDto(this.planId, this.productCode, this.quantity);

        //call api create, send newProduct
        // if (this.planId) {
        //     this.updateSubPlan();
        // } else {
        //     this.insertSubPlan();
        // }

        //post this.newProduct

        // console.log(this.newProduct.planId + '--' + this.newProduct.productCode + '--' + this.newProduct.quantity);
        // console.log(this.productCode + '--' + this.quantity);

        this.close();
    }

    insertSubPlan() {
        this._apiService.post('api/Purchase/CreatePurchase', this.newProduct)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    updateSubPlan() {
        this._apiService.put('api/MenuClient/UpdateMenuClient', this.newProduct)
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
}
