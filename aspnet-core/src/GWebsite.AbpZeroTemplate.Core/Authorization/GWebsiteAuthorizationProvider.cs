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

            var taiSan = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan, L("TaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Create, L("CreatingNewTaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Edit, L("EditingTaiSan"));
            taiSan.CreateChildPermission(GWebsitePermissions.Pages_Administration_TaiSan_Delete, L("DeletingTaiSan"));

            var capPhat = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_CapPhat, L("CapPhat"));
            capPhat.CreateChildPermission(GWebsitePermissions.Pages_Administration_CapPhat_Create, L("CreatingNewCapPhat"));
            capPhat.CreateChildPermission(GWebsitePermissions.Pages_Administration_CapPhat_Edit, L("EditingCapPhat"));
            capPhat.CreateChildPermission(GWebsitePermissions.Pages_Administration_CapPhat_Delete, L("DeletingCapPhat"));

            var dieuChuyen = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen, L("DieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Create, L("CreatingNewDieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Edit, L("EditingDieuChuyen"));
            dieuChuyen.CreateChildPermission(GWebsitePermissions.Pages_Administration_DieuChuyen_Delete, L("DeletingDieuChuyen"));

            var thuHoi = gwebsite.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi, L("ThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Create, L("CreatingNewThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Edit, L("EditingThuHoi"));
            thuHoi.CreateChildPermission(GWebsitePermissions.Pages_Administration_ThuHoi_Delete, L("DeletingThuHoi"));


        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
