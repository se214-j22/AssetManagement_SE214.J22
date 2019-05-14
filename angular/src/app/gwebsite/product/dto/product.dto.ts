import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';

export class ProductDto {
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

export class GetProductOutput {
    product: ProductDto;
    products: ComboboxItemDto[];
}
