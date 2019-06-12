using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.BidProfiles
{

    [AbpAuthorize(GWebsitePermissions.Pages_Administration_BidProfile)]
    public class BidProfileAppService : GWebsiteAppServiceBase, IBidProfileAppService
    {
        private readonly IRepository<BidProfile, int> bidProfileRepository;
        public BidProfileAppService(IRepository<BidProfile, int> bidProfileRepository)
        {
            this.bidProfileRepository = bidProfileRepository;
        }
        public async Task<PagedResultDto<BidProfileDto>> GetBidProfileWithFilterAsync(BidProfileImput input)
        {
            IQueryable<BidProfile> query = bidProfileRepository.GetAllIncluding(p => p.Project, x => x.OrganizationUnit);
            if (input.Code != null)
            {
                query = query.Where(p => p.Code.Contains(input.Code));
            }

            if (input.Status == 1 || input.Status == 2)
            {
                query = query.Where(p => p.Status == input.Status);
            }
            if (input.BidCatalog != null)
            {
                query = query.Where(p => p.BidCatalog.Contains(input.BidCatalog));
            }
            if (input.BidType != null)
            {
                query = query.Where(p => p.BidType.Contains(input.BidType));
            }
            if (input.ReceivedDate.Year > 2000)
            {
                query = query.Where(p => input.ReceivedDate >= p.StartReceivedDate && input.ReceivedDate <= p.EndReceivedDate);
            }
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<BidProfileDto>(
               totalCount,
               items.Select(item => this.ObjectMapper.Map<BidProfileDto>(item)).ToList());
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BidProfile_Create)]
        public async Task<IServiceResult> DeleteBidProfileAsync(int id)
        {
            try
            {

                
                BidProfile query = await this.bidProfileRepository.FirstOrDefaultAsync(item => item.Id == id);
                if (query.Status == 2)
                {
                    await this.bidProfileRepository.DeleteAsync(query);
                }
                else
                {
                    return new ServiceResult(false, message: "This Bid profile can't delete");
                }
                return new ServiceResult(payload: "Success");
            }
            catch (Exception e)
            {
                return new ServiceResult(false, message: "This Bid profile can't delete");
            }

        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BidProfile_Edit)]
        public async Task<BidProfileDto> UpdateProductCatalogAsync(BidProfileSaved bidProfileSaved)
        {
            BidProfile entity = await this.bidProfileRepository.GetAllIncluding(p => p.Project).FirstOrDefaultAsync(item => item.Id == bidProfileSaved.Id);
            this.ObjectMapper.Map(bidProfileSaved, entity);
            entity = await this.bidProfileRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<BidProfileDto>(entity);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BidProfile_Create)]
        public async Task<BidProfileDto> CreateProductCatalogAsync(BidProfileSaveForCreate BidProfile)
        {
            BidProfile productType = ObjectMapper.Map<BidProfile>(BidProfile);
            await bidProfileRepository.InsertAndGetIdAsync(productType);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<BidProfileDto>(productType);
        }
        public async Task<BidProfileAllDto> GetBidProfileByIdAsync(int id)
        {
            BidProfile bidProfile = await bidProfileRepository.GetAllIncluding().Include(p => p.Project).Include(p => p.BidUnits).ThenInclude(p => p.Product).Include(x => x.OrganizationUnit).FirstOrDefaultAsync(p => p.Id == id);
            return this.ObjectMapper.Map<BidProfileAllDto>(bidProfile);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_BidProfile_Edit)]
        public async Task<BidProfileDto> ApprovalBidProfileAsync(int id)
        {
            BidProfile entity = await this.bidProfileRepository.GetAllIncluding(p => p.Project).FirstOrDefaultAsync(item => item.Id == id);
            entity.Status = 1;
            entity = await this.bidProfileRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<BidProfileDto>(entity);
        }


    }
}
