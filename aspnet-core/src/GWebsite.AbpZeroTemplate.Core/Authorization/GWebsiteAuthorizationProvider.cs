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

            var provider = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Provider, L("Provider"));
            provider.CreateChildPermission(GWebsitePermissions.Pages_Administration_Provider_Create, L("CreatingNewProvider"));
            provider.CreateChildPermission(GWebsitePermissions.Pages_Administration_Provider_Edit, L("EditingProvider"));
            provider.CreateChildPermission(GWebsitePermissions.Pages_Administration_Provider_Delete, L("DeletingProvider"));

            var assetdetail = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetDetail, L("AssetDetail"));
            assetdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetDetail_Create, L("CreatingNewAssetDetail"));
            assetdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetDetail_Edit, L("EditingAssetDetail"));
            assetdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_AssetDetail_Delete, L("DeletingAssetDetail"));

            var liquidation = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation, L("Liquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Create, L("CreatingNewLiquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Edit, L("EditingLiquidation"));
            liquidation.CreateChildPermission(GWebsitePermissions.Pages_Administration_Liquidation_Delete, L("DeletingLiquidation"));

            var liquidationdetail = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_LiquidationDetail, L("LiquidationDetail"));
            liquidationdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_LiquidationDetail_Create, L("CreatingNewLiquidationDetail"));
            liquidationdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_LiquidationDetail_Edit, L("EditingLiquidationDetail"));
            liquidationdetail.CreateChildPermission(GWebsitePermissions.Pages_Administration_LiquidationDetail_Delete, L("DeletingLiquidationDetail"));

            var category = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Category, L("Category"));
            category.CreateChildPermission(GWebsitePermissions.Pages_Administration_Category_Create, L("CreatingNewCategory"));
            category.CreateChildPermission(GWebsitePermissions.Pages_Administration_Category_Edit, L("EditingCategory"));
            category.CreateChildPermission(GWebsitePermissions.Pages_Administration_Category_Delete, L("DeletingCategory"));

            var asset = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset, L("Asset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Create, L("CreatingNewAsset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Edit, L("EditingAsset"));
            asset.CreateChildPermission(GWebsitePermissions.Pages_Administration_Asset_Delete, L("DeletingAsset"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
