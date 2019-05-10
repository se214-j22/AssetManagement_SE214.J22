import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
//qlts
import { CategoryComponent } from './category/category.component';
import { AssetComponent } from './asset/asset.component';
import { AssetDetailComponent } from './assetdetail/assetdetail.component';
import { ProviderComponent } from './provider/provider.component';
import { LiquidationComponent } from './liquidation/liquidation.component';
import { LiquidationDetailComponent } from './liquidationdetail/liquidationdetail.component';

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
                        path: 'demo-model', component: DemoModelComponent,
                        data: { permission: 'Pages.Administration.DemoModel' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'customer', component: CustomerComponent,
                        data: { permission: 'Pages.Administration.Customer' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'category', component: CategoryComponent,
                        data: { permission: 'Pages.Administration.Category' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'asset', component: AssetComponent,
                        data: { permission: 'Pages.Administration.Asset' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'assetdetail', component: AssetDetailComponent,
                        data: { permission: 'Pages.Administration.AssetDetail' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'provider', component: ProviderComponent,
                        data: { permission: 'Pages.Administration.Provider' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'liquidation', component: LiquidationComponent,
                        data: { permission: 'Pages.Administration.Liquidation' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'liquidationdetail', component: LiquidationDetailComponent,
                        data: { permission: 'Pages.Administration.LiquidationDetail' }
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
