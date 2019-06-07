import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { PhongBanServiceProxy, BangYeuCauCungCapTaiSanServiceProxy, PhongBanDto} from '@shared/service-proxies/service-proxies';
import { CreateOrEditBangYeuCauCungCapTaiSanModalComponent } from './create-or-edit-bang-yeu-cau-cung-cap-tai-san-modal.component';
import { ViewBangYeuCauCungCapTaiSanModalComponent } from './view-bang-yeu-cau-cung-cap-tai-san-modal.component';


@Component({
    templateUrl: './bang-yeu-cau-cung-cap-tai-san.component.html',
    animations: [appModuleAnimation()]
})
export class BangYeuCauCungCapTaiSanComponent extends AppComponentBase implements OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditBangYeuCauCungCapTaiSanModalComponent;
    @ViewChild('viewBangYeuCauCungCapTaiSanModal') viewBangYeuCauCungCapTaiSanModal: ViewBangYeuCauCungCapTaiSanModalComponent;

    /**
     * tạo các biến dể filters
     */
    phongBanId: number;
    selectedPhongBan: PhongBanDto;
    phongBanList: PhongBanDto[];

    constructor(
        injector: Injector,
        private _phongBanService: PhongBanServiceProxy,
        private _bangYeuCauCungCapTaiSanService: BangYeuCauCungCapTaiSanServiceProxy,
        private _activatedRoute: ActivatedRoute,
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.phongBanId = params['TEMP'] || '';
            //this.reloadList(this.phongBanId, null);
        });
        this.loadPhongBanList();
    }

    getBangYeuCauCungCapTaiSans(event?: LazyLoadEvent) {
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
        this._bangYeuCauCungCapTaiSanService.getBangYeuCauCungCapTaiSansByFilter(
            this.phongBanId,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            console.log(result);
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteBangYeuCauCungCapTaiSan(id): void {
        this._bangYeuCauCungCapTaiSanService.deleteBangYeuCauCungCapTaiSan(id).subscribe(() => {
            this.reloadPage();
        })
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    loadPhongBanList() {
        this._phongBanService.getPhongBansByFilter(undefined, undefined, undefined, undefined, undefined
        ).subscribe(result => {
            console.log(result);
            this.phongBanList = result.items;
        });
    }

    SelectPhongBan(phongBan: PhongBanDto) {
        this.selectedPhongBan = phongBan;
    }

    createBangYeuCauCungCapTaiSan() {
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
