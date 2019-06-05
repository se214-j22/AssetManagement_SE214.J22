using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.BidProfile
{
  public  interface IBidProfileAppService
    {
        Task<PagedResultDto<BidProfileDto>> GetBidProfileWithFilterAsync(BidProfileImput input);
        Task DeleteBidProfileAsync(int id);
        Task<BidProfileDto> UpdateProductCatalogAsync(BidProfileSaved bidProfileSaved);
        Task<BidProfileDto> CreateProductCatalogAsync(BidProfileDto BidProfile);
    }
}
