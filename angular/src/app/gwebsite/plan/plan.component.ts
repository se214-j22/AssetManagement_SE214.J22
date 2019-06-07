import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { CreateOrEditPlanModalComponent } from './create-or-edit-plan-modal/create-or-edit-plan-modal.component';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { ApprovalStatusEnum } from './dto/plan.dto';

@Component({
    selector: 'app-plan',
    templateUrl: './plan.component.html',
    styleUrls: ['./plan.component.css'],
    animations: [appModuleAnimation()]
})
export class PlanComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('textsTable') textsTable: ElementRef;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditPlanModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    /**
     * tạo các biến dể filters
     */
    public filterText: string;
    public approvalStatusEnum = ApprovalStatusEnum;
    public approvalStatus = 3; // all status
    public ApprovalStatusList = [
        {
            id: ApprovalStatusEnum.AllStatus,
            name: 'All Status'
        },
        {
            id: ApprovalStatusEnum.Approved,
            name: 'Approved'
        },
        {
            id: ApprovalStatusEnum.AwaitingApproval,
            name: 'Awaiting Approval'
        }
    ];

    public YearImplementList = ['All Year', '2016', '2017', '2018', '2019', '2020', '2021', '2022', '2023', '2024'];
    public yearImplement = this.YearImplementList[0];
    public UnitCodeList = ['All Units', 'HN', 'HP', 'DN', 'TPHCM', 'CT'];
    public unitCode = this.UnitCodeList[0];

    public DepartmentCodeList = ['All Departments', 'IT', 'HR', 'Acco', 'Mark', 'Sale', 'PR'];
    public deparmentCode = this.DepartmentCodeList[0];
    public planIdFilter = 0;

    public datas = [
        {
            planId: 11,
            effectiveDate: '01/01/2019',
            totalPrice: 1000000,
            unitCode: 'HN',
            departmentCode: 'IT',
            status: ApprovalStatusEnum.AwaitingApproval,
            countChanged: 1
        },
        {
            planId: 22,
            effectiveDate: '02/01/2019',
            totalPrice: 2000000,
            unitCode: 'TPHCM1',
            departmentCode: 'HR',
            status: ApprovalStatusEnum.Approved,
            countChanged: 3
        },
        {
            planId: 33,
            effectiveDate: '03/01/2019',
            totalPrice: 3000000,
            unitCode: 'HP',
            departmentCode: 'Acco',
            status: ApprovalStatusEnum.Approved,
            countChanged: 2
        },
        {
            planId: 44,
            effectiveDate: '04/01/2019',
            totalPrice: 4000000,
            unitCode: 'DN',
            departmentCode: 'Mark',
            status: ApprovalStatusEnum.AwaitingApproval,
            countChanged: 1
        },
        {
            planId: 55,
            effectiveDate: '01/01/2019',
            totalPrice: 1000000,
            unitCode: 'TPHCM2',
            departmentCode: 'IT',
            status: ApprovalStatusEnum.AwaitingApproval,
            countChanged: 5
        },
        {
            planId: 66,
            effectiveDate: '02/01/2019',
            totalPrice: 2000000,
            unitCode: 'CT',
            departmentCode: 'Sale',
            status: ApprovalStatusEnum.Approved,
            countChanged: 3
        },
        {
            planId: 77,
            effectiveDate: '03/01/2019',
            totalPrice: 3000000,
            unitCode: 'DN',
            departmentCode: 'IT',
            status: ApprovalStatusEnum.Approved,
            countChanged: 2
        },
        {
            planId: 88,
            effectiveDate: '04/01/2019',
            totalPrice: 4000000,
            unitCode: 'HN2',
            departmentCode: 'Marketing',
            status: ApprovalStatusEnum.AwaitingApproval,
            countChanged: 6
        },
        {
            planId: 99,
            effectiveDate: '01/01/2019',
            totalPrice: 1000000,
            unitCode: 'NT',
            departmentCode: 'PR',
            status: ApprovalStatusEnum.AwaitingApproval,
            countChanged: 4
        },
        {
            planId: 100,
            effectiveDate: '02/01/2019',
            totalPrice: 2000000,
            unitCode: 'TPHCM3',
            departmentCode: 'HR',
            status: ApprovalStatusEnum.Approved,
            countChanged: 2
        }
    ];
    public isRoleApprovedMan = false;

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
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
        //dựa vào user id, get role approve cho user đó
        // nếu người đó có quyền duyệt thì mới cho hiện btn Approved lên

        //nếu là roles approved thì
        this.isRoleApprovedMan = true;

        //gọi api get plan theo paging, sử dụng hàm getPlans để gọi
        // hoặc gọi trong hàm init() bên dưới
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
     * onScrollX
     * @param event
     */
    public onScrollX(event): void {
        this.myConfigStyleHeader = {
            ...this.myConfigStyle,
            left: this.header ? `${this.header.getBoundingClientRect().left}px` : 'auto'
        };
    }

    /**
     * Hàm get danh sách Plan
     * @param event
     */
    getPlans(event?: LazyLoadEvent) {
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
        this.primengTableHelper.records = this.datas;
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

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
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
        if (this.createOrEditModal.plan.id) {
            for (let i = 0; i < this.primengTableHelper.records.length; i++) {
                if (this.primengTableHelper.records[i].id === this.createOrEditModal.plan.id) {
                    this.primengTableHelper.records[i] = this.createOrEditModal.plan;
                    return;
                }
            }
        } else { this.reloadPage(); }
    }

    //hàm show view create Plan
    createPlan() {
        this.createOrEditModal.show();
    }
    public searchPlan(): void {

    }

    public approvalPlan(planId: number, $event: Event, index: number): void {
        $event.stopPropagation();
        this.datas[index].status = ApprovalStatusEnum.Approved;

        //call api approved cho planId này.
    }

    public gotoPlanDetail(planId: number, $event: Event): void {
        this._router.navigate(['app/gwebsite/plan/detail/', planId]);
    }
}
