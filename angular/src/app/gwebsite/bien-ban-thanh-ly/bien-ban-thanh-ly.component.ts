import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { HoaDonNhapServiceProxy, DonViCungCapTaiSanDto, DonViCungCapTaiSanServiceProxy, BienBanThanhLyServiceProxy, LoaiTaiSanDto, HoaDonNhapDto, LoaiTaiSanServiceProxy, HoaDonNhapOutput, TaiSanCoDinhForViewDto, TaiSanCoDinhServiceProxy } from '@shared/service-proxies/service-proxies';

import { CreateOrEditBienBanThanhLyModalComponent } from './create-or-edit-bien-ban-thanh-ly-modal.component';
import { ViewBienBanThanhLyModalComponent } from './view-bien-ban-thanh-ly-modal.component';

@Component({
    templateUrl: './bien-ban-thanh-ly.component.html',
    animations: [appModuleAnimation()]
})
export class BienBanThanhLyComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditBienBanThanhLyModalComponent;
    @ViewChild('viewBienBanThanhLyModal') viewBienBanThanhLyModal: ViewBienBanThanhLyModalComponent;

    taiSanCoDinhs: TaiSanCoDinhForViewDto[];

    /**
     * tạo các biến dể filters
     */
    tenDonViThanhLyFilter: string;

    constructor(
        injector: Injector,
        private _bienBanThanhLyService: BienBanThanhLyServiceProxy,
        private _taiSanCoDinhService: TaiSanCoDinhServiceProxy,
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
     * Hàm get danh sách HoaDonNhap
     * @param event
     */
    getBienBanThanhLys(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable || !event) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
         */

        this.reloadList(event);

    }

    reloadList(event?: LazyLoadEvent) {
        this._bienBanThanhLyService.getBienBanThanhLysByFilter(
            this.tenDonViThanhLyFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteBienBanThanhLy(id): void {
        this._bienBanThanhLyService.deleteBienBanThanhLy(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.tenDonViThanhLyFilter = params['EDIT_THIS_LATER'] || null;

            this._taiSanCoDinhService.getTaiSanCoDinhsByFilter('', '', 1000, 0).subscribe(
                result => { this.taiSanCoDinhs = result.items; }
            )
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    createBienBanThanhLy() {
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
