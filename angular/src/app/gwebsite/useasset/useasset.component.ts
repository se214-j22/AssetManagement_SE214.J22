import { ViewUseAssetModalComponent } from './view-useasset-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { UseAssetServiceProxy, AssetServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditUseAssetModalComponent } from './create-or-edit-useasset-modal.component';

@Component({
    templateUrl: './useasset.component.html',
    animations: [appModuleAnimation()]
})
export class UseAssetComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
    * @ViewChild là dùng get control và call thuộc tính, functions của control đó
    */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditUseAssetModalComponent;
    @ViewChild('viewUseAssetModal') viewUseAssetModal: ViewUseAssetModalComponent;

    /**
    * tạo các biến dể filters
    */
    useassetName: string;

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy,
        private _assetService: AssetServiceProxy,
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
    * Hàm get danh sách UseAsset
    * @param event
    */
    getUseAssets(event?: LazyLoadEvent) {
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

    reloadList(useassetName, event?: LazyLoadEvent) {
        this._useassetService.getUseAssetsByFilter(useassetName, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteUseAsset(id): void {
        this._useassetService.deleteUseAsset(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.useassetName = params['name'] || '';
            this.reloadList(this.useassetName, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.useassetName, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    createUseAsset() {
        this.createOrEditModal.show();
    }

    /**
    * Tạo pipe thay vì tạo từng hàm truncate như thế này
    * @param text
    */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    approvedUseAsset(id): void {
        this._useassetService.approveUseAsset(id).subscribe(() => {
            this._useassetService.getUseAssetForView(id).subscribe(result => {
                this._assetService.updateAssetStatusUsing(result.assetId).subscribe(() => {
                    this.reloadPage();
                })
            });
        })
    }
}