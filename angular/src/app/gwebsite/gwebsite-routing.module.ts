import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
import { DonViCungCapTaiSanComponent } from './don-vi-cung-cap-tai-san/don-vi-cung-cap-tai-san.component';
import { PhongBanComponent } from './phong-ban/phong-ban.component';
import { HoaDonNhapComponent } from './hoa-don-nhap/hoa-don-nhap.component';
import { LoaiTaiSanComponent } from './loai-tai-san/loai-tai-san.component';
import { BangYeuCauCungCapTaiSanComponent } from './bang-yeu-cau-cung-cap-tai-san/bang-yeu-cau-cung-cap-tai-san.component';
import { TaiSanCoDinhComponent } from './tai-san-co-dinh/tai-san-co-dinh.component';
import { PhieuBaoDuongComponent } from './phieu-bao-duong/phieu-bao-duong.component';
import { BienBanThanhLyComponent } from './bien-ban-thanh-ly/bien-ban-thanh-ly.component';
import { BienBanBanGiaoTaiSanComponent } from './bien-ban-ban-giao-tai-san/bien-ban-ban-giao-tai-san.component';

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
                        path: 'don-vi-cung-cap-tai-san', component: DonViCungCapTaiSanComponent,
                        data: { permission: 'Pages.Administration.DonViCungCapTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'hoa-don-nhap', component: HoaDonNhapComponent,
                        data: { permission: 'Pages.Administration.HoaDonNhap' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'phong-ban', component: PhongBanComponent,
                        data: { permission: 'Pages.Administration.PhongBan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'loai-tai-san', component: LoaiTaiSanComponent,
                        data: { permission: 'Pages.Administration.LoaiTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'bang-yeu-cau-cung-cap-tai-san', component: BangYeuCauCungCapTaiSanComponent,
                        data: { permission: 'Pages.Administration.BangYeuCauCungCapTaiSan' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'tai-san-co-dinh', component: TaiSanCoDinhComponent,
                        data: { permission: 'Pages.Administration.TaiSanCoDinh' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'phieu-bao-duong', component: PhieuBaoDuongComponent,
                        data: { permission: 'Pages.Administration.PhieuBaoDuong' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'bien-ban-thanh-ly', component: BienBanThanhLyComponent,
                        data: { permission: 'Pages.Administration.BienBanThanhLy' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'bien-ban-ban-giao-tai-san', component: BienBanBanGiaoTaiSanComponent,
                        data: { permission: 'Pages.Administration.BienBanBanGiaoTaiSan' }
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
