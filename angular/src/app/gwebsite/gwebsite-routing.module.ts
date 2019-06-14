import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
import { TaiSanComponent } from './taisan/taisan.component';
import { XuatTaiSanComponent } from './xuattaisan/xuattaisan.component';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { NhomTaiSanComponent } from './nhomtaisan/nhomtaisan.component';
import { DonViComponent } from './donvi/donvi.component';
import { NhanVienComponent } from './nhanvien/nhanvien.component';
import { SuaChuaComponent } from './suachua/suachua.component';
import { ThanhLyComponent } from './thanhly/thanhly.component';

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
                        path: 'suachua', component: SuaChuaComponent,
                        data: { permission: 'Pages.Administration.SuaChua' }
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
                        path: 'thanhly', component: ThanhLyComponent,
                        data: { permission: 'Pages.Administration.ThanhLy' }
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
