import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { CTDonViServiceProxy, DonViDto } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'donViFindModel',
    templateUrl: './donvi-chitiet.component.html',
    animations: [appModuleAnimation()]
})
export class CTDonViComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //@ViewChild('createOrEditModal') createOrEditModal: CreateOrEditDonViModalComponent;
    //@ViewChild('viewDonViModal') viewDonViModal: ViewDonViModalComponent;
    @ViewChild('donViFindModal') modal: ModalDirective;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalDonViSave: EventEmitter<any> = new EventEmitter<any>();

    /**
     * tạo các biến dể filters
     */
    tenDonVi: string;
    item: DonViDto = new DonViDto();

    constructor(
        injector: Injector,
        private _ctDonViService: CTDonViServiceProxy,
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
     * Hàm get danh sách CTDonVi
     * @param event
     */
    getCTDonVis(event?: LazyLoadEvent) {
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

    reloadList(tenDonVi, event?: LazyLoadEvent) {
        this._ctDonViService.getCTDonVisByFilter(tenDonVi, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteCTDonVi(id): void {
        this._ctDonViService.deleteCTDonVi(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.tenDonVi = params['tenDonVi'] || '';
            this.reloadList(this.tenDonVi, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.tenDonVi, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    //hàm show view create MenuClient
    //createCTDonVi() {
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
        this.modalDonViSave.emit(this.item);
    }
}
