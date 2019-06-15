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

            var SupplierCatalog = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_SupplierCatalog, L("SupplierCatalog"));
            SupplierCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_SupplierCatalog_Create, L("CreatingNewSupplierCatalog"));
            SupplierCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_SupplierCatalog_Edit, L("EditingSupplierCatalog"));
            SupplierCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_SupplierCatalog_Delete, L("DeletingSupplierCatalog"));


            var ProductCatalog = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_ProductCatalog, L("ProductCatalog"));
            ProductCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_ProductCatalog_Create, L("CreatingNewProductCatalog"));
            ProductCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_ProductCatalog_Edit, L("EditingProductCatalog"));
            ProductCatalog.CreateChildPermission(GWebsitePermissions.Pages_Administration_ProductCatalog_Delete, L("DeletingProductCatalog"));


            var Plan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Plan, L("Plan"));
            Plan.CreateChildPermission(GWebsitePermissions.Pages_Administration_Plan_Create, L("CreatingNewPlan"));
            Plan.CreateChildPermission(GWebsitePermissions.Pages_Administration_Plan_Edit, L("EditingPlan"));
            Plan.CreateChildPermission(GWebsitePermissions.Pages_Administration_Plan_Delete, L("DeletingPlan"));
            Plan.CreateChildPermission(GWebsitePermissions.Pages_Administration_Approved, L("Approved"));
            Plan.CreateChildPermission(GWebsitePermissions.Pages_Administration_UnApproved, L("UnApproved"));

            var SubPlan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_SubPlan, L("SubPlan"));
            SubPlan.CreateChildPermission(GWebsitePermissions.Pages_Administration_SubPlan_Create, L("CreatingNewSubPlan"));
            SubPlan.CreateChildPermission(GWebsitePermissions.Pages_Administration_SubPlan_Edit, L("EditingSubPlan"));
            SubPlan.CreateChildPermission(GWebsitePermissions.Pages_Administration_SubPlan_Delete, L("DeletingSubPlan"));

            var BidProfile = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_BidProfile, L("BidProfile"));
            BidProfile.CreateChildPermission(GWebsitePermissions.Pages_Administration_BidProfile_Create, L("CreatingNewBidProfile"));
            BidProfile.CreateChildPermission(GWebsitePermissions.Pages_Administration_BidProfile_Edit, L("EditingBidProfile"));
            BidProfile.CreateChildPermission(GWebsitePermissions.Pages_Administration_BidProfile_Delete, L("DeletingBidProfile"));



            var Project = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_Project, L("Project"));
            Project.CreateChildPermission(GWebsitePermissions.Pages_Administration_Project_Create, L("CreatingNewProject"));
            Project.CreateChildPermission(GWebsitePermissions.Pages_Administration_Project_Edit, L("EditingProject"));
            Project.CreateChildPermission(GWebsitePermissions.Pages_Administration_Project_Delete, L("DeletingProject"));

            //var abcxyz = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_abcxyz, L("abcxyz"));
            //abcxyz.CreateChildPermission(GWebsitePermissions.Pages_Administration_abcxyz_Create, L("CreatingNewabcxyz"));
            //abcxyz.CreateChildPermission(GWebsitePermissions.Pages_Administration_abcxyz_Edit, L("Editingabcxyz"));
            //abcxyz.CreateChildPermission(GWebsitePermissions.Pages_Administration_abcxyz_Delete, L("Deletingabcxyz"));

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
