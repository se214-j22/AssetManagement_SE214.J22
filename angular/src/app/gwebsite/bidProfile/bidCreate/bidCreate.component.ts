import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { IMyDpOptions, IMyDateModel, IMyDate } from 'mydatepicker';
import * as moment from 'moment';
import { ApprovalStatusEnum, BidTypeEnum, StatusEnum, BidProfileTypeInfo, BidProfileTypeInfo2, NewBidUnit } from '../dto/bidProfile.dto';

@Component({
    selector: 'app-bidCreate',
    templateUrl: './bidCreate.component.html',
    styleUrls: ['./bidCreate.component.css'],
    animations: [appModuleAnimation()]
})
export class BidCreateComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('textsTable') textsTable: ElementRef;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    /**
     * tạo các biến dể filters
     */
    filterText: string;
    //permission cho duyệt, thêm, xóa, sửa: Admin và Department tạo bidCreate đó.
    // duyệt: chỉ mỗi Admin đc duyệt
    // sửa, đóng: department tạo ra bidCreate đó và Admin.
    // thêm: ai thêm cũng đc, ko phân quyền
    public isPermissionEditCloseActive = false;
    public bidCatalogProductId: number;


    //api 8.7, get all products có status=1(active hay open)
    public productInfos = [];
    public productFakes = [
            {
                id: 1,
                code: 'Pd01',
                name: 'Product1'
            },
            {
                id: 2,
                code: 'Pd02',
                name: 'Product1'
            },
            {
                id: 3,
                code: 'Pd03',
                name: 'Product1'
            },
            {
                id: 4,
                code: 'Pd04',
                name: 'Product1'
            },
            {
                id: 5,
                code: 'Pd05',
                name: 'Product1'
            },
            {
                id: 6,
                code: 'Pd06',
                name: 'Product1'
            }
        ];

    saving = false;

    public bidProfileWinStatus = 2;
    public bidProjectId: number;

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

    public startDateString = '';
    public endDateString = '';
    public openDateString = '';


    public newSupplierCode;
    public newsupplierSubmitDateString = '';
    public newProofNum;
    public newBank;
    public newBeginCost;
    public newNotes;

    public newBidUnits: Array<NewBidUnit> = [];


    public datePickerOptions: IMyDpOptions = {
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
        disableUntil: {
            year: new Date().getFullYear(),
            month: new Date().getMonth() + 1,
            day: new Date().getDate() - 1
        }
    };

    // public model: any = { date: { year: new Date().getFullYear(), month: new Date().getMonth(), day: new Date().getDate() } };
    // public model = new Date();
    public creatDateString = '';
    public bidProfileCode = '';
    public bidCreateName = '';

    // -những dự án của năm cũ, sẽ tự động close (mỗi lần đến 1/1/newyear, sẽ trigger cho nó close hết bidCreates năm cũ),
    //      dù có đc approved hay chưa.
    // -những dự án của năm hiện tại: chỉ dc phép close khi nó chưa đc approved.
    public bidCreateFakes = [
        {
            id: 1,
            code: 'S001',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        },
        {
            id: 2,
            code: 'S002',
            name: 'Purchase early in the year',
            bidCreateTypeId: 3,
            supplierId: 1,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: false
        },
        {
            id: 3,
            code: 'S003',
            name: 'Purchase early in the year',
            bidCreateTypeId: 4,
            supplierId: 6,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: false
        },
        {
            id: 4,
            code: 'S004',
            name: 'Purchase early in the year',
            bidCreateTypeId: 9,
            supplierId: 7,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        },
        {
            id: 5,
            code: 'S005',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 5,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: false
        },
        {
            id: 6,
            code: 'S006',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: false
        },
        {
            id: 7,
            code: 'S007',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        },
        {
            id: 8,
            code: 'S008',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: false
        },
        {
            id: 9,
            code: 'S009',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        },
        {
            id: 10,
            code: 'S010',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        },
        {
            id: 11,
            code: 'S011',
            name: 'Purchase early in the year',
            bidCreateTypeId: 1,
            supplierId: 2,
            unitPrice: '20000',
            calUnit: 'VND',
            description: 'This is bidCreate item',
            supplierAddress: 'Quan 3 - TP HCM',
            createDate: '05/11/2018',
            status: 2,
            isUsed: true
        }
    ];

    public isExistAdd = false;

    public oldObject = {};

    public approvalStatusEnum = ApprovalStatusEnum;

    public myConfigStyleHeader: any = {
        'font-size': '11px'
    };
    public myConfigStyle: any = {
        'font-size': '11px'
    };
    public header;

    public bidTypeEnum = BidTypeEnum;
    public bidType = 1;
    public bidTypes = [
        {
            id: BidTypeEnum.Bidding,
            name: 'Bidding'
        },
        {
            id: BidTypeEnum.AppointContractors,
            name: 'Appoint Contractors'
        }
    ];

    public cautionMoney;

    public note: string;

    //get all supplier có status=1(active hay open),
    // chỉ cần get 3 fields
    supplierInfos = [];
    public supplierFakes = [
        {
            id: 1,
            code: 'SP01',
            name: 'Supplier1'
        },
        {
            id: 2,
            code: 'SP02',
            name: 'Supplier2'
        },
        {
            id: 3,
            code: 'SP03',
            name: 'Supplier3'
        },
        {
            id: 4,
            code: 'SP04',
            name: 'Supplier4'
        }
    ];

    public projectInfos = [];
    public allProjectFakes = [
        {
            id: 1,
            code: 'Pd01',
            name: 'Product1'
        },
        {
            id: 2,
            code: 'Pd02',
            name: 'Product1'
        },
        {
            id: 3,
            code: 'Pd03',
            name: 'Product1'
        },
        {
            id: 4,
            code: 'Pd04',
            name: 'Product1'
        },
        {
            id: 5,
            code: 'Pd05',
            name: 'Product1'
        },
        {
            id: 6,
            code: 'Pd06',
            name: 'Product1'
        }
    ];

    // get all bidUnit của bidProfile này.
    public isAdd = false;


    constructor(
        injector: Injector,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
        this.isPermissionEditCloseActive = true;

        // call hàm này khi subcribe api 8.7 get all product success
        this.handelSelectsProject();
        this.handelSelectsProduct();
        this.handelSelectsSupplier();
    }

    /**
     * Hàm xử lý sau khi View được init
     */
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
    }
    public handelSelectsProject(): void {
        // filter products
        this.projectInfos = [];
        this.allProjectFakes.forEach((item, i) => {
            this.projectInfos.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
        this.bidProjectId = this.projectInfos[0].id;
    }
    public handelSelectsProduct(): void {
        this.productInfos = [];
        this.productFakes.forEach((item, i) => {
            this.productInfos.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
        this.bidCatalogProductId = this.productInfos[0].id;
    }
    public handelSelectsSupplier(): void {
        this.supplierInfos = [];
        this.supplierFakes.forEach((item, i) => {
            this.supplierInfos.push(
                new BidProfileTypeInfo2(item.code, `${item.code} - ${item.name}`));
        });
    
        this.newSupplierCode = this.supplierInfos[0].code;
    }

    addNewContractor(): void {
        this.newBidUnits.push(new NewBidUnit(this.bidProfileCode, this.newSupplierCode, this.newsupplierSubmitDateString,
            this.newProofNum, this.newBeginCost, this.newBank, this.newNotes, 2));
      
    }

    removeBidUnit(i: number): void {
        this.newBidUnits.splice(i, 1);
    }

    cancelAdd(): void {

        this.isAdd = false;
    }

    addSupplier(): void {
        // if (this.isAdd && this.productCode && this.productCode !== 0 && this.quantity > 0) {
        //     this.newProductTableList.push(new NewProductAddList(this.productCode, this.quantity, false));
        //     this.productInfoList = this.productInfoList.filter(x => x.productCode !== this.productCode);
        //     this.isAdd = false;
            //newPlanProductList exist >= 1item
            if (!this.isExistAdd) {
                this.isExistAdd = true;
            }
        // }
    }

    public onDateChangedByStart(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.startDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
    public onDateChangedByEnd(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.endDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
    public onDateChangedByOpen(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.openDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

    public onDateChangedBySupplierSubmit(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.newsupplierSubmitDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

    public OpenAddSupplier(): void {
        this.isAdd =  true;
    }

    /**
     * Hàm get danh sách BidCreate
     * @param event
     */
    getBidCreates(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * Sử dụng _apiService để call các api của backend
         */

        // this._apiService.get('api/MenuClient/GetMenuClientsByFilter',
        //     [{ fieldName: 'Name', value: this.filterText }],
        //     this.primengTableHelper.getSorting(this.dataTable),
        //     this.primengTableHelper.getMaxResultCount(this.paginator, event),
        //     this.primengTableHelper.getSkipCount(this.paginator, event),
        // ).subscribe(result => {
        //     this.primengTableHelper.totalRecordsCount = result.totalCount;
        //     this.primengTableHelper.records = result.items;
        //     this.primengTableHelper.hideLoadingIndicator();
        // });

        this.primengTableHelper.totalRecordsCount = 16;
        this.primengTableHelper.records = this.bidCreateFakes;

        this.primengTableHelper.records.forEach((item) => {
            item.isEdit = false;
        });

        this.primengTableHelper.hideLoadingIndicator();
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
        this._router.navigate(['app/gwebsite/bidCreate', {
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

    public searchBidCreate(): void {
        //3 params filter FE truyền vào api
        // filter, values default = ''
        console.log(this.status + '--' + this.bidProfileCode + '--' + this.bidCreateName);
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
            // dựa vào id, set status cho bidCreate là close nếu nó đang open và ngược lại.

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

    save(): void {
        this.saving = true;

        this.newBidUnits.forEach((ele, index) => {
            let radioButton = document.getElementById(`selectWinContractor-${index}`);
            // if (radioButton.checked) {
            //     ele.status = 1;
            //     this.bidProfileWinStatus = 1;
            // } else {
            //     ele.status = 2;
            // }
        });
        // save xong
        this.saving = false;
    }

}
