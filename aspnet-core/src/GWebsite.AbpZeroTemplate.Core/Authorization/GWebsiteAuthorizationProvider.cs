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

            var menuClients = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_OrderPackage, L("MenuClient"));
            menuClients.CreateChildPermission(GWebsitePermissions.Pages_Administration_OrderPackage_Create, L("CreatingNewMenuClient"));
            menuClients.CreateChildPermission(GWebsitePermissions.Pages_Administration_OrderPackage_Edit, L("EditingMenuClient"));
            menuClients.CreateChildPermission(GWebsitePermissions.Pages_Administration_OrderPackage_Delete, L("DeletingMenuClient"));

            var purchase = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Purchase, L("Purchase"));
            purchase.CreateChildPermission(GWebsitePermissions.Pages_Administration_Purchase_Create, L("CreatingNewPurchase"));
            purchase.CreateChildPermission(GWebsitePermissions.Pages_Administration_Purchase_Edit, L("EditingPurchase"));
            purchase.CreateChildPermission(GWebsitePermissions.Pages_Administration_Purchase_Delete, L("DeletingPurchase"));

            var orderPackages = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient, L("OrderPackage"));
            orderPackages.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Create, L("CreatingNewOrderPackage"));
            orderPackages.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Edit, L("EditingOrderPackage"));
            orderPackages.CreateChildPermission(GWebsitePermissions.Pages_Administration_MenuClient_Delete, L("DeletingOrderPackage"));

            var videoInstructions = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstruction, L("VideoInstruction"));
            videoInstructions.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstruction_Create, L("CreatingNewVideoInstruction"));
            videoInstructions.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstruction_Edit, L("EditingVideoInstruction"));
            videoInstructions.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstruction_Delete, L("DeletingVideoInstruction"));

            var VideoInstructionCategories = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstructionCategory, L("VideoInstructionCategory"));
            VideoInstructionCategories.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstructionCategory_Create, L("CreatingNewVVideoInstructionCategory"));
            VideoInstructionCategories.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstructionCategory_Edit, L("EditingVideoInstructionCategory"));
            VideoInstructionCategories.CreateChildPermission(GWebsitePermissions.Pages_Administration_VideoInstructionCategory_Delete, L("DeletingVideoInstructionCategory"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
