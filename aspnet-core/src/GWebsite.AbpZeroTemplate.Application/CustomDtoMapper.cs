using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.DieuChuyens.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;

using GWebsite.AbpZeroTemplate.Core.Models;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto;

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

            //CapPhat
            configuration.CreateMap<CapPhat, CapPhatDto>();
            configuration.CreateMap<CapPhatInput, CapPhat>();
            configuration.CreateMap<CapPhat, CapPhatInput>();
            configuration.CreateMap<CapPhat, CapPhatForViewDto>();
            
            //DieuChuyen
            configuration.CreateMap<DieuChuyen, DieuChuyenDto>();
            configuration.CreateMap<DieuChuyenInput, DieuChuyen>();
            configuration.CreateMap<DieuChuyen, DieuChuyenInput>();
            configuration.CreateMap<DieuChuyen, DieuChuyenForViewDto>();

            //ThuHoi
            configuration.CreateMap<ThuHoi, ThuHoiDto>();
            configuration.CreateMap<ThuHoiInput, ThuHoi>();
            configuration.CreateMap<ThuHoi, ThuHoiInput>();
            configuration.CreateMap<ThuHoi, ThuHoiForViewDto>();
        }
    }
}