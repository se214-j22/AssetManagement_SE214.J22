import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { CreateOrEditProductModalComponent } from './create-or-edit-product-modal/create-or-edit-product-modal.component';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { IMyDpOptions, IMyDateModel, IMyDate } from 'mydatepicker';
import * as moment from 'moment';
import { ApprovalStatusEnum, StatusEnum } from './dto/product.dto';
import { ProductsServiceProxy, ProductSavedDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { AbpSessionService } from 'abp-ng2-module/dist/src/session/abp-session.service';


@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css'],
    animations: [appModuleAnimation()]
})
export class ProductComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('textsTable') textsTable: ElementRef;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditProductModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    /**
     * tạo các biến dể filters
     */
    filterText: string;
    //permission cho duyệt, thêm, xóa, sửa: Admin và Department tạo product đó.
    // duyệt: chỉ mỗi Admin đc duyệt
    // sửa, đóng: department tạo ra product đó và Admin.
    // thêm: ai thêm cũng đc, ko phân quyền
    public isPermissionEditCloseActive = false;

    public status = StatusEnum.All;
    public statusEnum = StatusEnum;
    public StatusList = [
        {
            id: StatusEnum.All,
            name: ''
        },
        {
            id: StatusEnum.Open,
            name: 'Open'
        },
        {
            id: StatusEnum.Close,
            name: 'Close'
        }
    ];

    public createDatePickerOptions: IMyDpOptions = {
        selectorWidth: '240px',
        dateFormat: 'dd/mm/yyyy',
        showTodayBtn: true,
        todayBtnTxt: 'Now',
        showClearDateBtn: true,
        alignSelectorRight: true,
        openSelectorOnInputClick: true,
        inline: false,
        editableDateField: false,
        selectionTxtFontSize: '13px',
        height: '37px',
        firstDayOfWeek: 'su',
        sunHighlight: true,
        disableSince: {
            year: new Date().getFullYear(),
            month: new Date().getMonth() + 1,
            day: new Date().getDate() + 1
        }
    };
    // public model: any = { date: { year: new Date().getFullYear(), month: new Date().getMonth(), day: new Date().getDate() } };
    // public model = new Date();
    public creatDateString = '';
    public productCodeFilter = '';
    public productNameFilter = '';

    // -những dự án của năm cũ, sẽ tự động close (mỗi lần đến 1/1/newyear, sẽ trigger cho nó close hết products năm cũ),
    //      dù có đc approved hay chưa.
    // -những dự án của năm hiện tại: chỉ dc phép close khi nó chưa đc approved.


    public oldObject = {};

    public approvalStatusEnum = ApprovalStatusEnum;

    public myConfigStyleHeader: any = {
        'font-size': '11px'
    };
    public myConfigStyle: any = {
        'font-size': '11px'
    };
    public header;

    constructor(
        injector: Injector,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: ProductsServiceProxy,
        private _sessionService: AbpSessionService,
        private _userService: UserServiceProxy,
    ) {
        super(injector);
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {

         this._userService.getUserForEdit(this._sessionService.userId).subscribe(user => {
            this.isPermissionEditCloseActive = user.roles.findIndex(item => item.roleName === 'Admin') !== -1;
        });
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
     * Hàm get danh sách Product
     * @param event
     */
    getProducts(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * Sử dụng _apiService để call các api của backend
         */
        this._apiService.getProducts(
            this.productNameFilter, this.status, this.productCodeFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
                this.primengTableHelper.records.forEach((item) => {
                    item.isEdit = false;
                });
            }, err => console.log(err));
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            this.filterText = params['filterText'] || '';
            //reload lại gridview
            this.reloadPage();
        });
    }
    /**
     * onScrollX
     * @param event
     */
    public onScrollX(event): void {
        this.myConfigStyleHeader = {
            ...this.myConfigStyle,
            left: this.header ? `${this.header.getBoundingClientRect().left}px` : 'auto'
        };
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this._router.navigate(['app/gwebsite/product', {
            filterText: this.filterText
        }]);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    /**
     * Tạo pipe thay vì tạo từng hàm truncate như thế này
     * @param text
     */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    //Refresh grid khi thực hiện create or edit thành công
    refreshValueFromModal(): void {
        if (this.createOrEditModal.product.id) {
            for (let i = 0; i < this.primengTableHelper.records.length; i++) {
                if (this.primengTableHelper.records[i].id === this.createOrEditModal.product.id) {
                    this.primengTableHelper.records[i] = this.createOrEditModal.product;
                    return;
                }
            }
        } else { this.reloadPage(); }
    }

    //hàm show view create Product
    createProduct() {
        this.createOrEditModal.show();
    }

    public searchProduct(): void {
        //3 params filter FE truyền vào api
        // filter, values default = ''
        console.log(this.status + '--' + this.productCodeFilter + '--' + this.productNameFilter);
    }

    public onDateChangedBy(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.creatDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

    public actionEdit(row: any, $event: Event): void {
        // $event.stopPropagation();
        this.oldObject['name'] = row.name;
        this.oldObject['unitPrice'] = row.unitPrice;
        this.oldObject['calUnit'] = row.calUnit;
        this.oldObject['description'] = row.description;
        row.isEdit = true;
    }

    public saveEditItem(id: number, row: any, $event: Event): void {
        if (this.isPermissionEditCloseActive && row.name && row.name !== '') {
            //call api edit name thông qua id truyền vào

            //Các fields cần đưa vào model để update.

            // vì bên html đã tự bind [(ngModel)] vào row.name và row.note rồi, nên ở đây ta chỉ cần lấy ra giá trị để update
            console.log(id + '---' + row.name + '---' + row.unitPrice + '---' + row.calUnit + '---' + row.description);
            this._apiService.updateProductAsync(new ProductSavedDto({ id: row.id, address: row.address, description: row.description, name: row.name, unitPrice: row.unitPrice })).subscribe(() => {
                console.log('success');
            });
            //save thành công
            row.isEdit = false;
        }

    }

    public cancelEdit(row: any, $event: Event): void {
        row.name = this.oldObject['name'];
        row.unitPrice = this.oldObject['unitPrice'];
        row.calUnit = this.oldObject['calUnit'];
        row.description = this.oldObject['description'];

        row.isEdit = false;
    }

    public actionPCItem(id: number, row: any): void {
        if (this.isPermissionEditCloseActive) {
            // dựa vào id, set status cho product là close nếu nó đang open và ngược lại.

            //sau khi set success
            row.status = row.status === StatusEnum.Close ? StatusEnum.Open : StatusEnum.Close;
        }
    }

    //chỉ những người có permission mới đc phép thực thi action với PC
    public removePcItem(id: number, row: any, index: number): void {
        if (this.isPermissionEditCloseActive) {
            this.primengTableHelper.records.splice(index, 1);
        }
        this.primengTableHelper.hideLoadingIndicator();
    }
}
