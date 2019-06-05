import { ViewTaiSanModalComponent } from './view-taisan-modal.component';
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { TaiSanServiceProxy, TaiSanDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTaiSanModalComponent } from './create-or-edit-taisan-modal.component';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'taiSanFindModel',
    templateUrl: './taisan-find.component.html',
    animations: [appModuleAnimation()]
})
export class TaiSanFindComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //@ViewChild('createOrEditModal') createOrEditModal: CreateOrEditTaiSanModalComponent;
    //@ViewChild('viewTaiSanModal') viewTaiSanModal: ViewTaiSanModalComponent;
    @ViewChild('taiSanFindModal') modal: ModalDirective;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalTaiSanSave: EventEmitter<any> = new EventEmitter<any>();

    /**
     * tạo các biến dể filters
     */
    TenTs: string;
    item: TaiSanDto = new TaiSanDto();

    constructor(
        injector: Injector,
        private _taiSanService: TaiSanServiceProxy,
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

    //6-4 show
    show(): void {
        this.modal.show();
    }

    /**
     * Hàm get danh sách TaiSan
     * @param event
     */
    getTaiSans(event?: LazyLoadEvent) {
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

    reloadList(TenTs, event?: LazyLoadEvent) {
        this._taiSanService.getTaiSansByFilter(TenTs, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteTaiSan(id): void {
        this._taiSanService.deleteTaiSan(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.TenTs = params['TenTs'] || '';
            this.reloadList(this.TenTs, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.TenTs, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    //createTaiSan() {
    //    this.createOrEditModal.show();
    //}

    /**
     * Tạo pipe thay vì tạo từng hàm truncate như thế này
     * @param text
     */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    close(): void {
        this.modal.hide();
        this.modalTaiSanSave.emit(this.item);
    }
}
