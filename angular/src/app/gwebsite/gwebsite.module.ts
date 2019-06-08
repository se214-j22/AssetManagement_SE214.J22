import { CustomerServiceProxy, TaiSanServiceProxy, ModelServiceProxy, NhaCungCapServiceProxy, ThongTinXeServiceProxy, QuanLyVanHanhServiceProxy, ThongTinBaoHiemServiceProxy, PhiDuongBoServiceProxy, ThongTinBaoDuongServiceProxy, ThongTinDangKiemServiceProxy, ThietBiKemTheoServiceProxy, ThongTinSuaChuaServiceProxy, CheckServiceProxy } from './../../shared/service-proxies/service-proxies';
import { ViewDemoModelModalComponent } from './demo-model/view-demo-model-modal.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule, PopoverModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { AutoCompleteModule, EditorModule, FileUploadModule as PrimeNgFileUploadModule, InputMaskModule, PaginatorModule, ButtonModule, CalendarModule, DropdownModule, CheckboxModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';
import { GWebsiteRoutingModule } from './gwebsite-routing.module';

import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CreateOrEditDemoModelModalComponent } from './demo-model/create-or-edit-demo-model-modal.component';
import { DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';
import { ThongTinXeComponent } from './thongtinxe/thongtinxe.component';
import { CreateOrEditThongTinXeModalComponent } from './thongtinxe/create-or-edit-thongtinxe-modal.component';
import { ViewThongTinXeModalComponent } from './thongtinxe/view-thongtinxe-modal.component';
import { ThongTinXeModalComponent } from './thongtinxe/thongtinxe-modal.component';
import { VanHanhXeComponent } from './vanhanhxe/vanhanhxe.component';
import { CreateOrEditVanHanhXeModalComponent } from './vanhanhxe/create-or-edit-vanhanhxe-modal.component';
import { ViewVanHanhXeModalComponent } from './vanhanhxe/view-vanhanhxe-modal.component';
import { ThongTinBaoHiemComponent } from './thongtinbaohiem/thongtinbaohiem.component';
import { CreateOrEditBaoHiemXeModalComponent } from './thongtinbaohiem/create-or-edit-thongtinbaohiem-modal.component';
import { ViewBaoHiemXeModalComponent } from './thongtinbaohiem/view-thongtinbaohiem-modal.component';
import { PhiDuongBoComponent } from './phiduongbo/phiduongbo.component';
import { CreateOrEditPhiDuongBoModalComponent } from './phiduongbo/create-or-edit-phiduongbo-modal.component';
import { ViewPhiDuongBoModalComponent } from './phiduongbo/view-phiduongbo-modal.component';
import { ThongTinBaoDuongComponent } from './thongtinbaoduong/thongtinbaoduong.component';
import { CreateOrEditThongTinBaoDuongModalComponent } from './thongtinbaoduong/create-or-edit-thongtinbaoduong-modal.component';
import { ViewThongTinBaoDuongModalComponent } from './thongtinbaoduong/view-thongtinbaoduong-modal.component';
import { ThongTinDangKiemComponent } from './thongtindangkiem/thongtindangkiem.component';
import { CreateOrEditDangKiemXeModalComponent } from './thongtindangkiem/create-or-edit-thongtindangkiem-modal.component';
import { ViewDangKiemXeModalComponent } from './thongtindangkiem/view-thongtindangkiem-modal.component';
import { ThongTinSuaChuaComponent } from './thongTinSuaChua/thongtinsuachua.component';
import { CreateOrEditThongTinSuaChuaModalComponent } from './thongTinSuaChua/create-or-edit-thongtinsuachua-modal.component';
import { ViewThongTinSuaChuaModalComponent } from './thongTinSuaChua/view-thongtinsuachua-modal.component';
import { ChiTietXeComponent } from './chitietxe/chitietxe.component';
import { NhaCungCapComponent } from './nhacungcap/nhacungcap.component';
import { TaiSanComponent } from './taisan/taisan.component';
import { ModelComponent } from './model/model.component';

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
        CalendarModule,
        DropdownModule,
        CheckboxModule
    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        DemoModelComponent, CreateOrEditDemoModelModalComponent, ViewDemoModelModalComponent,
        CustomerComponent, CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,
        ThongTinXeComponent, CreateOrEditThongTinXeModalComponent, ViewThongTinXeModalComponent, ThongTinXeModalComponent,
        VanHanhXeComponent, CreateOrEditVanHanhXeModalComponent, ViewVanHanhXeModalComponent,
        ThongTinBaoHiemComponent, CreateOrEditBaoHiemXeModalComponent, ViewBaoHiemXeModalComponent,
        PhiDuongBoComponent, CreateOrEditPhiDuongBoModalComponent, ViewPhiDuongBoModalComponent,
        ThongTinBaoDuongComponent, CreateOrEditThongTinBaoDuongModalComponent, ViewThongTinBaoDuongModalComponent,
        ThongTinDangKiemComponent, CreateOrEditDangKiemXeModalComponent, ViewDangKiemXeModalComponent,
        ThongTinSuaChuaComponent, CreateOrEditThongTinSuaChuaModalComponent, ViewThongTinSuaChuaModalComponent,
        ChiTietXeComponent, NhaCungCapComponent,TaiSanComponent, ModelComponent,
        ThongTinDangKiemComponent, CreateOrEditDangKiemXeModalComponent, ViewDangKiemXeModalComponent
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        TaiSanServiceProxy,
        ModelServiceProxy,
        NhaCungCapServiceProxy,
        ThongTinXeServiceProxy,
        QuanLyVanHanhServiceProxy,
        ThongTinBaoHiemServiceProxy,
        PhiDuongBoServiceProxy,
        ThongTinBaoDuongServiceProxy,
        ThongTinDangKiemServiceProxy,
        ThietBiKemTheoServiceProxy,
        ThongTinSuaChuaServiceProxy,
        CheckServiceProxy,
        ThietBiKemTheoServiceProxy

    ]
})
export class GWebsiteModule { }
