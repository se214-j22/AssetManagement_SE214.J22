import { CustomerServiceProxy, TaiSanServiceProxy, XuatTaiSanServiceProxy, ThuHoiServiceProxy, DieuChuyenServiceProxy, NhomTaiSanServiceProxy, DonViServiceProxy, NhanVienServiceProxy, CTDonViServiceProxy } from './../../shared/service-proxies/service-proxies';
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
import { XuatTaiSanComponent } from './xuattaisan/xuattaisan.component';
import { CreateOrEditXuatTaiSanModalComponent } from './xuattaisan/create-or-edit-xuattaisan-modal.component';
import { ViewXuatTaiSanModalComponent } from './xuattaisan/view-xuattaisan-modal.component';
import { ThuHoiComponent } from './thuhoi/thuhoi.component';
import { CreateOrEditThuHoiModalComponent } from './thuhoi/create-or-edit-thuhoi-modal.component';
import { ViewThuHoiModalComponent } from './thuhoi/view-thuhoi-modal.component';
import { DieuChuyenComponent } from './dieuchuyen/dieuchuyen.component';
import { CreateOrEditDieuChuyenModalComponent } from './dieuchuyen/create-or-edit-dieuchuyen-modal.component';
import { ViewDieuChuyenModalComponent } from './dieuchuyen/view-dieuchuyen-modal.component';
import { NhomTaiSanComponent } from './nhomtaisan/nhomtaisan.component';
import { CreateOrEditNhomTaiSanModalComponent } from './nhomtaisan/create-or-edit-nhomtaisan-modal.component';
import { ViewNhomTaiSanModalComponent } from './nhomtaisan/view-nhomtaisan-modal.component';
import { SearchTaiSanComponent } from './nhomtaisan/search-taisan.component';
import { TaiSanFindComponent } from './taisan/taisan-find.component';
import { DonViComponent } from './donvi/donvi.component';
import { CreateOrEditDonViModalComponent } from './donvi/create-or-edit-donvi-modal.component';
import { ViewDonViModalComponent } from './donvi/view-donvi-modal.component';
import { NhanVienComponent } from './nhanvien/nhanvien.component';
import { CreateOrEditNhanVienModalComponent } from './nhanvien/create-or-edit-nhanvien-modal.component';
import { ViewNhanVienModalComponent } from './nhanvien/view-nhanvien-modal.component';
import { CTDonViComponent } from './donvi/donvi-chitiet.component';

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
        TaiSanComponent, CreateOrEditTaiSanModalComponent, ViewTaiSanModalComponent, TaiSanFindComponent,
        NhomTaiSanComponent, CreateOrEditNhomTaiSanModalComponent, ViewNhomTaiSanModalComponent, SearchTaiSanComponent, //6-3 searchTaiSan
        XuatTaiSanComponent, CreateOrEditXuatTaiSanModalComponent, ViewXuatTaiSanModalComponent,
        DieuChuyenComponent, CreateOrEditDieuChuyenModalComponent, ViewDieuChuyenModalComponent,
        ThuHoiComponent, CreateOrEditThuHoiModalComponent, ViewThuHoiModalComponent,
        DonViComponent, CreateOrEditDonViModalComponent, ViewDonViModalComponent, CTDonViComponent,
        NhanVienComponent, CreateOrEditNhanVienModalComponent, ViewNhanVienModalComponent
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        TaiSanServiceProxy,
        NhomTaiSanServiceProxy,
        XuatTaiSanServiceProxy,
        DieuChuyenServiceProxy,
        ThuHoiServiceProxy,
        DonViServiceProxy,
        CTDonViServiceProxy,
        NhanVienServiceProxy
    ]
})
export class GWebsiteModule { }
