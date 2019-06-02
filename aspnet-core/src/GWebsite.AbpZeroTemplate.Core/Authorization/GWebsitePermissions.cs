namespace GWebsite.AbpZeroTemplate.Core.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="GWebsiteAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class GWebsitePermissions
    {
        public const string Pages = "Pages";
        public const string Pages_Administration_GWebsite = "Pages.Administration.GWebsite";

        public const string Pages_Administration_MenuClient = "Pages.Administration.MenuClient";
        public const string Pages_Administration_MenuClient_Create = "Pages.Administration.MenuClient.Create";
        public const string Pages_Administration_MenuClient_Edit = "Pages.Administration.MenuClient.Edit";
        public const string Pages_Administration_MenuClient_Delete = "Pages.Administration.MenuClient.Delete";

        public const string Pages_Administration_DemoModel = "Pages.Administration.DemoModel";
        public const string Pages_Administration_DemoModel_Create = "Pages.Administration.DemoModel.Create";
        public const string Pages_Administration_DemoModel_Edit = "Pages.Administration.DemoModel.Edit";
        public const string Pages_Administration_DemoModel_Delete = "Pages.Administration.DemoModel.Delete";

        public const string Pages_Administration_Customer = "Pages.Administration.Customer";
        public const string Pages_Administration_Customer_Create = "Pages.Administration.Customer.Create";
        public const string Pages_Administration_Customer_Edit = "Pages.Administration.Customer.Edit";
        public const string Pages_Administration_Customer_Delete = "Pages.Administration.Customer.Delete";

        #region Permissions related to Asset
        public const string Pages_Administration_Asset = "Pages.Administration.Asset";
        public const string Pages_Administration_Asset_Create_Edit = "Pages.Administration.Asset.Create_Edit";
        public const string Pages_Administration_Asset_Delete = "Pages.Administration.Asset.Delete";

        public const string Pages_Administration_AssetType = "Pages.Administration.AssetType";
        public const string Pages_Administration_AssetType_Create_Edit = "Pages.Administration.AssetType.Create_Edit";
        public const string Pages_Administration_AssetType_Delete = "Pages.Administration.AssetType.Delete";

        public const string Pages_Administration_AssetLine = "Pages.Administration.AssetLine";
        public const string Pages_Administration_AssetLine_Create_Edit = "Pages.Administration.AssetLine.Create_Edit";
        public const string Pages_Administration_AssetLine_Delete = "Pages.Administration.AssetLine.Delete";

        public const string Pages_Administration_Manufacturer = "Pages.Administration.Manufacturer";
        public const string Pages_Administration_Manufacturer_Create_Edit = "Pages.Administration.Manufacturer.Create_Edit";
        public const string Pages_Administration_Manufacturer_Delete = "Pages.Administration.Manufacturer.Delete";
        #endregion
    }
}