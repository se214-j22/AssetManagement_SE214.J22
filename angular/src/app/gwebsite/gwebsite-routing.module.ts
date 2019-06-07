import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { ProductComponent } from './product/product.component';
import { ProductCategoryComponent } from './productCategory/productCategory.component';
import { PlanComponent } from './plan/plan.component';
import { SupplierComponent } from './supplier/supplier.component';
import { SupplierCategoryComponent } from './supplierCategory/supplierCategory.component';
import { ProjectComponent } from './project/project.component';
import { PurchaseContractComponent } from './purchaseContract/purchaseContract.component';
import { PurchaseOrderComponent } from './purchaseOrder/purchaseOrder.component';
import { SubmissionComponent } from './submission/submission.component';
import { SubPlanComponent } from './plan/sub-plan/sub-plan.component';
import { BidProfileComponent } from './bidProfile/bidProfile.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'menu-client', component: MenuClientComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'supplier-category', component: SupplierCategoryComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'supplier', component: SupplierComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'product-category', component: ProductCategoryComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'plan', component: PlanComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                    {
                        path: 'plan/detail/:id', component: SubPlanComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    }
                ]
            },
            {
                path: '',
                children: [

                    {
                        path: 'product', component: ProductComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                    {
                        path: 'supplier', component: SupplierComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    }
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'project', component: ProjectComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'bidProfile', component: BidProfileComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'purchaseContract', component: PurchaseContractComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'purchaseOrder', component: PurchaseOrderComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'submission', component: SubmissionComponent,
                        data: { permission: 'Pages.Administration.MenuClient' }
                    },
                ]
            },
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class GWebsiteRoutingModule { }
