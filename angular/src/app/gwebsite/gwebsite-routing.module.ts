import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
import { AssetComponent } from './asset/asset.component';
import { AssetLineComponent } from './asset-line/asset-line.component';
import { AssetTypeComponent } from './asset-type/asset-type.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';

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
                        path: 'manufacturer', component: ManufacturerComponent,
                        data: { permission: 'Pages.Administration.Manufacturer' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'asset-type', component: AssetTypeComponent,
                        data: { permission: 'Pages.Administration.AssetType' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'asset-line', component: AssetLineComponent,
                        data: { permission: 'Pages.Administration.AssetLine' }
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
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class GWebsiteRoutingModule { }
