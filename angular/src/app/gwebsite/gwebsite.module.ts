import { CustomerServiceProxy } from './../../shared/service-proxies/service-proxies';
import { ViewDemoModelModalComponent } from './demo-model/view-demo-model-modal.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule, PopoverModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { AutoCompleteModule, EditorModule, FileUploadModule as PrimeNgFileUploadModule, InputMaskModule, PaginatorModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';
import { GWebsiteRoutingModule } from './gwebsite-routing.module';

import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CreateOrEditDemoModelModalComponent } from './demo-model/create-or-edit-demo-model-modal.component';
import { DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { CustomerComponent } from './QuanLyBDS/BDS/customer.component';
import { Customer_SuaChuaComponent } from './QuanLyBDS/BDS_SuaChua/customer_SuaChua.component';
import { Customer_QuanLyCongTrinhXayDungComponent } from './QuanLyBDS/BDS_QuanLyCongTrinhXayDung/customer_QuanLyCongTrinhXayDung.component';
import { ViewCustomerModalComponent } from './QuanLyBDS/BDS/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './QuanLyBDS/BDS/create-or-edit-customer-modal.component';
import { ViewCustomer_SuaChuaModalComponent } from './QuanLyBDS/BDS_SuaChua/view-customer_SuaChua-modal.component';
import { CreateOrEditCustomer_SuaChuaModalComponent } from './QuanLyBDS/BDS_SuaChua/create-or-edit-customer_SuaChua-modal.component';
import { ViewCustomer_QuanLyCongTrinhXayDungModalComponent } from './QuanLyBDS/BDS_QuanLyCongTrinhXayDung/view-customer_QuanLyCongTrinhXayDung-modal.component';
import { CreateOrEditCustomer_QuanLyCongTrinhXayDungModalComponent } from './QuanLyBDS/BDS_QuanLyCongTrinhXayDung/create-or-edit-customer_QuanLyCongTrinhXayDung-modal.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        FileUploadModule,
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        GWebsiteRoutingModule,
        UtilsModule,
        AppCommonModule,
        TableModule,
        PaginatorModule,
        PrimeNgFileUploadModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule
    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        DemoModelComponent, CreateOrEditDemoModelModalComponent, ViewDemoModelModalComponent,
        CustomerComponent,Customer_SuaChuaComponent,Customer_QuanLyCongTrinhXayDungComponent, 
        CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,CreateOrEditCustomer_SuaChuaModalComponent,CreateOrEditCustomer_QuanLyCongTrinhXayDungModalComponent,
         ViewCustomer_SuaChuaModalComponent , ViewCustomer_QuanLyCongTrinhXayDungModalComponent
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy
    ]
})
export class GWebsiteModule { }
