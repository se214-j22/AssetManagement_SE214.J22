import { CustomerServiceProxy, NhomTaiSanServiceProxy, ThongTinTaiSanServiceProxy,DieuChuyenServiceProxy,
XuatSuDungServiceProxy,SuaChuaServiceProxy,ThuHoiServiceProxy,ThanhLyServiceProxy } from './../../shared/service-proxies/service-proxies';

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


import { NhomTaiSanComponent } from './nhomtaisan/nhomtaisan.component';
import { ViewNhomTaiSanModalComponent } from './nhomtaisan/view-nhomtaisan-modal.component';
import { CreateOrEditNhomTaiSanModalComponent } from './nhomtaisan/create-or-edit-nhomtaisan-modal.component';


import { ThongTinTaiSanComponent } from './thongtintaisan/thongtintaisan.component';
import { ViewThongTinTaiSanModalComponent } from './thongtintaisan/view-thongtintaisan-modal.component';
import { CreateOrEditThongTinTaiSanModalComponent } from './thongtintaisan/create-or-edit-thongtintaisan-modal.component';

import { XuatSuDungComponent } from './xuatsudung/xuatsudung.component';
import { ViewXuatSuDungModalComponent } from './xuatsudung/view-xuatsudung-modal.component';
import { CreateOrEditXuatSuDungModalComponent } from './xuatsudung/create-or-edit-xuatsudung-modal.component';

import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { ViewDieuChuyenModalComponent } from './dieuchuyen/view-dieuchuyen-modal.component';
import { CreateOrEditDieuChuyenModalComponent } from './dieuchuyen/create-or-edit-dieuchuyen-modal.component';

import { SuaChuaComponent } from './suachua/suachua.component';
import { ViewSuaChuaModalComponent } from './suachua/view-suachua-modal.component';
import { CreateOrEditSuaChuaModalComponent } from './suachua/create-or-edit-suachua-modal.component';

import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { ViewThuHoiModalComponent } from './thuhoi/view-thuhoi-modal.component';
import { CreateOrEditThuHoiModalComponent } from './thuhoi/create-or-edit-thuhoi-modal.component';

import { ThanhLyComponent } from './thanhly/thanhly.component';
import { ViewThanhLyModalComponent } from './thanhly/view-thanhly-modal.component';
import { CreateOrEditThanhLyModalComponent } from './thanhly/create-or-edit-thanhly-modal.component';

import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CreateOrEditDemoModelModalComponent } from './demo-model/create-or-edit-demo-model-modal.component';
import { DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';
import { ViewDemoModelModalComponent } from './demo-model/view-demo-model-modal.component';


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
        CustomerComponent, CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,
        NhomTaiSanComponent, CreateOrEditNhomTaiSanModalComponent, ViewNhomTaiSanModalComponent,
        ThongTinTaiSanComponent, CreateOrEditThongTinTaiSanModalComponent, ViewThongTinTaiSanModalComponent,
        XuatSuDungComponent, CreateOrEditXuatSuDungModalComponent, ViewXuatSuDungModalComponent,
        DieuChuyenComponent, CreateOrEditDieuChuyenModalComponent, ViewDieuChuyenModalComponent,
        SuaChuaComponent, CreateOrEditSuaChuaModalComponent, ViewSuaChuaModalComponent,
        ThuHoiComponent, CreateOrEditThuHoiModalComponent, ViewThuHoiModalComponent,
        ThanhLyComponent, CreateOrEditThanhLyModalComponent, ViewThanhLyModalComponent,
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        NhomTaiSanServiceProxy,
        ThongTinTaiSanServiceProxy,
        XuatSuDungServiceProxy,
        DieuChuyenServiceProxy,
        SuaChuaServiceProxy,
        ThuHoiServiceProxy,
        ThanhLyServiceProxy,
    ]
})
export class GWebsiteModule { }
