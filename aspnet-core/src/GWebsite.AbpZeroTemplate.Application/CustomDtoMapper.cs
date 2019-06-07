using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Speedsters.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using GWebsite.AbpZeroTemplate.Application.Share.DonViCungCapTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhongBan.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.HoaDonNhaps.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BangYeuCauCungCapTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoaiTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSanCoDinhs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys.Dto;

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

			// Speedster
			configuration.CreateMap<Speedster, SpeedsterDto>();
			configuration.CreateMap<SpeedsterInput, Speedster>();
			configuration.CreateMap<Speedster, SpeedsterInput>();
			configuration.CreateMap<Speedster, SpeedsterForViewDto>();

			// DonViCungCapTaiSan
			configuration.CreateMap<DonViCungCapTaiSan, DonViCungCapTaiSanDto>();
			configuration.CreateMap<DonViCungCapTaiSanInput, DonViCungCapTaiSan>();
			configuration.CreateMap<DonViCungCapTaiSan, DonViCungCapTaiSanInput>();
			configuration.CreateMap<DonViCungCapTaiSan, DonViCungCapTaiSanForViewDto>();

			// PhongBan
			configuration.CreateMap<PhongBan, PhongBanDto>();
			configuration.CreateMap<PhongBanInput, PhongBan>();
			configuration.CreateMap<PhongBan, PhongBanInput>();
			configuration.CreateMap<PhongBan, PhongBanForViewDto>();


			// HoaDonNhap
			configuration.CreateMap<HoaDonNhap, HoaDonNhapDto>();
			configuration.CreateMap<HoaDonNhapInput, HoaDonNhap>();
			configuration.CreateMap<HoaDonNhap, HoaDonNhapInput>();
			configuration.CreateMap<HoaDonNhap, HoaDonNhapForViewDto>();

			// BangYeuCauCungCapTaiSan
			configuration.CreateMap<BangYeuCauCungCapTaiSan, BangYeuCauCungCapTaiSanDto>();
			configuration.CreateMap<BangYeuCauCungCapTaiSanInput, BangYeuCauCungCapTaiSan>();
			configuration.CreateMap<BangYeuCauCungCapTaiSan, BangYeuCauCungCapTaiSanInput>();
			configuration.CreateMap<BangYeuCauCungCapTaiSan, BangYeuCauCungCapTaiSanForViewDto>();

			// LoaiTaiSan
			configuration.CreateMap<LoaiTaiSan, LoaiTaiSanDto>();
			configuration.CreateMap<LoaiTaiSanInput, LoaiTaiSan>();
			configuration.CreateMap<LoaiTaiSan, LoaiTaiSanInput>();
			configuration.CreateMap<LoaiTaiSan, LoaiTaiSanForViewDto>();

            // TaiSaiCoDinh
            configuration.CreateMap<TaiSanCoDinh, TaiSanCoDinhDto>();
            configuration.CreateMap<TaiSanCoDinhInput, TaiSanCoDinh>();
            configuration.CreateMap<TaiSanCoDinh, TaiSanCoDinhInput>();
			//configuration.CreateMap<TaiSanCoDinh, TaiSanCoDinhForViewDto>();

			// PhieuBaoDuong
			configuration.CreateMap<PhieuBaoDuong, PhieuBaoDuongDto>();
			configuration.CreateMap<PhieuBaoDuongInput, PhieuBaoDuong>();
			configuration.CreateMap<PhieuBaoDuong, PhieuBaoDuongInput>();
			//configuration.CreateMap<PhieuBaoDuong, PhieuBaoDuongForViewDto>();

			// BienBanThanhLy
			configuration.CreateMap<BienBanThanhLy, BienBanThanhLyDto>();
			configuration.CreateMap<BienBanThanhLyInput, BienBanThanhLy>();
			configuration.CreateMap<BienBanThanhLy, BienBanThanhLyInput>();
			//configuration.CreateMap<BienBanThanhLy, BienBanThanhLyForViewDto>();

		}
	}
}