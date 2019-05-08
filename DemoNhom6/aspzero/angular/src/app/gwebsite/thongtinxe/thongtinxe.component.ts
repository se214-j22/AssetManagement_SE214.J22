
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ThongTinXeServiceProxy, ModelServiceProxy, ModelForViewDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditThongTinXeModalComponent } from './create-or-edit-thongtinxe-modal.component';
import { ViewThongTinXeModalComponent } from './view-thongtinxe-modal.component'
import { ThongTinXeViewDTO } from './dto/ThongTinXeViewDTO';
import { ThongTinXeFilter } from './dto/ThongTInXeFilter';
@Component({
    selector: 'thongTinXeComponent',
    templateUrl: './thongtinxe.component.html',
    styleUrls: ['./thongtinxe.component.css'],
    animations: [appModuleAnimation()]
})
export class ThongTinXeComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditThongTinXeModalComponent;
    @ViewChild('viewCustomerModal') viewThongTinXeModal: ViewThongTinXeModalComponent;

    /**
     * tạo các biến dể filters
     */

    soXe: string;
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxes: ThongTinXeViewDTO[] = [];
    filter: ThongTinXeFilter = new ThongTinXeFilter();



    constructor(
        injector: Injector,
        private _thongtinxeService: ThongTinXeServiceProxy,
        private _modelService: ModelServiceProxy,
        private _activatedRoute: ActivatedRoute,
    ) {
        super(injector);
        this.filter.soXe = null;
        this.filter.mucDichSuDung = null;
        this.filter.model = null;
        this.filter.namSanXuat = null;
        this.filter.trangThaiDuyet = null;
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

    Search(event?: LazyLoadEvent) {
        this._thongtinxeService.getThongTinXeByFilter(this.filter.soXe, this.filter.mucDichSuDung, this.filter.model, this.filter.namSanXuat, this.filter.trangThaiDuyet, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {

            // this.primengTableHelper.totalRecordsCount = result.totalCount;
            // this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });



    }

    /**
     * Hàm get danh sách Customer
     * @param event
     */
    getThongTinXes(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
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

        this._thongtinxeService.getThongTinXeByFilter(this.filter.soXe, this.filter.mucDichSuDung, this.filter.model, this.filter.namSanXuat, this.filter.trangThaiDuyet, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            var i = 0;
            // this.primengTableHelper.totalRecordsCount = result.totalCount;
            // this.primengTableHelper.records = result.items;
            this.thongtinxes = [];
            result.items.map(item => {


                var thongtinxe = new ThongTinXeViewDTO();
                thongtinxe.trangThaiDuyet = item.trangThaiDuyet;
                thongtinxe.mucDichSuDung = item.mucDichSuDung;
                thongtinxe.soXe = item.soXe;
                thongtinxe.donViSuDung = item.donViSuDung;
                console.log("Ne" + item.donViSuDung);
                thongtinxe.namSanXuat = item.namSanXuat;
                thongtinxe.model = item.model;
                thongtinxe.maTaiSan = item.maTaiSan;
                console.log("kkk", item.model);
                this._modelService.getModelForView(item.model).subscribe(result => {
                    thongtinxe.loaiXe = result.loaiXe;

                    thongtinxe.hangSanXuat = result.hangSanXuat;

                });
                this.thongtinxes.push(thongtinxe);


            });
            this.primengTableHelper.totalRecordsCount = this.thongtinxes.length;

            this.primengTableHelper.records = this.thongtinxes;
            this.primengTableHelper.hideLoadingIndicator();
            this.filter.soXe = null;
            this.filter.mucDichSuDung = null;
            this.filter.model = null;
            this.filter.namSanXuat = null;
            this.filter.trangThaiDuyet = null;
        });
    }

    deleteThongTinXe(soxe: string): void {
        this._thongtinxeService.deleteThongTinXe(soxe).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.soXe = params['name'] || '';
            this.reloadList(null);
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
    createThongTinXe() {
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
