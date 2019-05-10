import { ViewProviderModalComponent } from './view-provider-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ProviderServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditProviderModalComponent } from './create-or-edit-provider-modal.component';

@Component({
    templateUrl: './provider.component.html',
    animations: [appModuleAnimation()]
})
export class ProviderComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
    * @ViewChild là dùng get control và call thuộc tính, functions của control đó
    */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditProviderModalComponent;
    @ViewChild('viewProviderModal') viewProviderModal: ViewProviderModalComponent;

    /**
    * tạo các biến dể filters
    */
    providerName: string;

    constructor(
        injector: Injector,
        private _providerService: ProviderServiceProxy,
        private _activatedRoute: ActivatedRoute,
    ) {
        super(injector);
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
    * Hàm get danh sách Provider
    * @param event
    */
    getProviders(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
        * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
        */

        this.reloadList(null, event);

    }

    reloadList(providerName, event?: LazyLoadEvent) {
        this._providerService.getProvidersByFilter(providerName, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteProvider(id): void {
        this._providerService.deleteProvider(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.providerName = params['name'] || '';
            this.reloadList(this.providerName, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.providerName, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    createProvider() {
        this.createOrEditModal.show();
    }

    /**
    * Tạo pipe thay vì tạo từng hàm truncate như thế này
    * @param text
    */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }
}