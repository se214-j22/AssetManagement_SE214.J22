import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule, PopoverModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { AutoCompleteModule, EditorModule, FileUploadModule as PrimeNgFileUploadModule, InputMaskModule, PaginatorModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';
import { GWebsiteRoutingModule } from './gwebsite-routing.module';

import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { ProductCategoryComponent } from './productCategory/productCategory.component';
import { CreateOrEditProductCategoryModalComponent } from './productCategory/create-or-edit-productCategory-modal/create-or-edit-productCategory-modal.component';
import { SupplierServiceProxy, ProductsServiceProxy, ProjectServiceProxy } from '@shared/service-proxies/service-proxies';
import { ProductComponent } from './product/product.component';
import { CreateOrEditProductModalComponent } from './product/create-or-edit-product-modal/create-or-edit-product-modal.component';
import { PlanComponent } from './plan/plan.component';
import { CreateOrEditPlanModalComponent } from './plan/create-or-edit-plan-modal/create-or-edit-plan-modal.component';
import { SupplierComponent } from './supplier/supplier.component';
import { SupplierCategoryComponent } from './supplierCategory/supplierCategory.component';
import { CreateOrEditSupplierCategoryModalComponent } from './supplierCategory/create-or-edit-supplierCategory-modal/create-or-edit-supplierCategory-modal.component';
import { ProjectComponent } from './project/project.component';
import { CreateOrEditProjectModalComponent } from './project/create-or-edit-project-modal/create-or-edit-project-modal.component';
import { PurchaseContractComponent } from './purchaseContract/purchaseContract.component';
import { CreateOrEditPurchaseContractModalComponent } from './purchaseContract/create-or-edit-purchaseContract-modal/create-or-edit-purchaseContract-modal.component';
import { PurchaseOrderComponent } from './purchaseOrder/purchaseOrder.component';
import { CreateOrEditPurchaseOrderModalComponent } from './purchaseOrder/create-or-edit-purchaseOrder-modal/create-or-edit-purchaseOrder-modal.component';
import { SubmissionComponent } from './submission/submission.component';
import { CreateOrEditSubmissionModalComponent } from './submission/create-or-edit-submission-modal/create-or-edit-submission-modal.component';
import { SubPlanComponent } from './plan/sub-plan/sub-plan.component';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { CreateOrEditSubPlanModalComponent } from './plan/sub-plan/create-or-edit-subplan-modal/create-or-edit-subplan-modal.component';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { MyDatePickerModule } from 'mydatepicker';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CreateOrEditSupplierModalComponent } from './supplier/create-or-edit-supplier-modal/create-or-edit-supplier-modal.component';
import { BidProfileComponent } from './bidProfile/bidProfile.component';
import { CreateOrEditBidProfileModalComponent } from './bidProfile/create-or-edit-bidProfile-modal/create-or-edit-bidProfile-modal.component';



const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};

@NgModule({
    imports: [
        FormsModule,
        NgSelectModule,
        CommonModule,
        FileUploadModule,
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        GWebsiteRoutingModule,
        UtilsModule,
        AppCommonModule,
        TableModule,
        PaginatorModule,
        PrimeNgFileUploadModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule,
        MultiSelectModule,
        CalendarModule,
        DropdownModule,
        RadioButtonModule,
        PerfectScrollbarModule,
        MyDatePickerModule,
        InputSwitchModule
    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        SupplierCategoryComponent, CreateOrEditSupplierCategoryModalComponent,
        ProductCategoryComponent, CreateOrEditProductCategoryModalComponent,
        PlanComponent, CreateOrEditPlanModalComponent, SubPlanComponent,
        ProjectComponent, CreateOrEditProjectModalComponent,
        BidProfileComponent, CreateOrEditBidProfileModalComponent,
        PurchaseContractComponent, CreateOrEditPurchaseContractModalComponent,
        PurchaseOrderComponent, CreateOrEditPurchaseOrderModalComponent,
        SubmissionComponent, CreateOrEditSubmissionModalComponent,
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        PlanComponent, CreateOrEditPlanModalComponent, CreateOrEditSubPlanModalComponent,
        ProductComponent, CreateOrEditProductModalComponent,
        SupplierComponent, CreateOrEditSupplierModalComponent
    ],

    providers: [

        SupplierServiceProxy,
        ProductsServiceProxy,
        ProjectServiceProxy,
        {
            provide: PERFECT_SCROLLBAR_CONFIG,
            useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
        }
    ]
})
export class GWebsiteModule { }
