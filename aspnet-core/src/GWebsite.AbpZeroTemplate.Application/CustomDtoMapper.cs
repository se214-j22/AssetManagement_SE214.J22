using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetDetails.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LiquidationDetails.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Categories.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;

namespace GWebsite.AbpZeroTemplate.Applications
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MenuClient, MenuClientDto>();
            configuration.CreateMap<MenuClient, MenuClientListDto>();
            configuration.CreateMap<CreateMenuClientInput, MenuClient>();
            configuration.CreateMap<UpdateMenuClientInput, MenuClient>();

            // DemoModel
            configuration.CreateMap<DemoModel, DemoModelDto>();
            configuration.CreateMap<DemoModelInput, DemoModel>();
            configuration.CreateMap<DemoModel, DemoModelInput>();
            configuration.CreateMap<DemoModel, DemoModelForViewDto>();

            // Customer
            configuration.CreateMap<Customer, CustomerDto>();
            configuration.CreateMap<CustomerInput, Customer>();
            configuration.CreateMap<Customer, CustomerInput>();
            configuration.CreateMap<Customer, CustomerForViewDto>();

            // Category
            configuration.CreateMap<Category, CategoryDto>();
            configuration.CreateMap<CategoryInput, Category>();
            configuration.CreateMap<Category, CategoryInput>();
            configuration.CreateMap<Category, CategoryForViewDto>();

            // Asset
            configuration.CreateMap<Asset, AssetDto>();
            configuration.CreateMap<AssetInput, Asset>();
            configuration.CreateMap<Asset, AssetInput>();
            configuration.CreateMap<Asset, AssetForViewDto>();

            //Provider
            configuration.CreateMap<Provider, ProviderDto>();
            configuration.CreateMap<ProviderInput, Provider>();
            configuration.CreateMap<Provider, ProviderInput>();
            configuration.CreateMap<Provider, ProviderForViewDto>();

            //AssetDetail
            configuration.CreateMap<AssetDetail, AssetDetailDto>();
            configuration.CreateMap<AssetDetailInput, AssetDetail>();
            configuration.CreateMap<AssetDetail, AssetDetailInput>();
            configuration.CreateMap<AssetDetail, AssetDetailForViewDto>();

            //Liquidation
            configuration.CreateMap<Liquidation, LiquidationDto>();
            configuration.CreateMap<LiquidationInput, Liquidation>();
            configuration.CreateMap<Liquidation, LiquidationInput>();
            configuration.CreateMap<Liquidation, LiquidationForViewDto>();

            //LiquidationDetail
            configuration.CreateMap<LiquidationDetail, LiquidationDetailDto>();
            configuration.CreateMap<LiquidationDetailInput, LiquidationDetail>();
            configuration.CreateMap<LiquidationDetail, LiquidationDetailInput>();
            configuration.CreateMap<LiquidationDetail, LiquidationDetailForViewDto>();
        }
    }
}