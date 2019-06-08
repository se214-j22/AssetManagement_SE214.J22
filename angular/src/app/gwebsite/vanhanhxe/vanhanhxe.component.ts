
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ThongTinXeServiceProxy, ModelServiceProxy, ModelForViewDto, QuanLyVanHanhServiceProxy, CheckServiceProxy } from '@shared/service-proxies/service-proxies';
import { ViewVanHanhXeModalComponent } from './view-vanhanhxe-modal.component';
import { CreateOrEditVanHanhXeModalComponent } from './create-or-edit-vanhanhxe-modal.component';
import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';
import { ViewThongTinXeModalComponent } from '../thongtinxe/view-thongtinxe-modal.component';
import { ThongTinXeModalComponent } from '../thongtinxe/thongtinxe-modal.component';

@Component({
    selector: 'thongTinXeComponent',
    templateUrl: './vanhanhxe.component.html',
    animations: [appModuleAnimation()]
})
export class VanHanhXeComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditVanHanhXeModalComponent;
    @ViewChild('viewVanHanhXeModal') viewVanHanhXeModal: ViewVanHanhXeModalComponent;
    @ViewChild('viewThongTinXe') viewThongTinXe: ThongTinXeModalComponent;

    /**
     * tạo các biến dể filters
     */

    soXe: string;
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxeDto: ThongTinXeViewDTO = new ThongTinXeViewDTO();
    isDuyet: boolean;

    constructor(
        injector: Injector,
        private _vanhanhxeService: QuanLyVanHanhServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _isDuyet: CheckServiceProxy
    ) {
        super(injector);
        _isDuyet.isDuyet().subscribe(result => {
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
    getThongTinVanHanhs(soXe: string, event?: LazyLoadEvent) {
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
            this._vanhanhxeService.getQuanLyVanHanhsByFilter(soXe, this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.primengTableHelper.getSkipCount(this.paginator, event),
            ).subscribe(result => {

                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;

                console.log("test", result.items);

            });
        }
        this.primengTableHelper.hideLoadingIndicator();

    }

    deleteThongTinVanHanhXe(id: number): void {
        this._vanhanhxeService.deleteQuanLyVanHanh(id).subscribe(() => {
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
    createVanHanhXe() {
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
