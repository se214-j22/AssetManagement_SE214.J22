import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';

export class SupplierDto {
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

export class GetSupplierOutput {
    supplier: SupplierDto;
    suppliers: ComboboxItemDto[];
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

export class NewPJDto {
    code: string;
    name: string;
    supplierTypeId: number;
    address: string;
    email: string;
    fax: string;
    phone: string;
    contact: string;
    description: string;
    status: number;


    constructor(code: string, name: string, supplierTypeId: number, address: string,
        email: string, fax: string, phone: string, contact: string, description: string,
        status: number) {
        this.code = code;
        this.name = name;
        this.supplierTypeId = supplierTypeId;
        this.address = address;
        this.email = email;
        this.fax = fax;
        this.phone = phone;
        this.contact = contact;
        this.description = description;
        this.status = status;
    }
}

export class SupplierTypeInfo {
    id: number;
    info: string;
    constructor(id: number, info: string) {
        this.id = id;
        this.info = info;
    }
}
