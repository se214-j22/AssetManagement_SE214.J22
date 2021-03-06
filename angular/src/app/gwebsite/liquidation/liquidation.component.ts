import { ViewLiquidationModalComponent } from './view-liquidation-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { LiquidationServiceProxy, AssetServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLiquidationModalComponent } from './create-or-edit-liquidation-modal.component';

@Component({
    templateUrl: './liquidation.component.html',
    animations: [appModuleAnimation()]
})
export class LiquidationComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
    * @ViewChild là dùng get control và call thuộc tính, functions của control đó
    */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditLiquidationModalComponent;
    @ViewChild('viewLiquidationModal') viewLiquidationModal: ViewLiquidationModalComponent;

    /**
    * tạo các biến dể filters
    */
    liquidationName: string;
    constructor(
        injector: Injector,
        private _liquidationService: LiquidationServiceProxy,
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
    * Hàm get danh sách Liquidation
    * @param event
    */
    getLiquidations(event?: LazyLoadEvent) {
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

    reloadList(liquidationName, event?: LazyLoadEvent) {
        this._liquidationService.getLiquidationsByFilter(liquidationName, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteLiquidation(id): void {
        this._liquidationService.deleteLiquidation(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.liquidationName = params['name'] || '';
            this.reloadList(this.liquidationName, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.liquidationName, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    createLiquidation() {
        this.createOrEditModal.show();
    }

    /**
    * Tạo pipe thay vì tạo từng hàm truncate như thế này
    * @param text
    */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    approvedLiquidation(id): void {
        this._liquidationService.approveLiquidation(id).subscribe(() => {
            this._liquidationService.getLiquidationForView(id).subscribe(result => {
                this._assetService.updateAssetStatusLiquidated(result.assetID).subscribe(() => {
                    this.reloadPage();
                })
            });
        })
    }
}