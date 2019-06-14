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
import { NgxQRCodeModule } from 'ngx-qrcode2';

import { MenuClientComponent, CreateOrEditMenuClientModalComponent } from './index';
import { DemoModelComponent } from './demo-model/demo-model.component';
import { CreateOrEditDemoModelModalComponent } from './demo-model/create-or-edit-demo-model-modal.component';
import { DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';

// qlts
import { AssetGroupComponent } from './assetgroup/assetgroup.component';
import { ViewAssetGroupModalComponent } from './assetgroup/view-assetgroup-modal.component';
import { CreateOrEditAssetGroupModalComponent } from './assetgroup/create-or-edit-assetgroup-modal.component';

import { AssetComponent } from './asset/asset.component';
import { ViewAssetModalComponent } from './asset/view-asset-modal.component';
import { CreateOrEditAssetModalComponent } from './asset/create-or-edit-asset-modal.component';

import { CreateAssetComponent } from './asset/create-asset.component';

import { LiquidationComponent } from './liquidation/liquidation.component';
import { ViewLiquidationModalComponent } from './liquidation/view-liquidation-modal.component';
import { CreateOrEditLiquidationModalComponent } from './liquidation/create-or-edit-liquidation-modal.component';

// import { RepairComponent } from './repair/repair.component';
// import { ViewRepairModalComponent } from './repair/view-repair-modal.component';
// import { CreateOrEditRepairModalComponent } from './repair/create-or-edit-repair-modal.component';

import { RevokeComponent } from './revoke/revoke.component';
import { ViewRevokeModalComponent } from './revoke/view-revoke-modal.component';
import { CreateOrEditRevokeModalComponent } from './revoke/create-or-edit-revoke-modal.component';

// import { TransferComponent } from './transfer/transfer.component';
// import { ViewTransferModalComponent } from './transfer/view-transfer-modal.component';
// import { CreateOrEditTransferModalComponent } from './transfer/create-or-edit-transfer-modal.component';

import { UseAssetComponent } from './useasset/useasset.component';
import { ViewUseAssetModalComponent } from './useasset/view-useasset-modal.component';
import { CreateOrEditUseAssetModalComponent } from './useasset/create-or-edit-useasset-modal.component';

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
        NgxQRCodeModule
    ],
    declarations: [
        MenuClientComponent, CreateOrEditMenuClientModalComponent,
        DemoModelComponent, CreateOrEditDemoModelModalComponent, ViewDemoModelModalComponent,
        CustomerComponent, CreateOrEditCustomerModalComponent, ViewCustomerModalComponent,

        AssetGroupComponent, CreateOrEditAssetGroupModalComponent, ViewAssetGroupModalComponent,
        AssetComponent, CreateOrEditAssetModalComponent, ViewAssetModalComponent,
        CreateAssetComponent,
        LiquidationComponent, CreateOrEditLiquidationModalComponent, ViewLiquidationModalComponent,
        // RepairComponent, CreateOrEditRepairModalComponent, ViewRepairModalComponent,
        RevokeComponent, CreateOrEditRevokeModalComponent, ViewRevokeModalComponent,
        // TransferComponent, CreateOrEditTransferModalComponent, ViewTransferModalComponent,
        UseAssetComponent, CreateOrEditUseAssetModalComponent, ViewUseAssetModalComponent,
    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class GWebsiteModule { }
