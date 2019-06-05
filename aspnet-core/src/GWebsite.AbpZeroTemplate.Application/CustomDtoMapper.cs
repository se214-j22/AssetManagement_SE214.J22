using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;

//
using GWebsite.AbpZeroTemplate.Application.Share.Assets.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.AssetGroups.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Liquidations.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Repairs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Revokes.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Transfers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.UseAssets.Dto;
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

            /// <summary>
            /// qlts
            /// </summary>

            // Asset
            configuration.CreateMap<Asset, AssetDto>();
            configuration.CreateMap<AssetInput, Asset>();
            configuration.CreateMap<Asset, AssetInput>();
            configuration.CreateMap<Asset, AssetForViewDto>();

            //AssetGroup
            configuration.CreateMap<AssetGroup, AssetGroupDto>();
            configuration.CreateMap<AssetGroupInput, AssetGroup>();
            configuration.CreateMap<AssetGroup, AssetGroupInput>();
            configuration.CreateMap<AssetGroup, AssetGroupForViewDto>();

            //Liquidation
            configuration.CreateMap<Liquidation, LiquidationDto>();
            configuration.CreateMap<LiquidationInput, Liquidation>();
            configuration.CreateMap<Liquidation, LiquidationInput>();
            configuration.CreateMap<Liquidation, LiquidationForViewDto>();

            //Repair
            configuration.CreateMap<Repair, RepairDto>();
            configuration.CreateMap<RepairInput, Repair>();
            configuration.CreateMap<Repair, RepairInput>();
            configuration.CreateMap<Repair, RepairForViewDto>();

            //Revoke
            configuration.CreateMap<Revoke, RevokeDto>();
            configuration.CreateMap<RevokeInput, Revoke>();
            configuration.CreateMap<Revoke, RevokeInput>();
            configuration.CreateMap<Revoke, RevokeForViewDto>();

            //Transfer
            configuration.CreateMap<Transfer, TransferDto>();
            configuration.CreateMap<TransferInput, Transfer>();
            configuration.CreateMap<Transfer, TransferInput>();
            configuration.CreateMap<Transfer, TransferForViewDto>();

            //UseAsset
            configuration.CreateMap<UseAsset, UseAssetDto>();
            configuration.CreateMap<UseAssetInput, UseAsset>();
            configuration.CreateMap<UseAsset, UseAssetInput>();
            configuration.CreateMap<UseAsset, UseAssetForViewDto>();
        }
    }
}