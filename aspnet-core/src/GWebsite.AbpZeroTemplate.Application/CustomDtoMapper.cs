using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.ChiNhanhs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTDonVis.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DonVis.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhanViens.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SuaChuas.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.XuatTaiSans.Dto;

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

            // TaiSan
            configuration.CreateMap<TaiSan, TaiSanDto>();
            configuration.CreateMap<TaiSanInput, TaiSan>();
            configuration.CreateMap<TaiSan, TaiSanInput>();
            configuration.CreateMap<TaiSan, TaiSanForViewDto>();

            //Thong Tin TaiSan
            configuration.CreateMap<ThongTinTaiSan, TaiSanDto>();
            configuration.CreateMap<TaiSanInput, ThongTinTaiSan>();
            configuration.CreateMap<ThongTinTaiSan, TaiSanInput>();
            configuration.CreateMap<ThongTinTaiSan, TaiSanForViewDto>();

            // NhomTaiSan
            configuration.CreateMap<NhomTaiSan, NhomTaiSanDto>();
            configuration.CreateMap<NhomTaiSanInput, NhomTaiSan>();
            configuration.CreateMap<NhomTaiSan, NhomTaiSanInput>();
            configuration.CreateMap<NhomTaiSan, NhomTaiSanForViewDto>();

            // DonVi
            configuration.CreateMap<DonVi, DonViDto>();
            configuration.CreateMap<DonViInput, DonVi>();
            configuration.CreateMap<DonVi, DonViInput>();
            configuration.CreateMap<DonVi, DonViForViewDto>();

            // LoTaiSan
            configuration.CreateMap<LoTaiSan, LoTaiSanDto>();
            configuration.CreateMap<LoTaiSanInput, LoTaiSan>();
            configuration.CreateMap<LoTaiSan, LoTaiSanInput>();
            configuration.CreateMap<LoTaiSan, LoTaiSanForViewDto>();

            // TaiSan
            configuration.CreateMap<TS, TaiSanDto>();
            configuration.CreateMap<TaiSanInput, TS>();
            configuration.CreateMap<TS, TaiSanInput>();
            configuration.CreateMap<TS, TaiSanForViewDto>();

            // CTTaiSan
            configuration.CreateMap<CTTaiSan, CTTaiSanDto>();
            configuration.CreateMap<CTTaiSanInput, CTTaiSan>();
            configuration.CreateMap<CTTaiSan, CTTaiSanInput>();
            configuration.CreateMap<CTTaiSan, CTTaiSanForViewDto>();

            // XuatTaiSan
            configuration.CreateMap<XuatTaiSan, XuatTaiSanDto>();
            configuration.CreateMap<XuatTaiSanInput, XuatTaiSan>();
            configuration.CreateMap<XuatTaiSan, XuatTaiSanInput>();
            configuration.CreateMap<XuatTaiSan, XuatTaiSanForViewDto>();

            // DieuChuyen
            configuration.CreateMap<DieuChuyen, DieuChuyenDto>();
            configuration.CreateMap<DieuChuyenInput, DieuChuyen>();
            configuration.CreateMap<DieuChuyen, DieuChuyenInput>();
            configuration.CreateMap<DieuChuyen, DieuChuyenForViewDto>();

            // NhanVien
            configuration.CreateMap<NhanVien, NhanVienDto>();
            configuration.CreateMap<NhanVienInput, NhanVien>();
            configuration.CreateMap<NhanVien, NhanVienInput>();
            configuration.CreateMap<NhanVien, NhanVienForViewDto>();


            // ThuHoi
            configuration.CreateMap<ThuHoi, ThuHoiDto>();
            configuration.CreateMap<ThuHoiInput, ThuHoi>();
            configuration.CreateMap<ThuHoi, ThuHoiInput>();
            configuration.CreateMap<ThuHoi, ThuHoiForViewDto>();

            // SuaChua
            configuration.CreateMap<SuaChua, SuaChuaDto>();
            configuration.CreateMap<SuaChuaInput, SuaChua>();
            configuration.CreateMap<SuaChua, SuaChuaInput>();
            configuration.CreateMap<SuaChua, SuaChuaForViewDto>();

            // ChiNhanh
            configuration.CreateMap<ChiNhanh, ChiNhanhDto>();
            configuration.CreateMap<ChiNhanhInput, ChiNhanh>();
            configuration.CreateMap<ChiNhanh, ChiNhanhInput>();
            configuration.CreateMap<ChiNhanh, ChiNhanhForViewDto>();

            // CTDonVi
            configuration.CreateMap<CTDonVi, CTDonViDto>();
            configuration.CreateMap<CTDonViInput, CTDonVi>();
            configuration.CreateMap<CTDonVi, CTDonViInput>();
            configuration.CreateMap<CTDonVi, CTDonViForViewDto>();
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