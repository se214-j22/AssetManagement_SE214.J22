using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Purchases;
using GWebsite.AbpZeroTemplate.Application.Share.Purchases.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Purchases
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_Purchase)]
    public class PurchaseAppService : GWebsiteAppServiceBase, IPurchaseAppService
    {

        private readonly IRepository<Purchase, int> _purchaseRepository;

        public PurchaseAppService(IRepository<Purchase, int> purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }


        public async Task<PagedResultDto<PurchaseDto>> GetPurchasesAsync(Pagination pagination)
        {
            var query = _purchaseRepository.GetAllIncluding(p => p.PurchaseProducts, p => p.Department);
            var totalCount = await query.CountAsync();
            var items = await query.Skip(pagination.Start * pagination.NumberItem).Take(pagination.NumberItem).ToListAsync();
            return new PagedResultDto<PurchaseDto>(
             totalCount,
             items.Select(item => ObjectMapper.Map<PurchaseDto>(item)).ToList());

        }

        public async Task<PagedResultDto<PurchaseDto>> GetPurchasesAsync(GetPurchaseInput input)
        {
            var query = _purchaseRepository.GetAll()
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), m => m.User.Name.Contains(input.Name));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(x => x.User.Name).Skip(input.Start * input.NumberItem).Take(input.NumberItem).ToListAsync();

            return new PagedResultDto<PurchaseDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<PurchaseDto>(item)).ToList());
        }

        public async Task<PurchaseDto> GetPurchaseForEditAsync(NullableIdDto input)
        {
            var item = await _purchaseRepository.GetAllIncluding(p => p.PurchaseProducts, p => p.Department).FirstOrDefaultAsync(x => x.Id == input.Id);
            return ObjectMapper.Map<PurchaseDto>(item);
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Purchase_Create)]
        public async Task<int> CreatePurchaseAsync(PurchaseSave input)
        {
            var user = await GetCurrentUserAsync();
            var entity = ObjectMapper.Map<Purchase>(input);
            entity.Level = user.Level;
            return await _purchaseRepository.InsertAndGetIdAsync(entity);
        }


        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Purchase_Edit)]
        public async Task<PurchaseDto> UpdatePurchaseAsync(PurchaseSave input)
        {
            var entity = await _purchaseRepository.GetAllIncluding(p => p.PurchaseProducts, p1 => p1.User, p2 => p2.Department).FirstOrDefaultAsync(x => x.Id == input.Id);
            ObjectMapper.Map(input, entity);
            entity = await _purchaseRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<PurchaseDto>(entity);
        }


        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Purchase_Delete)]
        public async Task DeletePurchaseAsync(EntityDto<int> input)
        {
            var entity = await _purchaseRepository.GetAsync(input.Id);
            entity.Status = 3;
            entity = await _purchaseRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_Purchase_Edit)]
        public async Task<string> ApprovePurchaseAsync(EntityDto<int> input)
        {
            var user = await GetCurrentUserAsync();
            var entity = await _purchaseRepository.GetAsync(input.Id);
            if (user.Level > entity.Level)
            {
                entity.Status = 2;
                entity = await _purchaseRepository.UpdateAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();
                return user.Name + "has approved purchase Id " + entity.Id;
            }
            return "You don't enought to approve purchase Id " + entity.Id;
        }
    }
}
