import { CustomerServiceProxy, TaiSanServiceProxy, CapPhatServiceProxy, ThuHoiServiceProxy, DieuChuyenServiceProxy } from './../../shared/service-proxies/service-proxies';
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
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';
import { TaiSanComponent } from './taisan/taisan.component';
import { CreateOrEditTaiSanModalComponent } from './taisan/create-or-edit-taisan-modal.component';
import { ViewTaiSanModalComponent } from './taisan/view-taisan-modal.component';
import { CapPhatComponent } from './capphat/capphat.component';
import { CreateOrEditCapPhatModalComponent } from './capphat/create-or-edit-capphat-modal.component';
import { ViewCapPhatModalComponent } from './capphat/view-capphat-modal.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { CreateOrEditThuHoiModalComponent } from './thuhoi/create-or-edit-thuhoi-modal.component';
import { ViewThuHoiModalComponent } from './thuhoi/view-thuhoi-modal.component';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { CreateOrEditDieuChuyenModalComponent } from './dieuchuyen/create-or-edit-dieuchuyen-modal.component';
import { ViewDieuChuyenModalComponent } from './dieuchuyen/view-dieuchuyen-modal.component';

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
        TaiSanComponent, CreateOrEditTaiSanModalComponent, ViewTaiSanModalComponent,
        CapPhatComponent, CreateOrEditCapPhatModalComponent, ViewCapPhatModalComponent,
        DieuChuyenComponent, CreateOrEditDieuChuyenModalComponent, ViewDieuChuyenModalComponent,
        ThuHoiComponent, CreateOrEditThuHoiModalComponent, ViewThuHoiModalComponent
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        TaiSanServiceProxy,
        CapPhatServiceProxy,
        DieuChuyenServiceProxy,
        ThuHoiServiceProxy
    ]
})
export class GWebsiteModule { }
