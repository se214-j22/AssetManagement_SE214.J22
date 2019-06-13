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
// import { UseAssetComponent } from './useasset/useasset.component';
import { TaiSanComponent } from './taisan/taisan.component';
import { XuatTaiSanComponent } from './xuattaisan/xuattaisan.component';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { NhomTaiSanComponent } from './nhomtaisan/nhomtaisan.component';
import { DonViComponent } from './donvi/donvi.component';
import { NhanVienComponent } from './nhanvien/nhanvien.component';


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
            // {
            //     path: '',
            //     children: [
            //         {
            //             path: 'useasset', component: UseAssetComponent,
            //             data: { permission: 'Pages.Administration.UseAsset' }
            //         },
            //     ]
            // },
            {
                path: '',
                children: [
                    {
                        path: 'taisan', component: TaiSanComponent,
                        data: { permission: 'Pages.Administration.TaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'nhomtaisan', component: NhomTaiSanComponent,
                        data: { permission: 'Pages.Administration.NhomTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'xuattaisan', component: XuatTaiSanComponent,
                        data: { permission: 'Pages.Administration.XuatTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'dieuchuyen', component: DieuChuyenComponent,
                        data: { permission: 'Pages.Administration.DieuChuyen' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'thuhoi', component: ThuHoiComponent,
                        data: { permission: 'Pages.Administration.ThuHoi' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'donvi', component: DonViComponent,
                        data: { permission: 'Pages.Administration.DonVi' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'nhanvien', component: NhanVienComponent,
                        data: { permission: 'Pages.Administration.NhanVien' }
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
