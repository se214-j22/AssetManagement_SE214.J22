import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild, NgZone, HostListener } from '@angular/core';
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
import { ApprovalStatusEnum, BidTypeEnum, BidProfileTypeInfo } from './dto/bidProfile.dto';
import { BidProfileServiceProxy, ProductsServiceProxy, BidProfileSaved, PlanServiceProxy, UserServiceProxy, User } from '@shared/service-proxies/service-proxies';
import { SharedService } from 'account/login/share.service';
import { AbpSessionService } from 'abp-ng2-module/dist/src/session/abp-session.service';


@Component({
    selector: 'app-bidProfile',
    templateUrl: './bidProfile.component.html',
    styleUrls: ['./bidProfile.component.css'],
    animations: [appModuleAnimation()]
})
export class BidProfileComponent extends AppComponentBase implements AfterViewInit, OnInit {

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
    //permission cho duyệt, thêm, xóa, sửa: Admin và Department tạo bidProfile đó.
    // duyệt: chỉ mỗi Admin đc duyệt
    // sửa, đóng: department tạo ra bidProfile đó và Admin.
    // thêm: ai thêm cũng đc, ko phân quyền
     isPermissionEditCloseActive = false;

     approvalStatusEnum = ApprovalStatusEnum;
     approvalStatus = 3; // all status
     ApprovalStatusList = [
        {
            id: ApprovalStatusEnum.All,
            name: 'All'
        },
        {
            id: ApprovalStatusEnum.Approved,
            name: 'Approved'
        },
        {
            id: ApprovalStatusEnum.Awaiting,
            name: 'Awaiting'
        }
    ];

     bidTypeEnum = BidTypeEnum;
     bidType = 1;
     bidTypes = [
        {
            id: BidTypeEnum.Bidding,
            name: 'Bidding'
        },
        {
            id: BidTypeEnum.AppointContractors,
            name: 'Appoint Contractors'
        }
    ];

    public bidProfileFakes = [
        {
            id: 1,
            code: 'S001',
            name: 'Purchase early in the year',
            bidCatalog: 'ProductCode1',
            startReceivedDate: '05/11/2018',
            endReceivedDate: '06/11/2018',
            projectCode: 'ProjectCode1',
            bidType: 1
        },
        {
            id: 2,
            code: 'S002',
            name: 'Purchase early in the year',
            bidCatalog: 'ProductCode1',
            startReceivedDate: '05/11/2018',
            endReceivedDate: '06/11/2018',
            projectCode: 'ProjectCode1',
            bidType: 2
        }
    ];

     createStartDatePickerOptions: IMyDpOptions = {
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
     editStartDatePickerOptions: IMyDpOptions = {
        selectorWidth: '270px',
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
     editEndDatePickerOptions: IMyDpOptions = {
        selectorWidth: '270px',
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
     createEndDatePickerOptions: IMyDpOptions = {
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

    //  model: any = { date: { year: new Date().getFullYear(), month: new Date().getMonth(), day: new Date().getDate() } };
    //  model = new Date();
     startDateString = undefined;
     endDateString = undefined;
     bidProfileCodeFilter = undefined;
     bidCatalogFilterId = undefined;
     bidCatalogEditId;
     userPermission: any[]= [];

    //api 8.7, get all products có status=1(active hay open)
     productInfos = [];
     productFakes = [];

     oldObject = {};

     myConfigStyleHeader: any = {
        'font-size': '11px'
    };
     myConfigStyle: any = {
        'font-size': '11px'
    };
     header;
    constructor(
        injector: Injector,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: BidProfileServiceProxy,
        private _productService: ProductsServiceProxy,
        private _sessionService: AbpSessionService,
        private _userService: UserServiceProxy,
    ) {
        super(injector);
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
        this.isPermissionEditCloseActive = true;
        // call hàm này khi subcribe api 8.7 get all product success
        this.handelSelects();
        this._userService.getUserForEdit(this._sessionService.userId).subscribe(user => {
            this.userPermission = user.memberedOrganizationUnits;
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

    handelSelects() {
        // filter products
            this._productService.getProducts(undefined, undefined, undefined, undefined, undefined, undefined).subscribe(result => {
                this.productFakes = result.items;
                result.items.forEach((item, i) => {
                    this.productInfos.push(
                        new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
                });
            });
    }
    /**
     * Hàm get danh sách BidProfile
     * @param event
     */
    getBidProfilesInit(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * Sử dụng _apiService để call các api của backend
         */

        this._apiService.getBidProfiles(
            undefined, undefined, undefined, undefined, undefined,
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


    getBidProfiles(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        // this.primengTableHelper.showLoadingIndicator();

        /**
         * Sử dụng _apiService để call các api của backend
         */
        this._apiService.getBidProfiles(
            this.bidProfileCodeFilter, this.endDateString ? moment(this.endDateString) : undefined, this.bidCatalogFilterId, this.bidTypes[this.bidType - 1].name, this.approvalStatus,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
                this.primengTableHelper.totalRecordsCount = 10;
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
     onScrollX(event): void {
        this.myConfigStyleHeader = {
            ...this.myConfigStyle,
            left: this.header ? `${this.header.getBoundingClientRect().left}px` : 'auto'
        };
    }

    compareOrganizationUnit(code: string ) {
        let count = (code.match(/000/g) || []).length;
        let k = 1;
        for (let i = 0; i < this.userPermission.length; i++) {
           if (code.indexOf(this.userPermission[i]) === 0) {
               if (k === count) {
                return true;
               }
               k++;
           }
        }
        return false;
    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        //truyền params lên url thông qua router
        this._router.navigate(['app/gwebsite/bidProfile', {
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

    //hàm show view create BidProfile
    createBidProfile() {
        this._router.navigate(['app/gwebsite/bidProfile/create']);
    }

     searchBidProfile(): void {
        //3 params filter FE truyền vào api
        // filter, values default = ''
        console.log(this.approvalStatus + '--' + this.bidProfileCodeFilter + '--' + this.bidCatalogFilterId +
        '--' + this.bidType + '--' + this.startDateString + '--' + this.endDateString);
    }

     onDateChangedByStart(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.startDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
     onDateChangedByEnd(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.endDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

     onDateChangedByEditStart(event: IMyDateModel): void {
        // const date = Object.assign({}, event);
        // this.startDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
     onDateChangedByEditEnd(event: IMyDateModel): void {
        // const date = Object.assign({}, event);
        // this.endDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

     actionEdit(row: any, $event: Event): void {
        $event.stopPropagation();
        this.oldObject['name'] = row.name;
        this.oldObject['bidCatalog'] = row.bidCatalog; // 1, 2, 3, ...
        this.oldObject['bidType'] = row.bidType; // 1, 2
        this.oldObject['projectCode'] = row.projectCode;
        this.oldObject['startReceivedDate'] = row.startReceivedDate;
        this.oldObject['endReceivedDate'] = row.endReceivedDate;

        this.bidCatalogEditId = 0;

        row.isEdit = true;
    }

     saveEditItem(id: number, row: any, $event: Event): void {
        $event.stopPropagation();

        if (this.isPermissionEditCloseActive && row.name && row.name !== '') {

            // vì bên html đã tự bind [(ngModel)] vào row.name và row.note rồi, nên ở đây ta chỉ cần lấy ra giá trị để update
            console.log(id + '---' + row.name + '---' + row.unitPrice + '---' + row.calUnit + '---' + row.description);
            if (this.bidCatalogEditId !== 0) {
                row.bidCatalog = this.productFakes.find(x => +x.id === this.bidCatalogEditId).code;
                //update TẠI ĐÂY với các params cần update là: row.name, row.bidCatalog
                // sau khi đã đc xử lý ở FE, e chỉ cần nhập mấy cái này là params đưa vào api là đc.
                //Hiện tại row.name, row.bidCatalog... đã mang giá trị mới, e chỉ cần gọi nó vào api update.
            }

            //save thành công
            this._apiService.updateBidProfileAsync(new BidProfileSaved({
                id,
                bidCatalog: row.bidCatalog,
                bidType: row.bidType,
                code: row.code,
                projectId: row.projectId, name: row.name,
                organizationUnitId: 1})).subscribe(item =>   row.isEdit = false);

        }

    }

     cancelEdit(row: any, $event: Event): void {
        $event.stopPropagation();

        row.name = this.oldObject['name'];
        row.bidCatalog = this.oldObject['bidCatalog'];

        row.unitPrice = this.oldObject['unitPrice'];
        row.calUnit = this.oldObject['calUnit'];
        row.description = this.oldObject['description'];

        row.isEdit = false;
    }

     actionPCItem(id: number, row: any): void {
        if (this.isPermissionEditCloseActive) {
            // dựa vào id, set status cho bidProfile là close nếu nó đang open và ngược lại.

            //sau khi set success
            row.status = row.status === ApprovalStatusEnum.Awaiting ? ApprovalStatusEnum.Approved : ApprovalStatusEnum.Awaiting;
        }
    }

    //chỉ những người có permission mới đc phép thực thi action với PC
     removePcItem(id: number, row: any, index: number, $event: Event): void {
        $event.stopPropagation();

        if (this.isPermissionEditCloseActive) {
            // dựa vào id truyền vào, remove

            this.primengTableHelper.records.splice(index, 1);
        }
        this.primengTableHelper.hideLoadingIndicator();
    }

    public gotoBidDetail(bidId: number, $event: Event): void {
        this._router.navigate(['app/gwebsite/bidProfile/detail/', bidId]);
    }
}
