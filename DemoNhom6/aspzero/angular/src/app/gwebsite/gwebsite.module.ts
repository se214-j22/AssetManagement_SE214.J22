import { CustomerServiceProxy, TaiSanServiceProxy, ModelServiceProxy, NhaCungCapServiceProxy, ThongTinXeServiceProxy, QuanLyVanHanhDto, QuanLyVanHanhServiceProxy } from './../../shared/service-proxies/service-proxies';
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
import { CalendarModule } from 'primeng/calendar';
import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CreateOrEditDemoModelModalComponent } from './demo-model/create-or-edit-demo-model-modal.component';
import { DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';
import { TaiSanComponent } from './taisan/taisan.component'
import { ModelComponent } from './model/model.component';
import { NhaCungCapComponent } from './nhacungcap/nhacungcap.component';
import { ThongTinXeComponent } from './thongtinxe/thongtinxe.component';
import { CreateOrEditThongTinXeModalComponent } from './thongtinxe/create-or-edit-thongtinxe-modal.component';
import { ViewThongTinXeModalComponent } from './thongtinxe/view-thongtinxe-modal.component';
import { ButtonModule } from 'primeng/button';
import { VanHanhXeComponent } from './vanhanhxe/vanhanhxe.component';
import { CreateOrEditVanHanhXeModalComponent } from './vanhanhxe/create-or-edit-vanhanhxe-modal.component';
import { ViewVanHanhXeModalComponent } from './vanhanhxe/view-vanhanhxe-modal.component';
import { ThongTinXeModalComponent } from './thongtinxe/thongtinxe-modal.component';
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
        InputMaskModule,
        ButtonModule,
        CalendarModule

    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        DemoModelComponent, CreateOrEditDemoModelModalComponent, ViewDemoModelModalComponent,
        TaiSanComponent, ModelComponent, NhaCungCapComponent,
        CustomerComponent, CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,
        ThongTinXeComponent, CreateOrEditThongTinXeModalComponent, ViewThongTinXeModalComponent, ThongTinXeModalComponent,
        VanHanhXeComponent, CreateOrEditVanHanhXeModalComponent, ViewVanHanhXeModalComponent,

    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        TaiSanServiceProxy,
        ModelServiceProxy,
        NhaCungCapServiceProxy,
        ThongTinXeServiceProxy,
        QuanLyVanHanhServiceProxy,
    ]
})
export class GWebsiteModule { }
