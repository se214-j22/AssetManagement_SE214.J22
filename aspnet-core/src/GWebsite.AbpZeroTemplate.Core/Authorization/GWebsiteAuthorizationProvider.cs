﻿using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace GWebsite.AbpZeroTemplate.Core.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class GWebsiteAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public GWebsiteAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public GWebsiteAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(GWebsitePermissions.Pages) ?? context.CreatePermission(GWebsitePermissions.Pages, L("Pages"));
            var gwebsite = pages.CreateChildPermission(GWebsitePermissions.Pages_Administration_GWebsite, L("GWebsite"));

            var menuClient = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient, L("MenuClient"));
            menuClient.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Create, L("CreatingNewMenuClient"));
            menuClient.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Edit, L("EditingMenuClient"));
            menuClient.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Delete, L("DeletingMenuClient"));

            var demoModel = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_DemoModel, L("DemoModel"));
            demoModel.CreateChildPermission(GWebsitePermissions.Pages_Administration_DemoModel_Create, L("CreatingNewDemoModel"));
            demoModel.CreateChildPermission(GWebsitePermissions.Pages_Administration_DemoModel_Edit, L("EditingDemoModel"));
            demoModel.CreateChildPermission(GWebsitePermissions.Pages_Administration_DemoModel_Delete, L("DeletingDemoModel"));

            var customer = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Customer, L("Customer"));
            customer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Customer_Create, L("CreatingNewCustomer"));
            customer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Customer_Edit, L("EditingCustomer"));
            customer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Customer_Delete, L("DeletingCustomer"));

            var taiSan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan, L("TaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Create, L("CreatingNewTaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Edit, L("EditingTaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Delete, L("DeletingTaiSan"));

            var ctts = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTTaiSan, L("CTTaiSan"));
            ctts.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTTaiSan_Create, L("CreatingNewCTTaiSan"));
            ctts.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTTaiSan_Edit, L("EditingCTTaiSan"));
            ctts.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTTaiSan_Delete, L("DeletingCTTaiSan"));

            var NhomTaiSan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhomTaiSan, L("NhomTaiSan"));
            NhomTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhomTaiSan_Create, L("CreatingNewNhomTaiSan"));
            NhomTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhomTaiSan_Edit, L("EditingNhomTaiSan"));
            NhomTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhomTaiSan_Delete, L("DeletingNhomTaiSan"));

            var donVi = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_DonVi, L("DonVi"));
            donVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_DonVi_Create, L("CreatingNewDonVi"));
            donVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_DonVi_Edit, L("EditingDonVi"));
            donVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_DonVi_Delete, L("DeletingDonVi"));


            var loTaiSan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_LoTaiSan, L("LoTaiSan"));
            loTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_LoTaiSan_Create, L("CreatingNewLoTaiSan"));
            loTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_LoTaiSan_Edit, L("EditingLoTaiSan"));
            loTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_LoTaiSan_Delete, L("DeletingLoTaiSan"));

            var xuatTaiSan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_XuatTaiSan, L("XuatTaiSan"));
            xuatTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_XuatTaiSan_Create, L("CreatingNewXuatTaiSan"));
            xuatTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_XuatTaiSan_Edit, L("EditingXuatTaiSan"));
            xuatTaiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_XuatTaiSan_Delete, L("DeletingXuatTaiSan"));

            var dieuChuyen = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen, L("DieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Create, L("CreatingNewDieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Edit, L("EditingDieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Delete, L("DeletingDieuChuyen"));



            var nhanVien = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhanVien, L("NhanVien"));
            nhanVien.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhanVien_Create, L("CreatingNewNhanVien"));
            nhanVien.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhanVien_Edit, L("EditingNhanVien"));
            nhanVien.CreateChildPermission(GWebsitePermissions.Pages_Administration_NhanVien_Delete, L("DeletingNhanVien"));


            var thuHoi = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi, L("ThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Create, L("CreatingNewThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Edit, L("EditingThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Delete, L("DeletingThuHoi"));

            var suaChua = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_SuaChua, L("SuaChua"));
            suaChua.CreateChildPermission(GWebsitePermissions.Pages_Administration_SuaChua_Create, L("CreatingNewSuaChua"));
            suaChua.CreateChildPermission(GWebsitePermissions.Pages_Administration_SuaChua_Edit, L("EditingSuaChua"));
            suaChua.CreateChildPermission(GWebsitePermissions.Pages_Administration_SuaChua_Delete, L("DeletingSuaChua"));

            var chiNhanh = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_ChiNhanh, L("ChiNhanh"));
            chiNhanh.CreateChildPermission(GWebsitePermissions.Pages_Administration_ChiNhanh_Create, L("CreatingNewChiNhanh"));
            chiNhanh.CreateChildPermission(GWebsitePermissions.Pages_Administration_ChiNhanh_Edit, L("EditingChiNhanh"));
            chiNhanh.CreateChildPermission(GWebsitePermissions.Pages_Administration_ChiNhanh_Delete, L("DeletingChiNhanh"));

            var ctDonVi = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTDonVi, L("CTDonVi"));
            ctDonVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTDonVi_Create, L("CreatingNewCTDonVi"));
            ctDonVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTDonVi_Edit, L("EditingCTDonVi"));
            ctDonVi.CreateChildPermission(GWebsitePermissions.Pages_Administration_CTDonVi_Delete, L("DeletingCTDonVi"));

            /// <summary>
            /// Entity Nhóm tài sản
            /// </summary>
            /// 

            var asset = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset, L("Asset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Create, L("CreatingNewAsset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Edit, L("EditingAsset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Delete, L("DeletingAsset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Approve, L("ApproveAsset"));

            var assetGroup = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetGroup, L("AssetGroup"));
            assetGroup.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetGroup_Create, L("CreatingNewAssetGroup"));
            assetGroup.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetGroup_Edit, L("EditingAssetGroup"));
            assetGroup.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetGroup_Delete, L("DeletingAssetGroup"));

            var liquidation = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation, L("Liquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Create, L("CreatingNewLiquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Edit, L("EditingLiquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Delete, L("DeletingLiquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Approve, L("ApproveLiquidation"));

            var repair = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Repair, L("Repair"));
            repair.CreateChildPermission(GWebsitePermissions.Pages_Administration_Repair_Create, L("CreatingNewRepair"));
            repair.CreateChildPermission(GWebsitePermissions.Pages_Administration_Repair_Edit, L("EditingRepair"));
            repair.CreateChildPermission(GWebsitePermissions.Pages_Administration_Repair_Delete, L("DeletingRepair"));
            repair.CreateChildPermission(GWebsitePermissions.Pages_Administration_Repair_Approve, L("ApproveRepair"));

            var revoke = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Revoke, L("Revoke"));
            revoke.CreateChildPermission(GWebsitePermissions.Pages_Administration_Revoke_Create, L("CreatingNewRevoke"));
            revoke.CreateChildPermission(GWebsitePermissions.Pages_Administration_Revoke_Edit, L("EditingRevoke"));
            revoke.CreateChildPermission(GWebsitePermissions.Pages_Administration_Revoke_Delete, L("DeletingRevoke"));
            revoke.CreateChildPermission(GWebsitePermissions.Pages_Administration_Revoke_Approve, L("ApproveRevoke"));

            var transfer = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Transfer, L("Transfer"));
            transfer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Transfer_Create, L("CreatingNewTransfer"));
            transfer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Transfer_Edit, L("EditingTransfer"));
            transfer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Transfer_Delete, L("DeletingTransfer"));
            transfer.CreateChildPermission(GWebsitePermissions.Pages_Administration_Transfer_Approve, L("ApproveTransfer"));

            var useAsset = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_UseAsset, L("UseAsset"));
            useAsset.CreateChildPermission(GWebsitePermissions.Pages_Administration_UseAsset_Create, L("CreatingNewUseAsset"));
            useAsset.CreateChildPermission(GWebsitePermissions.Pages_Administration_UseAsset_Edit, L("EditingUseAsset"));
            useAsset.CreateChildPermission(GWebsitePermissions.Pages_Administration_UseAsset_Delete, L("DeletingUseAsset"));
            useAsset.CreateChildPermission(GWebsitePermissions.Pages_Administration_UseAsset_Approve, L("ApproveUseAsset"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
