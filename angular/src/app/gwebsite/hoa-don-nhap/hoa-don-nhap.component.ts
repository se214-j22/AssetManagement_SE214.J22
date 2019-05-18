import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { HoaDonNhapServiceProxy, DonViCungCapTaiSanDto, DonViCungCapTaiSanServiceProxy } from '@shared/service-proxies/service-proxies';

import { CreateOrEditHoaDonNhapModalComponent } from './create-or-edit-hoa-don-nhap-modal.component';
import { ViewHoaDonNhapModalComponent } from './view-hoa-don-nhap-modal.component';

@Component({
    templateUrl: './hoa-don-nhap.component.html',
    animations: [appModuleAnimation()]
})
export class HoaDonNhapComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditHoaDonNhapModalComponent;
    @ViewChild('viewHoaDonNhapModal') viewHoaDonNhapModal: ViewHoaDonNhapModalComponent;

    /**
     * tạo các biến dể filters
     */
    donViCungCapTaiSans: DonViCungCapTaiSanDto[];
    donViCungCapTaiSanId: number;
    soHoaDon: string;

    constructor(
        injector: Injector,
        private _hoaDonNhapService: HoaDonNhapServiceProxy,
        private _donViCungCapTaiSanService: DonViCungCapTaiSanServiceProxy,
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
    getHoaDonNhaps(event?: LazyLoadEvent) {
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
        this._hoaDonNhapService.getHoaDonNhapsByFilter(
            this.donViCungCapTaiSanId, this.soHoaDon, 
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteHoaDonNhap(id): void {
        this._hoaDonNhapService.deleteHoaDonNhap(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.donViCungCapTaiSanId = params['EDIT_THIS_LATER'] || null;
            this.soHoaDon = params['EDIT_THIS_LATER'] || '';
            // this.reloadList(null);

            this._donViCungCapTaiSanService.getDonViCungCapTaiSansByFilter('', '', 1000, 0).subscribe(
                result => { this.donViCungCapTaiSans = result.items; }
            );
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

    //hàm show view create MenuClient
    createHoaDonNhap() {
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
