import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild, Optional, Inject } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { CreateOrEditProductModalComponent } from './create-or-edit-product-modal/create-or-edit-product-modal.component';
import { ProductsServiceProxy, API_BASE_URL, ListResultDtoOfProductDto } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css'],
    animations: [appModuleAnimation()]
})
export class ProductComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('textsTable') textsTable: ElementRef;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditProductModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    /**
     * tạo các biến dể filters
     */
    filterText: string;
    baseUrl1 = 'http://localhost:5000';
    constructor(
        injector: Injector,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _productService: ProductsServiceProxy,
        @Optional() @Inject(API_BASE_URL) baseUrl?: string
    ) {
        super(injector);
        // console.log('baseUrl', baseUrl)
        // this.baseUrl1 = baseUrl
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
    }

    /**
     * Hàm xử lý sau khi View được init
     */
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
    }

    /**
     * Hàm get danh sách Product
     * @param event
     */
    getProducts(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }
        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * Sử dụng _apiService để call các api của backend
         */
        this._productService.getProducts(this.filterText, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event))
            .subscribe((result: any) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
                this.primengTableHelper.records.map(p => {
                    p.supplierName = 'Add Bidding';
                    for (let i = 0; i < p.biddings.length; i++) {
                        if (p.biddings[i].status === 1) {
                            p.supplierName = p.biddings[i].supplier.name;
                            break;
                        }
                    }
                });
                console.log(this.primengTableHelper.records);
            });
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.filterText = params['filterText'] || '';
            //reload lại gridview
            this.reloadPage();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this._router.navigate(['app/gwebsite/product', {
            filterText: this.filterText
        }]);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    /**
     * Tạo pipe thay vì tạo từng hàm truncate như thế này
     * @param text
     */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    //Refresh grid khi thực hiện create or edit thành công
    refreshValueFromModal(): void {
        this.getProducts();
    }

    //hàm show view create Product
    createProduct(id?: number) {
        this.createOrEditModal.show(id);
    }
}
