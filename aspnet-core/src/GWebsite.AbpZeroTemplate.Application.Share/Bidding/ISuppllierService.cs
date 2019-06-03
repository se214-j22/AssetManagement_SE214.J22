using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Product.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Bidding
{
  public interface ISupplierAppService
    {
        Task<PagedResultDto<SupplierDto>> GetSupplierByTypeAsync(GetMenuClientInput input, string SupplierType);
        Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(GetMenuClientInput input);
        Task DeleteSupplierCatalogAsync(EntityDto<int> input);
        Task<SupplierTypeDto> CreateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto);
        Task<SupplierTypeDto> UpdateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto);
        //Task<PagedResultDto<SupplierDto>> GetSupplierByProductAsync(GetMenuClientInput input, int productId);
        Task<PagedResultDto<FilterSupplierTypeResponeModel>> GetSupplierTypesWithFilterAsync(int skipCount, int pageSize, string code, string name, int status);
        //Task<SupplierTypeDto> ToggleStatusSupplierCatalogAsync(EntityDto<int> input);
        //Task<PagedResultDto<SupplierDto>> GetAllBiddingPassAsync(GetMenuClientInput input);
        //Task<BiddingProduct> BiddingProductAsync(BiddingSaved biddingSaved);
        //Task<BiddingProduct> ChangeOwnerBiddingProductAsync(BiddingSaved biddingSaved);
        Task<SupplierDto> CreateSupplierAsync(SupplierSavedDto supplierSavedDto);
        Task<SupplierDto> UpdateSupplierAsync(SupplierSavedDto supplierSavedDto);
        Task<SupplierDto> GetSupplierByIdAsync(EntityDto<int> input);
        Task DeleteSupplierAsync(EntityDto<int> input);
        Task<PagedResultDto<SupplierDto>> GetSupplierWithFilterAsync(GetProductInput input);

    }
}
