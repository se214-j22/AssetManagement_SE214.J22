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
import { PlanServiceProxy } from '@shared/service-proxies/service-proxies';

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
    public yearImplement = undefined;
    public UnitCodeList = ['All Units', 'HN', 'HP', 'DN', 'TPHCM', 'CT'];
    public unitCode = undefined;

    DepartmentCodeList = [];


    public deparmentCode = undefined;
    public planIdFilter = undefined;

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
        private _apiService: PlanServiceProxy
    ) {
        super(injector);
        this.DepartmentCodeList = ['All Departments', 'IT', 'HR', 'Acco', 'Mark', 'Sale', 'PR'];
        this._apiService.getAllDepartment().subscribe((item: any) => {

            this.DepartmentCodeList = item;
            this.DepartmentCodeList.unshift('All Departments');
        }, err => console.log(err));
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

        this._apiService.getPlans(
            this.planIdFilter, this.yearImplement, this.approvalStatus, this.unitCode, this.deparmentCode,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
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

    public approvalPlan(record: any, $event: Event, index: number): void {
        $event.stopPropagation();
        this._apiService.approvedPlanAsync(record.id).subscribe((i) => {
            record.status = ApprovalStatusEnum.Approved;
        }, err => console.log(err));
        //call api approved cho planId này.
    }

    public gotoPlanDetail(planId: number, $event: Event): void {
        this._router.navigate(['app/gwebsite/plan/detail/', planId]);
    }
}
