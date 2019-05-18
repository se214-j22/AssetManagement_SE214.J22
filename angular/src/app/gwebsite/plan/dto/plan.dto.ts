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

