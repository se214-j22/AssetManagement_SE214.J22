using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoHiems.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinDangKiems.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhiDuongBos;

using GWebsite.AbpZeroTemplate.Application.Share.Models.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.NhaCungCap.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinXes.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinSuaChuas.DTO;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos.Dto;

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


            // Quan ly van hanh
            configuration.CreateMap<QuanLyVanHanh, QuanLyVanHanhDto>();
            configuration.CreateMap<QuanLyVanHanhInput, QuanLyVanHanh>();
            configuration.CreateMap<QuanLyVanHanh, QuanLyVanHanhInput>();
            configuration.CreateMap<QuanLyVanHanh, QuanLyVanHanhForViewDto>();

            // Thong tin bao duong 
            configuration.CreateMap<ThongTinBaoDuong, ThongTinBaoDuongDto>();
            configuration.CreateMap<ThongTinBaoDuongInput, ThongTinBaoDuong>();
            configuration.CreateMap<ThongTinBaoDuong, ThongTinBaoDuongInput>();
            configuration.CreateMap<ThongTinBaoDuong, ThongTinBaoDuongForViewDto>();

            // thông tin đăng kiểm
            configuration.CreateMap<ThongTinDangKiem, ThongTinDangKiemDto>();
            configuration.CreateMap<ThongTinDangKiemInput, ThongTinDangKiem>();
            configuration.CreateMap<ThongTinDangKiem, ThongTinDangKiemInput>();
            configuration.CreateMap<ThongTinDangKiem, ThongTinDangKiemForViewDto>();
            // thông tin bảo hiểm
            configuration.CreateMap<ThongTinBaoHiem, ThongTinBaoHiemDto>();
            configuration.CreateMap<ThongTinBaoHiemInput, ThongTinBaoHiem>();
            configuration.CreateMap<ThongTinBaoHiem, ThongTinBaoHiemInput>();
            configuration.CreateMap<ThongTinBaoHiem, ThongTinBaoHiemForViewDto>();

            // PhiDuongBo
            configuration.CreateMap<PhiDuongBo, PhiDuongBoDTO>();
            configuration.CreateMap<PhiDuongBoInput, PhiDuongBo>();
            configuration.CreateMap<PhiDuongBo, PhiDuongBoInput>();
            configuration.CreateMap<PhiDuongBo, PhiDuongBoForViewDTO>();
            // Model
            configuration.CreateMap<Model, ModelDto>();
            configuration.CreateMap<ModelInput, Model>();
            configuration.CreateMap<Model, ModelInput>();
            configuration.CreateMap<Model, ModelForViewDto>();

            // NhaCungCap
            configuration.CreateMap<NhaCungCap, NhaCungCapDto>();
            configuration.CreateMap<NhaCungCapInput, NhaCungCap>();
            configuration.CreateMap<NhaCungCap, NhaCungCapInput>();
            configuration.CreateMap<NhaCungCap, NhaCungCapForViewDto>();

            //taisan
            configuration.CreateMap<TaiSan, TaiSanDto>();
            configuration.CreateMap<TaiSanInput, TaiSan>();
            configuration.CreateMap<TaiSan, TaiSanInput>();
            configuration.CreateMap<TaiSan, TaiSanForViewDto>();

            //thongtinxe
            configuration.CreateMap<ThongTinXe, ThongTinXeDto>();
            configuration.CreateMap<ThongTinXeInput, ThongTinXe>();
            configuration.CreateMap<ThongTinXe, ThongTinXeInput>();
            configuration.CreateMap<ThongTinXe, ThongTinXeForViewDto>();

            //thongtinSuchua
            configuration.CreateMap<ThongTinSuaChua, ThongTinSuaChuaDTO>();
            configuration.CreateMap<ThongTinSuaChuaInput, ThongTinSuaChua>();
            configuration.CreateMap<ThongTinSuaChua, ThongTinSuaChuaInput>();
            configuration.CreateMap<ThongTinSuaChua, ThongTinSuaChuaForViewDTO>();

            //ThietBiKemTheo
            configuration.CreateMap<ThietBiKemTheo, ThietBiKemTheoDto>();
            configuration.CreateMap<ThietBiKemTheoInput, ThietBiKemTheo>();
            configuration.CreateMap<ThietBiKemTheo, ThietBiKemTheoInput>();
            configuration.CreateMap<ThietBiKemTheo, ThietBiKemTheoForViewDto>();
        }
    }
}