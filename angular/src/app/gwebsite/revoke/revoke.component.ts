import { ViewRevokeModalComponent } from './view-revoke-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { RevokeServiceProxy, AssetServiceProxy, AssetDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditRevokeModalComponent } from './create-or-edit-revoke-modal.component';

@Component({
    templateUrl: './revoke.component.html',
    animations: [appModuleAnimation()]
})
export class RevokeComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditRevokeModalComponent;
    @ViewChild('viewRevokeModal') viewRevokeModal: ViewRevokeModalComponent;

    /**
     * tạo các biến dể filters
     */
    revokeName: string;

    constructor(
        injector: Injector,
        private _revokeService: RevokeServiceProxy,
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
     * Hàm get danh sách Revoke
     * @param event
     */
    getRevokes(event?: LazyLoadEvent) {
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

    reloadList(revokeName, event?: LazyLoadEvent) {
        this._revokeService.getRevokesByFilter(revokeName, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteRevoke(id): void {
        this._revokeService.deleteRevoke(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.revokeName = params['name'] || '';
            this.reloadList(this.revokeName, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.revokeName, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    createRevoke() {
        this.createOrEditModal.show();
    }

    /**
     * Tạo pipe thay vì tạo từng hàm truncate như thế này
     * @param text
     */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    approvedRevoke(id): void {
        this._revokeService.approveRevoke(id).subscribe(() => {
            this._revokeService.getRevokeForView(id).subscribe(result => {
                this._assetService.updateAssetStatusInStock(result.assetId).subscribe(() => {
                    this.reloadPage();
                })
            });
        })
    }
}
