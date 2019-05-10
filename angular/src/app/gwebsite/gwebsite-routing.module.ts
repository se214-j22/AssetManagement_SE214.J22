import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './customer/customer.component';
import { TaiSanComponent } from './taisan/taisan.component';
import { CapPhatComponent } from './capphat/capphat.component';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';

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
                        path: 'capphat', component: CapPhatComponent,
                        data: { permission: 'Pages.Administration.CapPhat' }
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
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class GWebsiteRoutingModule { }
