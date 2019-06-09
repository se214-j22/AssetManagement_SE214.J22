import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router, ParamMap } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { ApprovalStatusEnum } from '../dto/plan.dto';
import { CreateOrEditSubPlanModalComponent } from './create-or-edit-subplan-modal/create-or-edit-subplan-modal.component';
import { switchMap } from 'rxjs/operators';
import { SubPlanServiceProxy, SubPlanSavedDto, PlanDto, SubPlanDto, PlanServiceProxy } from '@shared/service-proxies/service-proxies';

// import { ScrollableView } from 'primeng/table';
// import ResizeObserver from 'resize-observer-polyfill';
// https://www.npmjs.com/package/resize-observer-polyfill
// https://stackblitz.com/edit/primeng-dynamic-scrollable
// ScrollableView.prototype.ngAfterViewChecked = function () {
//   if (!this.initialized && this.el.nativeElement.offsetParent) {
//     this.alignScrollBar();
//     this.initialized = true;
//     new ResizeObserver(entries => {
//       //for (let entry of entries)
//       this.alignScrollBar();
//     }).observe(this.scrollBodyViewChild.nativeElement);
//   }
// };

@Component({
  selector: 'app-sub-plan',
  templateUrl: './sub-plan.component.html',
  styleUrls: ['./sub-plan.component.css'],
  animations: [appModuleAnimation()]
})
export class SubPlanComponent extends AppComponentBase implements AfterViewInit, OnInit {

  /**
   * @ViewChild là dùng get control và call thuộc tính, functions của control đó
   */
  @ViewChild('textsTable') textsTable: ElementRef;
  @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditSubPlanModalComponent;
  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;

  /**
   * tạo các biến dể filters
   */
  public filterText: string;
  public productCode: number = undefined;
  public YearImplementList = [(new Date()).getFullYear()];
  public approvalStatusEnum = ApprovalStatusEnum;
  public approvalStatus = ApprovalStatusEnum.AwaitingApproval;
  public ApprovalStatusList = [
    {
      id: this.approvalStatus,
      name: this.approvalStatus === 1 ? 'Approved' : 'Awaiting Approval'
    }
  ];
  public effectiveDate = '10/05/2019';
  public totalPrice = 0;
  public implementPrice = 0;
  public residualPrice = +this.totalPrice - +this.implementPrice;



  public isRoleApprovedMan = false;
  public planId: number;

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
    private _apiService: SubPlanServiceProxy, private apiService: PlanServiceProxy
  ) {
    super(injector);
  }

  /**
   * Hàm xử lý trước khi View được init
   */

  plan: PlanDto = new PlanDto();
  ngOnInit(): void {
    //dựa vào user id, get role approve cho user đó
    // nếu người đó có quyền duyệt thì cũng có quyền editQty(phòng ban tạo plan cũng edit đc)
    // nhưng nếu như đã duyệt, thì chỉ có role approved thì mới có quyền edit, còn role department ko đc quyền edit khi đã duyệt

    //nếu là roles approved thì
    this.isRoleApprovedMan = true;

    //https://angular.io/guide/router

    // let planObject = this._activatedRoute.paramMap.pipe(
    //   switchMap((params: ParamMap) =>
    //     // this.service.getHero(params.get('id')))
    //     this.id = params.get('id'))
    // );

    // id lấy từ url là sting, phải dùng + để parse sang number
    this.planId = +this._activatedRoute.snapshot.paramMap.get('id');
    this.apiService.getPlanForEdit(this.planId).subscribe(result => {
      this.plan = result;
      this.approvalStatus = result.status;
      this.ApprovalStatusList = [
        {
          id: this.approvalStatus,
          name: this.approvalStatus === 1 ? 'Approved' : 'Awaiting Approval'
        }
      ];
      this.totalPrice = result.totalPrice;
      this.YearImplementList = [result.implementDate.year()];
      result.subPlans.map(item => this.implementPrice += item.implementPrice);
      this.residualPrice = +this.totalPrice - +this.implementPrice;
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
   * Hàm get danh sách SubPlan
   * @param event
   */
  getSubPlans(event?: LazyLoadEvent) {
    if (!this.paginator || !this.dataTable) {
      return;
    }

    //show loading trong gridview
    this.primengTableHelper.showLoadingIndicator();

    /**
     * Sử dụng _apiService để call các api của backend
     */

    this._apiService.getSubPlans(undefined, this.productCode, this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getMaxResultCount(this.paginator, event),
      this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
        this.primengTableHelper.totalRecordsCount = 10;
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.records.forEach((row: any, i: number) => {
          row.isEdit = false;
          row.quantityEdit = 0;
        });
        this.primengTableHelper.hideLoadingIndicator();
      });
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

  public approvedPlan(): void {
    // this.planId: approved theo planId
    if (this.approvalStatus === ApprovalStatusEnum.AwaitingApproval) {
      this.apiService.approvedPlanAsync(this.planId).subscribe(result => {
        this.approvalStatus = ApprovalStatusEnum.Approved;
        this.ApprovalStatusList[0].id = 1;
        this.ApprovalStatusList[0].name = 'Approved';
      });
    }

  }


  public activeEditRow(row: any): void {
    row.quantityEdit = row.quantity;
    row.isEdit = true;
  }

  public saveEditRow(row: any): void {
    //call api save
    //save row.quantityEdit to quantity of row.productCode in this.planId
    console.log(row.quantityEdit);

    row.quantity = row.quantityEdit;
    //done save
    row.isEdit = false;

    // Chú ý: mỗi lần save change một subplan thành công, thì dưới BE tự tăng CountChanged++ cho Plan lớn (bao gồm các subplan)
    // theo planId.
    console.log({ planId: row.planId, productId: row.product.id, quantity: row.quantity });
    this._apiService.updateSubPlanAsync(new SubPlanSavedDto({ planId: row.planId, productId: row.product.id, quantity: row.quantity })).subscribe(item => console.log(item));
  }

  public cancelEditRow(row: any): void {
    row.isEdit = false;
  }

  //Refresh grid khi thực hiện create or edit thành công
  refreshValueFromModal(event): void {
    // if (this.createOrEditModal.newProduct.planId) {
    //   for (let i = 0; i < this.primengTableHelper.records.length; i++) {
    //     if (this.primengTableHelper.records[i].id === this.createOrEditModal.newProduct.planId) {
    //       this.primengTableHelper.records[i] = this.createOrEditModal.newProduct;
    //       return;
    //     }
    //   }
    // } else { this.reloadPage(); }
    this._apiService.getSubPlans(undefined, this.productCode, this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getMaxResultCount(this.paginator, event),
      this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
        this.primengTableHelper.totalRecordsCount = 10;
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.records.forEach((row: any, i: number) => {
          row.isEdit = false;
          row.quantityEdit = 0;
        });
        this.primengTableHelper.hideLoadingIndicator();
      });
  }

  //hàm show view create Plan
  createSubPlan() {
    this.createOrEditModal.show(this.planId);
  }
}
