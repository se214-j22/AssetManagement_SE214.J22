import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';

export class PlanDto {
    id: number;
    purchaseProducts: PurchaseProducts;
    userId: number;
    status: number;
    comment: string;
    departmentId: number;
    raisedDate: string;
}

export class PurchaseProducts {
    quantity: number;
    productId: number;
}

export class GetPlanOutput {
    plan: PlanDto;
    plans: ComboboxItemDto[];
}

export enum StatusEnum {
    Draft = 1,
    Official = 2
}

export enum ApprovalStatusEnum {
    Approved = 1,
    AwaitingApproval = 2,
    AllStatus = 3
}

// create sub plan by model
export class SubPlanDto {
    planId: number;
    productCode: string;
    quantity: number;
    // month schedule: time now

    constructor(planId: number, productCode: string, quantity: number) {
        this.planId = planId;
        this.productCode = productCode;
        this.quantity = quantity;
    }
}

export class ProductSubPlanDto {
    productCode: string;
    productInfo: string;

    constructor(productCode: string, productInfo: string) {
        this.productCode = productCode;
        this.productInfo = productInfo;
    }
}

export class UserInfo {
    unitCode: string;
    departmentCode: string;

    constructor(unitCode: string, departmentCode: string) {
        this.unitCode = unitCode;
        this.departmentCode = departmentCode;
    }
}

export class NewPlanProducts {
    productCode: string;
    quantity: number;

    constructor(productCode: string, quantity: number) {
        this.productCode = productCode;
        this.quantity = quantity;
    }
}
export class NewProductAddList {
    productCode: string;
    quantity: number;
    isEdit: boolean;

    constructor(productCode: string, quantity: number, isEdit: boolean) {
        this.productCode = productCode;
        this.quantity = quantity;
        this.isEdit = isEdit;
    }
}