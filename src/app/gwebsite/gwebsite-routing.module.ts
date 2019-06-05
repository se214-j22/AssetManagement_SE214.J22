import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { NhomTaiSanComponent } from './nhomtaisan/nhomtaisan.component';
import { ThongTinTaiSanComponent } from './thongtintaisan/thongtintaisan.component';
import { XuatSuDungComponent } from './xuatsudung/xuatsudung.component';
import { DieuChuyenDto } from '@shared/service-proxies/service-proxies';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { SuaChuaComponent } from './suachua/suachua.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { ThanhLyComponent } from './thanhly/thanhly.component';
import { MenuClientComponent } from './menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';

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
                        path: 'customer', component:CustomerComponent,
                        data: { permission: 'Pages.Administration.Customer' }
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
                        path: 'thongtintaisan', component: ThongTinTaiSanComponent,
                        data: { permission: 'Pages.Administration.ThongTinTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'xuatsudung', component: XuatSuDungComponent,
                        data: { permission: 'Pages.Administration.XuatSuDung' }
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
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class GWebsiteRoutingModule { }
