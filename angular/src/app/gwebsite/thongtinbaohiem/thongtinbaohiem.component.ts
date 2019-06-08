
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ModelForViewDto, ThongTinBaoHiemServiceProxy, CheckServiceProxy } from '@shared/service-proxies/service-proxies';

import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';
import { ThongTinXeModalComponent } from '../thongtinxe/thongtinxe-modal.component';
import { ViewBaoHiemXeModalComponent } from './view-thongtinbaohiem-modal.component';
import { CreateOrEditBaoHiemXeModalComponent } from './create-or-edit-thongtinbaohiem-modal.component';

@Component({
    selector: 'baohiemxeComponent',
    templateUrl: './thongtinbaohiem.component.html',
    animations: [appModuleAnimation()]
})
export class ThongTinBaoHiemComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditBaoHiemXeModalComponent;
    @ViewChild('viewVanHanhXeModal') viewVanHanhXeModal: ViewBaoHiemXeModalComponent;
    @ViewChild('viewThongTinXe') viewThongTinXe: ThongTinXeModalComponent;


    soXe: string;
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxeDto: ThongTinXeViewDTO = new ThongTinXeViewDTO();
    isDuyet: boolean;

    constructor(
        injector: Injector,
        private _baohiemxeService: ThongTinBaoHiemServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _isDuyet: CheckServiceProxy
    ) {
        super(injector);
        this._isDuyet.isDuyet().subscribe(result => {
            this.isDuyet = result;
        })
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

    // Search(event?: LazyLoadEvent) {
    //     this._vanhanhxeService.getQuanLyVanHanhsByFilter(  , this.primengTableHelper.getSorting(this.dataTable),
    //         this.primengTableHelper.getMaxResultCount(this.paginator, event),
    //         this.primengTableHelper.getSkipCount(this.paginator, event),
    //     ).subscribe(result => {

    //         // this.primengTableHelper.totalRecordsCount = result.totalCount;
    //         // this.primengTableHelper.records = result.items;
    //         this.primengTableHelper.hideLoadingIndicator();
    //     });



    // }

    /**
     * Hàm get danh sách Customer
     * @param event
     */
    getThongTinBaoHiems(soXe: string, event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
         */
        this.reloadList(soXe, event);
    }

    reloadList(soXe: string, event?: LazyLoadEvent) {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._baohiemxeService.getThongTinBaoHiemsByFilter(this.soXe, this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.primengTableHelper.getSkipCount(this.paginator, event),
            ).subscribe(result => {

                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;

            });
        }
        this.primengTableHelper.hideLoadingIndicator();
    }

    deleteThongTinBaoHiemXe(id: number): void {
        this._baohiemxeService.deleteThongTinBaoHiem(id).subscribe(() => {
            this.reloadPage();
        })
    }
    getThongTinXe(item: ThongTinXeViewDTO) {
        this.thongtinxeDto = item;
        this.soXe = item.soXe;
        this.reloadList(this.soXe, null);
    }
    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.soXe = params['name'] || '';

            this.reloadList(this.soXe, null);
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
    createBaoHiem() {
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
