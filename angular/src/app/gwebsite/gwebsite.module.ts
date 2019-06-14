import { CustomerServiceProxy, AssetServiceProxy, AssetLineServiceProxy, ManufacturerServiceProxy, AssetTypeServiceProxy } from './../../shared/service-proxies/service-proxies';
import { ViewDemoModelModalComponent } from './demo-model/view-demo-model-modal.component';
import { NgModule } from '@angular/core';
import { QRCodeModule } from 'angular2-qrcode';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { AssetComponentGroup1 } from './asset-group1/asset.component';
import { ViewAssetModalComponentGroup1 } from './asset-group1/view-asset-modal.component';
import { CreateOrEditAssetModalComponentGroup1 } from './asset-group1/create-or-edit-asset-modal.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { AssetLineComponent } from './asset-line/asset-line.component';
import { ViewAssetLineModalComponent } from './asset-line/view-asset-line-modal.component';
import { CreateOrEditAssetLineModalComponent } from './asset-line/create-or-edit-asset-line-modal.component';
import { CreateOrEditAssetTypeModalComponent } from './asset-type/create-or-edit-asset-type-modal.component';
import { ViewAssetTypeModalComponent } from './asset-type/view-asset-type-modal.component';
import { AssetTypeComponent } from './asset-type/asset-type.component';
import { CreateOrEditManufacturerModalComponent } from './manufacturer/create-or-edit-manufacturer-modal.component';
import { ViewManufacturerModalComponent } from './manufacturer/view-manufacturer-modal.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        NgSelectModule,
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
        QRCodeModule
    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        DemoModelComponent, CreateOrEditDemoModelModalComponent, ViewDemoModelModalComponent,
        CustomerComponent, CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,
        AssetComponentGroup1, ViewAssetModalComponentGroup1,  CreateOrEditAssetModalComponentGroup1,
        AssetLineComponent, ViewAssetLineModalComponent,  CreateOrEditAssetLineModalComponent,
        AssetTypeComponent, ViewAssetTypeModalComponent,  CreateOrEditAssetTypeModalComponent,
        ManufacturerComponent, ViewManufacturerModalComponent,  CreateOrEditManufacturerModalComponent,
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy,
        AssetServiceProxy,
        AssetLineServiceProxy,
        AssetTypeServiceProxy,
        ManufacturerServiceProxy
    ]
})
export class GWebsiteModule { }
