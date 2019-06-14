import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CustomerComponent } from './QuanLyBDS/BDS/customer.component';
import { Customer_SuaChuaComponent } from './QuanLyBDS/BDS_SuaChua/customer_SuaChua.component';
import { Customer_QuanLyCongTrinhXayDungComponent } from './QuanLyBDS/BDS_QuanLyCongTrinhXayDung/customer_QuanLyCongTrinhXayDung.component';

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
                        path: 'BDS', component: CustomerComponent,
                        data: { permission: 'Pages.Administration.Customer' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'BDS_SuaChua', component: Customer_SuaChuaComponent,
                        data: { permission: 'Pages.Administration.Customer_SuaChua' }
                    },
                ]
            },
            {
                path: '',
                children: [
                    {
                        path: 'BDS_QuanLyCongTrinhXayDung', component: Customer_QuanLyCongTrinhXayDungComponent,
                        data: { permission: 'Pages.Administration.Customer_QuanLyCongTrinhXayDung' }
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
