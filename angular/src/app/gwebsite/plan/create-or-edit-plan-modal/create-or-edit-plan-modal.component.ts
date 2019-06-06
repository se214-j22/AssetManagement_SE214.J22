import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { PlanDto, NewPlanProducts, NewProductAddList, ProductSubPlanDto, UserInfo, PurchaseProducts, StatusEnum } from '../dto/plan.dto';

@Component({
    selector: 'createOrEditPlanModal',
    templateUrl: './create-or-edit-plan-modal.component.html',
    styleUrls: ['./create-or-edit-plan-modal.component.css']
})
export class CreateOrEditPlanModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('planCombobox') planCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;


    plan: PlanDto = new PlanDto();
    purchaseProducts: PurchaseProducts = new PurchaseProducts();
    products: ComboboxItemDto[] = [];

    public statusData = [
        {
            id: StatusEnum.Draft,
            name: 'Draft'
        },
        {
            id: StatusEnum.Official,
            name: 'Official '
        }
    ];

    public unitDepartment = '';
    public UserInfo: UserInfo;
    public FakeDataUser = {
        unitCode: 'HN',
        departmentCode: 'IT'
    };

    //get all product
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
    public quantity = 0;
    public newPlanProductList: NewPlanProducts[] = [];
    public newProductTableList: NewProductAddList[] = [];
    public isAdd = false;
    public emptyText = '';
    public productCode = '';
    public isExistAdd = false;

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        // this._apiService.get('api/Products/GetProducts').subscribe(result => {
        //     this.productsNotAssignThisPlan = result.products;
        //     this.modal.show();
        //     setTimeout(() => {
        //             $(this.planCombobox.nativeElement).selectpicker('refresh');
        //     }, 0);
        // });

        // this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', planId).subscribe(result => {
        //     this.plan = result.menuClient;
        //     this.modal.show();
        //     setTimeout(() => {
        //             $(this.planCombobox.nativeElement).selectpicker('refresh');
        //     }, 0);
        // });

        //get unit code, department code by user id
        this.UserInfo = this.FakeDataUser;
        this.unitDepartment = `${this.UserInfo.unitCode} - ${this.UserInfo.departmentCode}`;

        //get all product
        this.newPlanProductList = [];
        this.productInfoList = [];
        this.productCode = '';
        this.quantity = 0;
        this.saving = false;
        this.isAdd = false;
        this.newProductTableList = [];

        this.productsNotAssignThisPlan.forEach((item, i) => {
            this.productInfoList.push(new ProductSubPlanDto(item.productCode,
                `${item.productCode} - ${item.productName} - VND${item.unitPrice}/${item.calUnit}`));
        });

        this.modal.show();
        this.active = true;
    }

    addProduct(): void {
        if (this.isAdd && this.productCode && this.productCode !== '' && this.quantity > 0) {
            this.newProductTableList.push(new NewProductAddList(this.productCode, this.quantity, false));
            this.productInfoList = this.productInfoList.filter(x => x.productCode !== this.productCode);
            this.isAdd = false;
            //newPlanProductList exist >= 1item
            if (!this.isExistAdd) {
                this.isExistAdd = true;
            }
        }
    }

    editQuantity(row: NewProductAddList): void {
        row.isEdit = true;
    }

    saveEditQuantity(row: NewProductAddList): void {
        row.isEdit = false;
    }

    removeProduct(i: number): void {
        this.newProductTableList.splice(i, 1);
    }

    activeAddProduct(): void {
        if (!this.isAdd) {
            this.quantity = 0;
            this.productCode = '';
            this.isAdd = true;
        }
    }

    cancelAdd(): void {
        this.isAdd = false;
    }

    save(): void {
        let input = this.plan;
        this.saving = true;

        //post xuống với url theo userId và model là listproduct

        // post this.newPlanProductList;
        // BE sẽ lưu ở cả table Plan và SubPlan
        this.newPlanProductList = [];
        this.newProductTableList.forEach((item, i) => {
            this.newPlanProductList.push(new NewPlanProducts(item.productCode, item.quantity));
        });

        if (this.newPlanProductList.length && this.newPlanProductList.length > 0) {
            this.newPlanProductList.forEach((item, i) => {
                console.log(item.productCode + '---' + item.quantity);
            });
        }

        // if (input.id) {
        //     this.updatePlan();
        // } else {
        //     this.insertPlan();
        // }

        this.close();
    }

    insertPlan() {
        this._apiService.post('api/Purchase/CreatePurchase', this.plan)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    updatePlan() {
        this._apiService.put('api/MenuClient/UpdateMenuClient', this.plan)
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
