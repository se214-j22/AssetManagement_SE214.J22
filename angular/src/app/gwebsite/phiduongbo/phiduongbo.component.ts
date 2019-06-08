import { ViewPhiDuongBoModalComponent } from './view-phiduongbo-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { PhiDuongBoServiceProxy, ThongTinXeForViewDto, ModelForViewDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPhiDuongBoModalComponent } from './create-or-edit-phiDuongBo-modal.component';
import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';
import { ThongTinXeModalComponent } from '../thongtinxe/thongtinxe-modal.component';

@Component({
    templateUrl: './phiduongbo.component.html',
    animations: [appModuleAnimation()]
})

export class PhiDuongBoComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
    * @ViewChild là dùng get control và call thuộc tính, functions của control đó
    */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditPhiDuongBoModalComponent;
    @ViewChild('viewPhiDuongBoModal') viewPhiDuongBoModal: ViewPhiDuongBoModalComponent;
    @ViewChild('viewThongTinXe') viewThongTinXe: ThongTinXeModalComponent;
    /**
    * tạo các biến dể filters
    */

    thongtinxeDto: ThongTinXeViewDTO = new ThongTinXeViewDTO();
    soXe: string;
    model: ModelForViewDto = new ModelForViewDto();


    constructor(
        injector: Injector,
        private _phiDuongBoService: PhiDuongBoServiceProxy,
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
    * Hàm get danh sách PhiDuongBo
    * @param event
    */
    getPhiDuongBos(event?: LazyLoadEvent) {
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
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._phiDuongBoService.getPhiDuongBosByFilter(this.soXe, undefined, undefined, undefined, this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.primengTableHelper.getSkipCount(this.paginator, event),
            ).subscribe(result => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;

            });

        }
        this.primengTableHelper.hideLoadingIndicator();

    }

    getThongTinXe(item: ThongTinXeViewDTO) {
        this.thongtinxeDto = item;
        this.soXe = item.soXe;
        this.reloadList(null);

    }

    deletePhiDuongBo(id): void {
        this._phiDuongBoService.deletePhiDuongBo(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.soXe = params['soXe'] || '';
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
    createPhiDuongBo() {
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