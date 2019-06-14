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

export class BidProfileTypeInfo2 {
    value: string;
    label: string;
    constructor(code: string, info: string) {
        this.value = code;
        this.label = info;
    }
}

export enum ApprovalStatusEnum {
    Active = 1,
    Inactive = 2,
    Close = 3
}

export enum StatusEnum {
    Open = 1,
    Close = 2,
    All = 3
}
export class NewBidUnit {
    bidProfileCode: string;

    supplierCode: string;
    submitDate: string;
    proofNum: number;
    beginCost: number;
    bank: string;
    note: string;
    status: number;

    constructor(thisBidProfileCode: string,
        newSupplierCode: string, newSubmitDate: string, newProofNum: number, newBeginCost: number,
        newBank: string, newNote: string, newStatus: number) {

        this.bidProfileCode = thisBidProfileCode;

        this.supplierCode = newSupplierCode;
        this.submitDate = newSubmitDate;
        this.proofNum = newProofNum;
        this.beginCost = newBeginCost;
        this.bank = newBank;
        this.note = newNote;
        this.status = newStatus;
    }
}
