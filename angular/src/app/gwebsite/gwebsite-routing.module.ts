import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
//qlts
import { AssetGroupComponent } from './assetgroup/assetgroup.component';
import { AssetComponent } from './asset/asset.component';
import { LiquidationComponent } from './liquidation/liquidation.component';
// import { RepairComponent } from './repair/repair.component';
import { RevokeComponent } from './revoke/revoke.component';
// import { TransferComponent } from './transfer/transfer.component';
import { UseAssetComponent } from './useasset/useasset.component';
import { CreateAssetComponent } from './asset/create-asset.component';


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
                        path: 'assetgroup', component: AssetGroupComponent,
                        data: { permission: 'Pages.Administration.AssetGroup' }
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
                        path: 'asset/create-asset', component: CreateAssetComponent,
                        data: { permission: 'Pages.Administration.Asset' },
                    }
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
            // {
            //     path: '',
            //     children: [
            //         {
            //             path: 'repair', component: RepairComponent,
            //             data: { permission: 'Pages.Administration.Repair' }
            //         },
            //     ]
            // },
            {
                path: '',
                children: [
                    {
                        path: 'revoke', component: RevokeComponent,
                        data: { permission: 'Pages.Administration.Revoke' }
                    },
                ]
            },
            // {
            //     path: '',
            //     children: [
            //         {
            //             path: 'transfer', component: TransferComponent,
            //             data: { permission: 'Pages.Administration.Transfer' }
            //         },
            //     ]
            // },
            {
                path: '',
                children: [
                    {
                        path: 'useasset', component: UseAssetComponent,
                        data: { permission: 'Pages.Administration.UseAsset' }
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
