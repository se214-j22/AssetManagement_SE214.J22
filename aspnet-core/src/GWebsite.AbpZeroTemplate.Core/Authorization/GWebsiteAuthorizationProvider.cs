using Abp.Authorization;
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
