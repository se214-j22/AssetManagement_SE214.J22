using AutoMapper;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Plans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Products.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ProductType.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Projects.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Purchases.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.SubPlans.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace GWebsite.AbpZeroTemplate.Applications
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MenuClient, MenuClientDto>();
            configuration.CreateMap<Product, ProductDto>();
            configuration.CreateMap<Plan, PlanDto>();
            //.ForMember(dto => dto.Name, opt => opt.MapFrom(model => model.ProductType.Name));
            configuration.CreateMap<ProductType, ProductTypeDto>()
                         .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.Id));
            configuration.CreateMap<ProductTypeSavedDto, ProductType>()
                         .ForMember(dto => dto.Products, opt => opt.Ignore());
            configuration.CreateMap<Purchase, PurchaseDto>();
            configuration.CreateMap<Bidding, BiddingProduct>();
            configuration.CreateMap<Supplier, SupplierDto>();
            configuration.CreateMap<SupplierSavedDto, SupplierDto>();
            configuration.CreateMap<SupplierType, SupplierTypeDto>()
                         .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.Id));
            configuration.CreateMap<SupplierTypeDto, SupplierType>()
                         .ForMember(dto => dto.Suppliers, opt => opt.Ignore());
            configuration.CreateMap<BidProfile, BidProfileAllDto>();
            configuration.CreateMap<SubPlan, SubPlanDto>();
            configuration.CreateMap<PurchaseDto, Purchase>();
            configuration.CreateMap<MenuClient, MenuClientListDto>();
            configuration.CreateMap<CreateMenuClientInput, MenuClient>();
            configuration.CreateMap<UpdateMenuClientInput, MenuClient>();
            configuration.CreateMap<ContractDto, Contract>();
            configuration.CreateMap<Contract, ContractDto>();
            configuration.CreateMap<Project, ProjectDto>();
            configuration.CreateMap<BidProfile, BidProfileDto>();

            // DemoModel
            configuration.CreateMap<DemoModel, DemoModelDto>();
            configuration.CreateMap<DemoModelInput, DemoModel>();
            configuration.CreateMap<DemoModel, DemoModelInput>();


            // revert mapper 
            configuration.CreateMap<BidProfileSaveForCreate, BidProfile>()
                .ForMember(p => p.BidUnits, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    p.BidUnits = new Collection<BidUnit>();
                    var addedProduct = pr.BidUnits.Where(id => p.BidUnits.All(pc => pc.ProductId != id.ProductId && pc.BidProfileId != id.BidProfileId))
                        .Select(id => new BidUnit() { ProductId = id.ProductId, BidProfileId = pr.Id, SubmitDate = id.SubmitDate, BeginCost = id.BeginCost, Bank = id.Bank, ProofNum = id.ProofNum, Note = id.Note, Status = id.Status }).ToList();
                    foreach (var pc in addedProduct)
                    {
                        p.BidUnits.Add(pc);
                    }

                    var removedProduct =
                        p.BidUnits.Where(c => pr.BidUnits.FirstOrDefault(x => x.ProductId == c.ProductId).Equals(null)).ToList();
                    foreach (var pc in removedProduct)
                    {
                        p.BidUnits.Remove(pc);
                    }
                });
            configuration.CreateMap<BidProfileSaved, BidProfile>();
            configuration.CreateMap<ProjectSavedDto, Project>();
            configuration.CreateMap<SupplierTypeSavedDto, SupplierType>();
            configuration.CreateMap<ProductSavedDto, Product>();
            configuration.CreateMap<ProductSavedCreate, Product>();
            configuration.CreateMap<SubPlanSavedDto, SubPlan>();
            configuration.CreateMap<PlanSavedDto, Plan>();
            configuration.CreateMap<BiddingSaved, Bidding>();
            configuration.CreateMap<ContractSaved, Contract>();
            configuration.CreateMap<PlanSavedDto, Plan>();
            //.ForMember(p => p.SubPlans, opt => opt.Ignore())
            //.AfterMap((pr, p) =>
            //{
            //    p.SubPlans = new Collection<SubPlan>();
            //    var addedProduct = pr.SubPlans.Where(id => p.SubPlans.All(pc => pc.ProductId != id.ProductId && pc.PlanId != id.PlanId))
            //        .Select(id => new SubPlan() { ProductId = id.ProductId, PlanId = pr.Id, Quantity = id.Quantity,ImplementPrice=0, }).ToList();
            //    foreach (var pc in addedProduct)
            //    {
            //        p.SubPlans.Add(pc);
            //    }

            //    var removedProduct =
            //        p.SubPlans.Where(c => pr.SubPlans.FirstOrDefault(x => x.ProductId == c.ProductId).Equals(null)).ToList();
            //    foreach (var pc in removedProduct)
            //    {
            //        p.SubPlans.Remove(pc);
            //    }
            //});
            configuration.CreateMap<PurchaseSave, Purchase>()
                .ForMember(p => p.PurchaseProducts, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    p.PurchaseProducts = new Collection<PurchaseProduct>();
                    var addedProduct = pr.PurchaseProducts.Where(id => p.PurchaseProducts.All(pc => pc.ProductId != id.ProductId && pc.Quantity != id.Quantity))
                        .Select(id => new PurchaseProduct() { ProductId = id.ProductId, PurchaseId = pr.Id, Quantity = id.Quantity }).ToList();
                    foreach (var pc in addedProduct)
                    {
                        p.PurchaseProducts.Add(pc);
                    }

                    var removedProduct =
                        p.PurchaseProducts.Where(c => pr.PurchaseProducts.FirstOrDefault(x => x.ProductId == c.ProductId).Equals(null)).ToList();
                    foreach (var pc in removedProduct)
                    {
                        p.PurchaseProducts.Remove(pc);
                    }
                });
            configuration.CreateMap<SupplierSavedDto, Supplier>();
            //.ForMember(p => p.Biddings, opt => opt.Ignore())
            //.AfterMap((pr, p) =>
            //{
            //    var biddingAdded = pr.Biddings.Where(id => p.Biddings.All(pc => pc.ProductId != id.ProductId && pc.SupplierId != id.SupplierId))
            //                            .Select(id => new Bidding() { ProductId = id.ProductId, SupplierId = pr.Id, StartDate = id.StartDate, EndDate = id.EndDate, Status = id.Status }).ToList();
            //    foreach (var pc in biddingAdded)
            //    {
            //        p.Biddings.Add(pc);
            //    }

            //    var removedProduct =
            //        p.Biddings.Where(c => pr.Biddings.FirstOrDefault(x => x.ProductId == c.ProductId && x.SupplierId == c.SupplierId).Equals(null)).ToList();
            //    foreach (var pc in removedProduct)
            //    {
            //        p.Biddings.Remove(pc);
            //    }
            //});
        }
    }
}

//.ForMember(p => p.PurchaseProducts, opt => opt.Ignore())
//            .AfterMap((pr, p) =>
//            {
//    foreach (var pc in pr.PurchaseProducts)
//        p.PurchaseProducts.Add(new PurchaseProduct() { ProductId = 5, PurchaseId = 3, Quantity = 10 });

//});