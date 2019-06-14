using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Customers.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_SuaChua.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyToaNha.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Customers_QuanLyCongTrinhXayDung.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
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

            //Customer_suachua
            configuration.CreateMap<Customer_SuaChua, CustomerDto_SuaChua>();
            configuration.CreateMap<CustomerInput_SuaChua, Customer_SuaChua>();
            configuration.CreateMap<Customer_SuaChua, CustomerInput_SuaChua>();
            configuration.CreateMap<Customer_SuaChua, CustomerForViewDto_SuaChua>();

            //QuanLyToaNha
            configuration.CreateMap<Customer_QuanLyToaNha, CustomerDto_QuanLyToaNha>();
            configuration.CreateMap<CustomerInput_QuanLyToaNha, Customer_QuanLyToaNha>();
            configuration.CreateMap<Customer_QuanLyToaNha, CustomerInput_QuanLyToaNha>();
            configuration.CreateMap<Customer_QuanLyToaNha, CustomerForViewDto_QuanLyToaNha>();


            //QuanLyCongTrinhXayDung
            configuration.CreateMap<Customer_QuanLyCongTrinhXayDung, CustomerDto_QuanLyCongTrinhXayDung>();
            configuration.CreateMap<CustomerInput_QuanLyCongTrinhXayDung, Customer_QuanLyCongTrinhXayDung>();
            configuration.CreateMap<Customer_QuanLyCongTrinhXayDung, CustomerInput_QuanLyCongTrinhXayDung>();
            configuration.CreateMap<Customer_QuanLyCongTrinhXayDung, CustomerForViewDto_QuanLyCongTrinhXayDung>();
        }
    }
}