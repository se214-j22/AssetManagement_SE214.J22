import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';

export class ProductCategoryDto {
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

export class GetProductCategoryOutput {
    productCategory: ProductCategoryDto;
    productCategorys: ComboboxItemDto[];
}

export enum StatusEnum {
    Open = 1,
    Close = 2,
    All = 3
}

export class NewPCDto {
    code: string;
    name: string;
    status: number;
    note: string;

    constructor(code: string, name: string, status: number, note: string) {
        this.code = code;
        this.name = name;
        this.status = status;
        this.note = note;
    }
}

export class UpSupDto {
    id: number;
    code: string;
    name: string;
    status: number;
    note: string;

    constructor(id: number, code: string, name: string, status: number, note: string) {
        this.id = id;
        this.code = code;
        this.name = name;
        this.status = status;
        this.note = note;
    }
}
