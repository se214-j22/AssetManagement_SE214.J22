import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';

export class BidProfileDto {
    id: number;
    name: string;
    alias: string;
    description: string;
    parentId: number | null;
    displayOrder: number | null;
    homeOrder: number | null;
    image: string;
    homeFlag: boolean | null;
    metaKeyword: string;
    metaDescription: string;
}

export class GetBidProfileOutput {
    bidProfile: BidProfileDto;
    bidProfiles: ComboboxItemDto[];
}

export enum ApprovalStatusEnum {
    Approved = 1,
    Awaiting = 2,
    All = 3
}

export enum BidTypeEnum {
    Bidding = 1,
    AppointContractors = 2,
}

export class NewPJDto {
    code: string;
    name: string;
    bidProfileTypeId: number;
    supplierId: number;
    unitPrice: string;
    calUnit: string;
    description: string;
    status: number;

    constructor(code: string, name: string, bidProfileTypeId: number, supplierId: number,
        unitPrice: string, calUnit: string, description: string, status: number) {
        this.code = code;
        this.name = name;
        this.bidProfileTypeId = bidProfileTypeId;
        this.supplierId = supplierId;
        this.unitPrice = unitPrice;
        this.calUnit = calUnit;
        this.description = description;
        this.status = status;
    }
}

export class BidProfileTypeInfo {
    value: number;
    label: string;
    constructor(id: number, info: string) {
        this.value = id;
        this.label = info;
    }
}
