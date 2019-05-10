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
import { CustomerComponent } from './customer/customer.component';
import { ViewCustomerModalComponent } from './customer/view-customer-modal.component';
import { CreateOrEditCustomerModalComponent } from './customer/create-or-edit-customer-modal.component';

// qlts
import { CategoryComponent } from './category/category.component';
import { ViewCategoryModalComponent } from './category/view-category-modal.component';
import { CreateOrEditCategoryModalComponent } from './category/create-or-edit-category-modal.component';

import { AssetComponent } from './asset/asset.component';
import { ViewAssetModalComponent } from './asset/view-asset-modal.component';
import { CreateOrEditAssetModalComponent } from './asset/create-or-edit-asset-modal.component';

import { AssetDetailComponent} from './assetdetail/assetdetail.component';
import { ViewAssetDetailModalComponent } from './assetdetail/view-assetdetail-modal.component';
import { CreateOrEditAssetDetailModalComponent } from './assetdetail/create-or-edit-assetdetail-modal.component';

import { ProviderComponent } from './provider/provider.component';
import { ViewProviderModalComponent } from './provider/view-provider-modal.component';
import { CreateOrEditProviderModalComponent } from './provider/create-or-edit-provider-modal.component';

import { LiquidationComponent } from './liquidation/liquidation.component';
import { ViewLiquidationModalComponent } from './liquidation/view-liquidation-modal.component';
import { CreateOrEditLiquidationModalComponent } from './liquidation/create-or-edit-liquidation-modal.component';

import { LiquidationDetailComponent } from './liquidationdetail/liquidationdetail.component';
import { ViewLiquidationDetailModalComponent } from './liquidationdetail/view-liquidationdetail-modal.component';
import { CreateOrEditLiquidationDetailModalComponent } from './liquidationdetail/create-or-edit-liquidationdetail-modal.component';
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

        CategoryComponent, CreateOrEditCategoryModalComponent, ViewCategoryModalComponent,
        AssetComponent, CreateOrEditAssetModalComponent, ViewAssetModalComponent,
        AssetDetailComponent, CreateOrEditAssetDetailModalComponent, ViewAssetDetailModalComponent,
        ProviderComponent, CreateOrEditProviderModalComponent, ViewProviderModalComponent,
        LiquidationComponent, CreateOrEditLiquidationModalComponent, ViewLiquidationModalComponent,
        LiquidationDetailComponent, CreateOrEditLiquidationDetailModalComponent, ViewLiquidationDetailModalComponent,
    ],
    providers: [
        DemoModelServiceProxy,
        CustomerServiceProxy
    ]
})
export class GWebsiteModule { }
