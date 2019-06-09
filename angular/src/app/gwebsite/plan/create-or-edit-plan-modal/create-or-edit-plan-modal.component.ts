import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto, User, PlanServiceProxy, PlanSavedDto, SubPlanSavedDto } from '@shared/service-proxies/service-proxies';
import { PlanDto, NewPlanProducts, NewProductAddList, ProductSubPlanDto, UserInfo, PurchaseProducts, StatusEnum } from '../dto/plan.dto';

@Component({
    selector: 'createOrEditPlanModal',
    templateUrl: './create-or-edit-plan-modal.component.html',
    styleUrls: ['./create-or-edit-plan-modal.component.css']
})
export class CreateOrEditPlanModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('planCombobox') planCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    user: User = new User();
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
            productCode: 6,
            productName: 'Computer Screen',
            calUnit: 'Int',
            unitPrice: 20000
        },
        {
            productCode: 7,
            productName: 'Computer CPU',
            calUnit: 'Char',
            unitPrice: 50000
        },
        {
            productCode: 8,
            productName: 'Fridge',
            calUnit: 'Float',
            unitPrice: 30000
        },
        {
            productCode: 9,
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
    public productCode = 0;
    public isExistAdd = false;

    constructor(
        injector: Injector,
        private _apiService: PlanServiceProxy
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this._apiService.currentUserInfo().subscribe(user => this.user = user);
    }
    show(): void {
        //get unit code, department code by user id
        this.UserInfo = this.FakeDataUser;
        this.unitDepartment = `${this.user.unitCode} - ${this.user.departmentCode}`;

        //get all product
        this.newPlanProductList = [];
        this.productInfoList = [];
        this.productCode = 0;
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
        if (this.isAdd && this.productCode && this.productCode !== 0 && this.quantity > 0) {
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
            this.productCode = 0;
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

        if (this.newPlanProductList.length && this.newPlanProductList.length > 0) {
            this.newPlanProductList.forEach((item, i) => {
                console.log(item.productCode + '---' + item.quantity);
            });
        }
        let planSaved: PlanSavedDto = new PlanSavedDto({ id: 0, subPlans: [] });

        this.newProductTableList.map((item) => planSaved.subPlans.push(new SubPlanSavedDto({ productId: item.productCode, quantity: item.quantity, planId: 0 })));
        this._apiService.createPlanAsync(planSaved).subscribe(item => console.log(item));

        this.close();
    }



    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
