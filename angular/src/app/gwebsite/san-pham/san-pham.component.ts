import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { SanPhamServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSanPhamModalComponent } from './create-or-edit-san-pham-modal.component';
import { ViewSanPhamModalComponent } from './view-san-pham-modal.component';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import * as moment from 'moment/moment.js';
import {ExcelService} from './services/excel.service';

@Component({
    templateUrl: './san-pham.component.html',
    animations: [appModuleAnimation()]
})
export class SanPhamComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('textsTable') textsTable: ElementRef;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditSanPhamModalComponent;
    @ViewChild('viewSanPhamModal') viewSanPhamModal: ViewSanPhamModalComponent;
    /**
     * tạo các biến dể filters
     */
    //filterText:string;
    //filterText: moment.Moment;
    maSP: string;
    id: string;
    //ngay: Date;
  

    constructor(
        injector: Injector,
        private _sanPhamService: SanPhamServiceProxy,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: WebApiServiceProxy,
        private excelService:ExcelService
    ) {
        super(injector);
    }

    exportAsXLSX():void {
        this.excelService.exportAsExcelFile(this.data, 'report');
     }

     data: any = [];
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
     * Hàm get danh sách LoaiTaiSan
     * @param event
     */
    getSanPhams(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
         */

        this.reloadList(null,null,null,null,null, event);

    }

    reloadList(filterText,filterText1,filterText2,filterText3,filterText4,event?: LazyLoadEvent) {
        this._sanPhamService.getSanPhamsByFilter(filterText,filterText1,filterText2,filterText3,filterText4, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            
            this.primengTableHelper.hideLoadingIndicator();
            this.data=result.items;
        });
    }
    deleteSanPham(id): void {
        this._sanPhamService.deleteSanPham(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
         //get params từ url để thực hiện filter

        this._activatedRoute.params.subscribe((params: Params) => {
           // this.filterText=(moment)(params['filterText']);
             this.maSP=params['maSP'] || '';
             this.reloadList(this.maSP,null,null,null,null,null);
           // this.reloadList(null,null,this.filterText,null,null,null);
            
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }


    applyFilters(): void {
        //truyền params lên url thông qua router

        // this._router.navigate(['app/gwebsite/san-pham', {
        //      filterText: this.filterText
        // }]);
     
        // this.filterText=null;
        this.reloadList(this.maSP,null,null,null,null,null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    
    }
    //hàm show view create MenuClient
    createSanPham() {
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
